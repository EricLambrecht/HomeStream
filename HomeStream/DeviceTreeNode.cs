using System;

namespace HomeStream
{
	[Gtk.TreeNode (ListOnly = true)]
	public class DeviceTreeNode : Gtk.TreeNode
	{

		[Gtk.TreeNodeValue (Column=0)]
		public string Name;

		[Gtk.TreeNodeValue (Column=1)]
		public string IP;


		public DeviceTreeNode (string name, string ip)
		{
			Name = name;
			IP = ip;
		}
	}
}

