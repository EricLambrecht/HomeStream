using System;

namespace HomeStream
{
	public static class ImageUtilities
	{
		public static byte[] GetScreenshot(int compressionLevel) {

			var root = Gdk.Global.DefaultRootWindow;

			int width, height;
			root.GetSize (out width, out height);

			var tmp = new Gdk.Pixbuf (Gdk.Colorspace.Rgb, false, 8, width, height);
			var screenshot = tmp.GetFromDrawable (root, root.Colormap, 0, 0, 0, 0, width, height);

			if (compressionLevel == 0) {
				// return uncompressed
			}
			screenshot.Save ("screen.jpg", "jpeg");
			screenshot.Save ("screen.bmp", "bmp");
			return screenshot.SaveToBuffer ("jpeg");
		}
	}
}

