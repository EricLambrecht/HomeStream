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
		/// <summary>
		/// The app's main window.
		/// </summary>
		MainWindow Win;
		/// <summary>
		/// A nice and tasty tortilla.
		/// </summary>
		protected Tortilla Tortilla;
		/// <summary>
		/// A list of all receivers, i.e. all IPs that receive a stream from this app.
		/// </summary>
		public List<Receiver> Receivers;
		/// <summary>
		/// Gets a value indicating whether this <see cref="HomeStream.HomeStreamApp"/> is streaming.
		/// </summary>
		/// <value><c>true</c> if streaming; otherwise, <c>false</c>.</value>
		public bool Streaming { get; private set;}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="HomeStream.HomeStreamApp"/> is supposed to show details.
		/// </summary>
		/// <value><c>true</c> if show details; otherwise, <c>false</c>.</value>
		public bool ShowDetails { get; set; }
		/// <summary>
		/// The status bar ID for writing into the main window.
		/// </summary>
		const int statusBarID = 2;

		public static void Main (string[] args)
		{
			new HomeStreamApp ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HomeStream.HomeStreamApp"/> class.
		/// </summary>
		public HomeStreamApp() {
			Application.Init ();
			Tortilla = new Tortilla ();
			Tortilla.OutputReceived += OnOutputReceived;
			try {
				Tortilla.UpdateDevices();
			}
			catch(System.IO.FileNotFoundException e) { 
				Win.InvokeLogLine ("Fehler: " + e.Message);
			}

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
			
		protected void OnDetailsToggled (object sender, EventArgs e)
		{
			ShowDetails = !ShowDetails;
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

		/// <summary>
		/// Sends pings to a specified LAN-Address and checks whether it's responding or not. 
		/// If so, the IP will be added to the main window.
		/// </summary>
		/// <returns>A boolean indicating that the task has finished.</returns>
		/// <param name="ipOrHost">IP-address or hostname.</param>
		/// <param name="totalAmountOfPings">Total amount of pings to calculate progress in main window.</param>
		public async Task<bool> PingAndProcessAsync(string ipOrHost, int totalAmountOfPings) {

			PingReply reply;

			// Send a asynchronous ping.
			using (Ping pingSender = new Ping ()) {
				byte[] buffer = GetPingBuffer ();
				int timeout = 300;
				var options = new PingOptions (128, true);
				reply = await pingSender.SendPingAsync(ipOrHost, timeout, buffer, options);
			}

			// Write ping reply (when ping completed).
			if (ShowDetails) {
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

			// TODO: Move this to a appropriate spot, e.g settings window.
			foreach (string videoDevice in Tortilla.VideoDevices) { 
				Win.InvokeLogLine (videoDevice);
			}
			foreach (string audioDevice in Tortilla.AudioDevices) { 
				Win.InvokeLogLine (audioDevice);
			}
		}

		/// <summary>
		/// Starts a stream to the current selected IP in the main window.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">Event Args.</param>
		protected async void OnStreamingAttempt (object sender, EventArgs e) {
			if (!Streaming) 
			{
				// Streaming target is the selected ip in our MainWindow.
				string ip = Win.SelectedDeviceTreeNode.IP;
				Win.InvokeChangeStatus (statusBarID, "Streaming...");
				Win.InvokeLogLine (string.Format("Streaming to IP {0}...", ip));
				Streaming = true;

				// We create two tasks, one that streams the data and one that logs the corresponding output.
				Task streaming = Tortilla.StreamWindowsScreenToIpAsync ("UScreenCapture", "Stereo Mix (ASUS Xonar D1 Audio Device)", ip +":8090", StreamingMode.UDP);
				Task logging = Tortilla.LogFFmpegOutput ();
				await Task.WhenAll (streaming, logging);

				Streaming = false;
				Win.InvokeLogLine ("Streaming stopped.");
				Win.InvokeChangeStatus (statusBarID, ""); // Clear statusbar
			} 
			else 
			{
				Win.InvokeLogLine ("Trying to stop stream...");
				// 'q' quits a running ffmpeg process.
				Tortilla.SendInputToFFmpegProcess ('q');
			}
		}

		/// <summary>
		/// If <see cref="HomeStream.HomeStreamApp.ShowDetails"/> is set to true, this function will output all incoming output from Tortilla.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected virtual void OnOutputReceived (object sender, OutputReceivedEventArgs e)
		{
			if (ShowDetails)
				Win.InvokeLogLine (Tortilla.Output.Last ());
		}

		protected virtual void OnReceiverAdded (object sender, ConnectionEventArgs e) {
			Receivers.Add (new Receiver (e.IP, e.Name));
			ConnectToIp ("Added: " + e.IP + "to receiver list");
		}

	}

}
