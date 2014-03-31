using System;
using System.Collections;
using Gtk;
using HomeStream;

public partial class MainWindow: Gtk.Window
{
	public NodeView DeviceView;
	public DeviceTreeNode SelectedDeviceTreeNode;
	public event EventHandler RefreshRequest;
	public event EventHandler StreamingAttempt;
	public event EventHandler<ConnectionEventArgs> ReceiverAdded;
	public event EventHandler<ConnectionEventArgs> ConnectionAttempt;
	private readonly object SyncRoot = new object();
	public NodeStore Devices  { get; set; }
	protected Window childWindow;
	const int statusBarID = 1;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Devices = new NodeStore (typeof(DeviceTreeNode));
		DeviceView = new NodeView (Devices);
		scrolledwindow1.Add (DeviceView);
		DeviceView.AppendColumn ("Name", new CellRendererText (), "text", 0);
		DeviceView.AppendColumn ("IP", new CellRendererText (), "text", 1);
		DeviceView.ShowAll ();
		DeviceView.NodeSelection.Changed += OnDeviceSeletectionChanged;
		SelectedDeviceTreeNode = new DeviceTreeNode ("", "");
	}


	public void LogLine(string line) 
	{
		logview.Buffer.Text += line + '\n';
	}

	public void AddDevice (string ip, string name = "Unbekanntes Gerät") 
	{
		Devices.AddNode (new DeviceTreeNode (name, ip));
	}

	public void ResetDevices() 
	{
		Devices.Clear ();
	}

	public void UpdateDevice(string ip, string name) 
	{
		foreach (DeviceTreeNode device in Devices) {
			if (device.IP == ip) {
				device.Name = name;
				DeviceView.ShowAll();
				break;
			}
		}
	}

	public void ChangeStatus(uint id, string status) 
	{
		statusbar.Pop (id);
		statusbar.Push (id, status);
	}

	public void ResetProgress() 
	{
		progressbar.Fraction = 0;
	}

	public void ShowProgress(double amount) 
	{
		progressbar.Fraction += amount;
	}


	/*
	 * The following methods should be thread safe.
	 */

	public void InvokeLogLine(string line) 
	{
		Gtk.Application.Invoke (delegate {
			LogLine(line);
		});
	}

	public void InvokeAddDevice(string ip, string name = "Unbekanntes Gerät") 
	{
		Gtk.Application.Invoke (delegate {
			AddDevice(ip, name);
		});
	}

	public void InvokeUpdateDevice(string ip, string name) 
	{
		Gtk.Application.Invoke (delegate {
			UpdateDevice(ip, name);
		});
	}

	public void InvokeChangeStatus(uint id, string status) 
	{
		Gtk.Application.Invoke (delegate {
			ChangeStatus(id, status);
		});
	}

	public void InvokeShowProgress(double amount) 
	{
		Gtk.Application.Invoke (delegate {
			ShowProgress(amount);
		});
	}

	protected void OnDeviceSeletectionChanged (object sender, EventArgs args) 
	{
		NodeSelection selection = (NodeSelection)sender;
		SelectedDeviceTreeNode = (DeviceTreeNode)selection.SelectedNode;
	}

	protected void OnClickRefresh (object sender, EventArgs e)
	{
		EventHandler handler = RefreshRequest;
		if (handler != null) {
			handler (this, e); // raise RefreshRequest
		}
	}

	protected void OnClickConnect (object sender, EventArgs e)
	{
		EventHandler<ConnectionEventArgs> handler = ConnectionAttempt;
		if (handler != null) {
			handler (this, new ConnectionEventArgs(SelectedDeviceTreeNode.IP, SelectedDeviceTreeNode.Name)); // raise ConnectAttempt
		}
	}

	protected void OnClickStream (object sender, EventArgs e)
	{
		EventHandler handler = StreamingAttempt;
		if (handler != null) {
			handler (this, e); 
		}
	}

	protected void OnAdd (object sender, EventArgs e)
	{
		EventHandler<ConnectionEventArgs> handler = ReceiverAdded;
		if (handler != null) {
			handler (this, new ConnectionEventArgs(SelectedDeviceTreeNode.IP, SelectedDeviceTreeNode.Name)); // raise ConnectAttempt
		}
	}

	protected void OnExit (object sender, EventArgs e)
	{
		Application.Quit ();
	}
		
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
		
	protected void OnOpenSettings (object sender, EventArgs e)
	{
		childWindow = new SettingsWindow ();
	}
		
}

