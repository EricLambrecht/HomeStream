
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	private global::Gtk.Action HauptmenAction;
	private global::Gtk.Action InfoAction;
	private global::Gtk.Action HilfeAction;
	private global::Gtk.Action MitGertVerbindenAction;
	private global::Gtk.Action BeendenAction;
	private global::Gtk.Action EinstellungenAction;
	private global::Gtk.VBox vbox1;
	private global::Gtk.MenuBar menubar;
	private global::Gtk.VBox vbox3;
	private global::Gtk.HBox hbox1;
	private global::Gtk.ScrolledWindow scrolledwindow1;
	private global::Gtk.VBox vbox4;
	private global::Gtk.Button buttonRefreshNetwork;
	private global::Gtk.Button buttonConnect;
	private global::Gtk.Button button1;
	private global::Gtk.Button buttonBroadcast;
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	private global::Gtk.TextView logview;
	private global::Gtk.CheckButton showDetailsCheckBox;
	private global::Gtk.Statusbar statusbar;
	private global::Gtk.ProgressBar progressbar;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.HauptmenAction = new global::Gtk.Action ("HauptmenAction", global::Mono.Unix.Catalog.GetString ("Hauptmenü"), null, null);
		this.HauptmenAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Hauptmenü");
		w1.Add (this.HauptmenAction, null);
		this.InfoAction = new global::Gtk.Action ("InfoAction", global::Mono.Unix.Catalog.GetString ("Info"), null, null);
		this.InfoAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Info");
		w1.Add (this.InfoAction, null);
		this.HilfeAction = new global::Gtk.Action ("HilfeAction", global::Mono.Unix.Catalog.GetString ("Hilfe"), null, null);
		this.HilfeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Hilfe");
		w1.Add (this.HilfeAction, null);
		this.MitGertVerbindenAction = new global::Gtk.Action ("MitGertVerbindenAction", global::Mono.Unix.Catalog.GetString ("Mit Gerät verbinden..."), null, null);
		this.MitGertVerbindenAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Mit Gerät verbinden...");
		w1.Add (this.MitGertVerbindenAction, null);
		this.BeendenAction = new global::Gtk.Action ("BeendenAction", global::Mono.Unix.Catalog.GetString ("Beenden"), null, null);
		this.BeendenAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Beenden");
		w1.Add (this.BeendenAction, null);
		this.EinstellungenAction = new global::Gtk.Action ("EinstellungenAction", global::Mono.Unix.Catalog.GetString ("Einstellungen"), null, null);
		this.EinstellungenAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Einstellungen");
		w1.Add (this.EinstellungenAction, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.WidthRequest = 768;
		this.HeightRequest = 512;
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("HomeStream");
		this.Icon = global::Stetic.IconLoader.LoadIcon (this, "gtk-network", global::Gtk.IconSize.Menu);
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString (@"<ui><menubar name='menubar'><menu name='HauptmenAction' action='HauptmenAction'><menuitem name='MitGertVerbindenAction' action='MitGertVerbindenAction'/><menuitem name='BeendenAction' action='BeendenAction'/></menu><menu name='EinstellungenAction' action='EinstellungenAction'/><menu name='InfoAction' action='InfoAction'/><menu name='HilfeAction' action='HilfeAction'/></menubar></ui>");
		this.menubar = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar")));
		this.menubar.Name = "menubar";
		this.vbox1.Add (this.menubar);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.menubar]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.vbox3 = new global::Gtk.VBox ();
		this.vbox3.Name = "vbox3";
		this.vbox3.Spacing = 6;
		// Container child vbox3.Gtk.Box+BoxChild
		this.hbox1 = new global::Gtk.HBox ();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.scrolledwindow1 = new global::Gtk.ScrolledWindow ();
		this.scrolledwindow1.HeightRequest = 200;
		this.scrolledwindow1.CanFocus = true;
		this.scrolledwindow1.Name = "scrolledwindow1";
		this.scrolledwindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		this.scrolledwindow1.BorderWidth = ((uint)(6));
		this.hbox1.Add (this.scrolledwindow1);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.scrolledwindow1]));
		w3.Position = 0;
		// Container child hbox1.Gtk.Box+BoxChild
		this.vbox4 = new global::Gtk.VBox ();
		this.vbox4.Name = "vbox4";
		this.vbox4.Spacing = 6;
		// Container child vbox4.Gtk.Box+BoxChild
		this.buttonRefreshNetwork = new global::Gtk.Button ();
		this.buttonRefreshNetwork.CanFocus = true;
		this.buttonRefreshNetwork.Name = "buttonRefreshNetwork";
		this.buttonRefreshNetwork.UseUnderline = true;
		this.buttonRefreshNetwork.Relief = ((global::Gtk.ReliefStyle)(2));
		this.buttonRefreshNetwork.Xalign = 1F;
		this.buttonRefreshNetwork.BorderWidth = ((uint)(6));
		this.buttonRefreshNetwork.Label = global::Mono.Unix.Catalog.GetString ("Netzwerk durchsuchen");
		global::Gtk.Image w4 = new global::Gtk.Image ();
		w4.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-refresh", global::Gtk.IconSize.Button);
		this.buttonRefreshNetwork.Image = w4;
		this.vbox4.Add (this.buttonRefreshNetwork);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.buttonRefreshNetwork]));
		w5.Position = 0;
		w5.Expand = false;
		w5.Fill = false;
		// Container child vbox4.Gtk.Box+BoxChild
		this.buttonConnect = new global::Gtk.Button ();
		this.buttonConnect.CanFocus = true;
		this.buttonConnect.Name = "buttonConnect";
		this.buttonConnect.UseUnderline = true;
		this.buttonConnect.FocusOnClick = false;
		this.buttonConnect.BorderWidth = ((uint)(6));
		this.buttonConnect.Label = global::Mono.Unix.Catalog.GetString ("Verbinden");
		global::Gtk.Image w6 = new global::Gtk.Image ();
		w6.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-connect", global::Gtk.IconSize.Menu);
		this.buttonConnect.Image = w6;
		this.vbox4.Add (this.buttonConnect);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.buttonConnect]));
		w7.Position = 1;
		w7.Expand = false;
		w7.Fill = false;
		// Container child vbox4.Gtk.Box+BoxChild
		this.button1 = new global::Gtk.Button ();
		this.button1.CanFocus = true;
		this.button1.Name = "button1";
		this.button1.UseUnderline = true;
		this.button1.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");
		this.vbox4.Add (this.button1);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.button1]));
		w8.Position = 2;
		w8.Expand = false;
		w8.Fill = false;
		// Container child vbox4.Gtk.Box+BoxChild
		this.buttonBroadcast = new global::Gtk.Button ();
		this.buttonBroadcast.CanFocus = true;
		this.buttonBroadcast.Name = "buttonBroadcast";
		this.buttonBroadcast.UseUnderline = true;
		this.buttonBroadcast.BorderWidth = ((uint)(6));
		this.buttonBroadcast.Label = global::Mono.Unix.Catalog.GetString ("_Streamen");
		global::Gtk.Image w9 = new global::Gtk.Image ();
		w9.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-media-play", global::Gtk.IconSize.Button);
		this.buttonBroadcast.Image = w9;
		this.vbox4.Add (this.buttonBroadcast);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.buttonBroadcast]));
		w10.PackType = ((global::Gtk.PackType)(1));
		w10.Position = 3;
		w10.Expand = false;
		w10.Fill = false;
		this.hbox1.Add (this.vbox4);
		global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vbox4]));
		w11.Position = 1;
		w11.Expand = false;
		w11.Fill = false;
		this.vbox3.Add (this.hbox1);
		global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox1]));
		w12.Position = 0;
		w12.Expand = false;
		w12.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		this.GtkScrolledWindow.BorderWidth = ((uint)(6));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.logview = new global::Gtk.TextView ();
		this.logview.HeightRequest = 100;
		this.logview.CanFocus = true;
		this.logview.Name = "logview";
		this.GtkScrolledWindow.Add (this.logview);
		this.vbox3.Add (this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.GtkScrolledWindow]));
		w14.Position = 1;
		// Container child vbox3.Gtk.Box+BoxChild
		this.showDetailsCheckBox = new global::Gtk.CheckButton ();
		this.showDetailsCheckBox.CanFocus = true;
		this.showDetailsCheckBox.Name = "showDetailsCheckBox";
		this.showDetailsCheckBox.Label = global::Mono.Unix.Catalog.GetString ("Details anzeigen");
		this.showDetailsCheckBox.DrawIndicator = true;
		this.showDetailsCheckBox.UseUnderline = true;
		this.showDetailsCheckBox.BorderWidth = ((uint)(6));
		this.vbox3.Add (this.showDetailsCheckBox);
		global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.showDetailsCheckBox]));
		w15.Position = 2;
		w15.Expand = false;
		w15.Fill = false;
		this.vbox1.Add (this.vbox3);
		global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.vbox3]));
		w16.Position = 1;
		// Container child vbox1.Gtk.Box+BoxChild
		this.statusbar = new global::Gtk.Statusbar ();
		this.statusbar.Name = "statusbar";
		this.statusbar.Spacing = 6;
		// Container child statusbar.Gtk.Box+BoxChild
		this.progressbar = new global::Gtk.ProgressBar ();
		this.progressbar.Name = "progressbar";
		this.statusbar.Add (this.progressbar);
		global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.statusbar [this.progressbar]));
		w17.Position = 2;
		w17.Expand = false;
		w17.Fill = false;
		this.vbox1.Add (this.statusbar);
		global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.statusbar]));
		w18.Position = 2;
		w18.Expand = false;
		w18.Fill = false;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 768;
		this.DefaultHeight = 512;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.BeendenAction.Activated += new global::System.EventHandler (this.OnExit);
		this.EinstellungenAction.Activated += new global::System.EventHandler (this.OnOpenSettings);
		this.buttonRefreshNetwork.Clicked += new global::System.EventHandler (this.OnClickRefresh);
		this.buttonConnect.Clicked += new global::System.EventHandler (this.OnClickConnect);
		this.buttonBroadcast.Clicked += new global::System.EventHandler (this.OnClickStream);
		this.showDetailsCheckBox.Toggled += new global::System.EventHandler (this.OnDetailsToggle);
	}
}
