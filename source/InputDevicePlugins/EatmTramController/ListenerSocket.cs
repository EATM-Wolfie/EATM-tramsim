using OpenBveApi.Interface;
using System;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EatmTramController
{
	public class ListenerSocket
	{
		/// <summary>
		/// Define KeyDown event
		/// </summary>
		public event EventHandler<byte> KeyDown;

		/// <summary>
		/// Define KeyUp event
		/// </summary>
		public event EventHandler<byte> KeyUp;

		private byte lastKey = 0x00;

		private Socket listener;
		private Socket handler;

		public void StopServer()
		{
			Console.WriteLine("Shutting Down Server");
			handler.Shutdown(SocketShutdown.Both);
			handler.Close();
		}

		public void StartServer()
		{
			// Get Host IP Address that is used to establish a connection
			// In this case, we get one IP address of localhost that is IP : 127.0.0.1
			// If a host has multiple addresses, you will get a list of addresses
			IPHostEntry host = Dns.GetHostEntry("localhost");
			IPAddress ipAddress = host.AddressList[0];
			IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

			try
			{
				// Create a Socket that will use Tcp protocol
				listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				// A Socket must be associated with an endpoint using the Bind method
				listener.Bind(localEndPoint);
				// Specify how many requests a Socket can listen before it gives Server busy response.
				// We will listen 10 requests at a time
				listener.Listen(10);
				SystemSounds.Asterisk.Play();

				Console.WriteLine("Waiting for a connection...");
				handler = listener.Accept();
				byte[] msg = Encoding.ASCII.GetBytes("Hello you are connected");
				handler.Send(msg);
				// Incoming data from the client.
				byte[] bytes = null;

				while (true)
				{
					bytes = new byte[1024];
					int bytesRec = handler.Receive(bytes);

					//clear the terminal screen
					msg = Encoding.ASCII.GetBytes("\u001B[2J");
					handler.Send(msg);

					//we're only interested in byte zero
					byte byteToCheck = bytes[0];
					if (byteToCheck != lastKey)
					{
						msg = Encoding.ASCII.GetBytes(String.Format("V1: Sending Key: ({0:X}) [{2:c}]. Last Key {1:X}) \r\n", byteToCheck, lastKey, GetStringFromKey(byteToCheck)));

						//send it five times
						for (int i = 0; i < 30; i++)
						{
							if (i == 0) handler.Send(msg);
							KeyUp(this, lastKey);
							//System.Threading.Thread.Sleep(10);
						}
						lastKey = byteToCheck;
						//send it five times
						for (int i = 0; i < 30; i++)
						{
							//System.Threading.Thread.Sleep(10);
							KeyDown(this, lastKey);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine("\n Press any key to continue...");
		}

		private string GetStringFromKey(byte b)
		{
			byte[] b1 = new byte[2];
			b1[0] = b;
			return System.Text.Encoding.ASCII.GetString(b1);
		}
	}
}
