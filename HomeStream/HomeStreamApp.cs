using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using Gtk;
using Makhani.Tortilla;

namespace HomeStream
{
	class HomeStreamApp
	{
		MainWindow Win;
		protected Tortilla Tortilla;
		public List<Receiver> Receivers;
		public bool Streaming { get; private set;}
		public bool ShowDetails { get; set; }
		bool DebugMode = false;
		const int statusBarID = 2;

		public static void Main (string[] args)
		{
			new HomeStreamApp ();
		}

		public HomeStreamApp() {
			Application.Init ();
			Tortilla = new Tortilla ();
			Tortilla.OutputReceived += OnOutputReceived;
			Streaming = false;
			ShowDetails = false;
			Win = new MainWindow ();
			Win.ConnectionAttempt += OnConnectionAttempt;
			Win.RefreshRequest += OnRefreshRequest;
			Win.ReceiverAdded += OnReceiverAdded;
			Win.StreamingAttempt += OnStreamingAttempt;
			Win.DetailsToggled += OnDetailsToggled;
			Win.Show ();
			Application.Run ();
		}
			
		protected async void OnDetailsToggled (object sender, EventArgs e)
		{
			ShowDetails = !ShowDetails;
			Win.InvokeLogLine ("Toggled Details");
		}

		protected void OnRefreshRequest (object sender, EventArgs e) {
			SearchForNetworkDevices ();
		}

		protected byte[] GetPingBuffer() {
			// Create a buffer of 32 bytes.
			string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
			byte[] buffer = Encoding.ASCII.GetBytes (data);
			return buffer;
		}

		public async void SearchForNetworkDevices () {

			// UI Stuff: Clear device list, set status, write additional log line, clear progress bar.
			Win.ResetDevices ();
			Win.InvokeChangeStatus (statusBarID, "Pinging...");
			Win.InvokeLogLine("Searching for devices in local network...");
			Win.ResetProgress ();

			// Start a stopwatch.
			Stopwatch stopwatch = new Stopwatch ();
			stopwatch.Start ();

			// IP-Range
			int start = 1;
			int end = 255;

			// Build list containing all IPs in range.
			List<string> ipList = new List<string>(256);
			for (int i = start; i <= end; i++) {
				ipList.Add ("192.168.178." + i.ToString ());
			}

			// Run a task for every IP.
			IEnumerable<Task<bool>> taskResults = from ip in ipList
				                                  select PingAndProcessAsync (ip, end - start);

			// Await all IP-tasks.
			await Task.WhenAll (taskResults);

			// We end the search (the stopwatch, too) and notify the UI.
			stopwatch.Stop ();
			Win.InvokeLogLine("Search took " + ((float)stopwatch.ElapsedMilliseconds/1000).ToString() + " seconds.");
			Win.InvokeChangeStatus (statusBarID, "");
			Win.ResetProgress ();
		}

		public async Task<bool> PingAndProcessAsync(string ipOrHost, int totalAmountOfPings) {

			PingReply reply;

			// Send a asynchronous ping.
			using (Ping pingSender = new Ping ()) {
				byte[] buffer = GetPingBuffer ();
				int timeout = 200;
				var options = new PingOptions (128, true);
				reply = await pingSender.SendPingAsync(ipOrHost, timeout, buffer, options);
			}

			// Write ping reply (when ping completed).
			if (DebugMode) {
				Win.InvokeLogLine (string.Format (
					"{0}: {1} ({2})", 
					ipOrHost, 
					reply.Status.ToString () + ", RTT: " + reply.RoundtripTime, 
					Thread.CurrentThread.ManagedThreadId)
				);
			}

			// If ping was successful we add this ip to the list off online devices in main window.
			if (reply.Status == IPStatus.Success) {
				// Ad ip.
				Win.InvokeAddDevice (reply.Address.ToString ()); 
				try {
					// Update ip with hostname
					var hostEntry = await Dns.GetHostEntryAsync (reply.Address);
					Win.InvokeUpdateDevice (reply.Address.ToString(), hostEntry.HostName);
				}
				catch(System.Net.Sockets.SocketException ex){
					Win.InvokeLogLine (string.Format("{0}: {1} ({2})", "Fehler beim Auflösen des Hostnamens von", reply.Address, ex.Message));
				}
			}

			// Mark progress.
			Win.InvokeShowProgress ((double)1 / totalAmountOfPings);

			// Complete task.
			return true;
		}

		protected void OnConnectionAttempt (object sender, ConnectionEventArgs e) {
			ConnectToIp (e.IP);
		}

		public virtual void ConnectToIp (string ip) { // use IPAddress or something
			Win.InvokeLogLine ("Connecting to " + ip);
			foreach (string videoDevice in Tortilla.VideoDevices) { 
				Win.InvokeLogLine (videoDevice);
			}
			foreach (string audioDevice in Tortilla.AudioDevices) { 
				Win.InvokeLogLine (audioDevice);
			}
			foreach (string line in Tortilla.Output) { 
				Win.InvokeLogLine (line);
			}
			//tortilla.KillProcess (tortilla.FFmpegProcess);
		}

		protected async void OnStreamingAttempt (object sender, EventArgs e) {
			if (!Streaming) 
			{
				string ip = Win.SelectedDeviceTreeNode.IP;
				Win.InvokeLogLine (string.Format("Streaming to IP {0}...", ip));
				Streaming = true;
				Task streaming = Tortilla.StreamWindowsScreenToIpAsync ("UScreenCapture", "Stereo Mix (ASUS Xonar D1 Audio Device)", ip +":8080", StreamingMode.UDP);
				Task logging = Tortilla.LogFFmpegOutput ();
				await Task.WhenAll (streaming, logging);
				Streaming = false;
				Win.InvokeLogLine ("Streaming stopped.");
			} 
			else 
			{
				Win.InvokeLogLine ("Trying to stop stream.");
				Tortilla.SendInputToFFmpegProcess ('q');
				//Tortilla.Kill ();
			}
		}

		void OnOutputReceived (object sender, OutputReceivedEventArgs e)
		{
			if (ShowDetails)
				Win.InvokeLogLine (Tortilla.Output.Last ());
		}

		protected void OnReceiverAdded (object sender, ConnectionEventArgs e) {
			Receivers.Add (new Receiver (e.IP, e.Name));
			ConnectToIp ("Added: " + e.IP + "to receiver list");
		}

	}

}
