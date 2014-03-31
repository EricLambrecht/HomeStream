using System;

namespace HomeStream
{
	public partial class SettingsWindow : Gtk.Window
	{
		public SettingsWindow () : 
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
			Initialize ();
		}

		protected void Initialize() 
		{
			//audioDevicesComboBox.
		}
	}
}

