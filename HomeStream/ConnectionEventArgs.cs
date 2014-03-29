using System;

namespace HomeStream
{
	public class ConnectionEventArgs : EventArgs
	{
		public string IP { get; set; } 
		public string Name { get; set; } 

		public ConnectionEventArgs (string ip, string name)
		{
			IP = ip;
			Name = name;
		}
	}
}

