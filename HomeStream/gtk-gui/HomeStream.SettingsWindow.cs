
// This file has been generated by the GUI designer. Do not modify.
namespace HomeStream
{
	public partial class SettingsWindow
	{
		private global::Gtk.VBox vbox1;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Label settingsLabel;
		private global::Gtk.ComboBox audioDevicesComboBox;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget HomeStream.SettingsWindow
			this.Name = "HomeStream.SettingsWindow";
			this.Title = global::Mono.Unix.Catalog.GetString ("Einstellungen");
			this.Icon = global::Stetic.IconLoader.LoadIcon (this, "gtk-execute", global::Gtk.IconSize.Menu);
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child HomeStream.SettingsWindow.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.settingsLabel = new global::Gtk.Label ();
			this.settingsLabel.WidthRequest = 100;
			this.settingsLabel.Name = "settingsLabel";
			this.settingsLabel.Xalign = 1F;
			this.settingsLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("Aufnahme");
			this.hbox1.Add (this.settingsLabel);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.settingsLabel]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.audioDevicesComboBox = global::Gtk.ComboBox.NewText ();
			this.audioDevicesComboBox.WidthRequest = 200;
			this.audioDevicesComboBox.Name = "audioDevicesComboBox";
			this.hbox1.Add (this.audioDevicesComboBox);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.audioDevicesComboBox]));
			w2.Position = 1;
			this.vbox1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show ();
		}
	}
}
