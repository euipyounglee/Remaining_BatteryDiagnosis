using BaseLib.Pubsub;
using SQLManager.Data.DTO;
using SQLManager.Data.Query;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Net
{
	public class ATcpClientBase : Pubsub.ASubscribableBase<HostInfo>
	{
		#region property

		private TcpClient Client { get; set; }

		public string Hostname { get; private set; }

		public int Port { get; private set; }

		#endregion

		#region method

		public ATcpClientBase(LogDev logDev, int channelNo) : base(logDev, channelNo)
		{

		}

		protected virtual void RunReceive() { }

		/// <summary>
		/// connect
		/// </summary>
		/// <param name="connectionInfo"></param>
		/// <returns></returns>
		public override bool Connect(HostInfo connectionInfo)
		{
			Disconnect();

			TryToConnect(connectionInfo.Host, connectionInfo.Port);

			return true;
		}
		
		/// <summary>
		/// try to connect
		/// </summary>
		/// <param name="hostname"></param>
		/// <param name="port"></param>
		private async void TryToConnect(string hostname, int port)
		{
			ConnectionState = Defines.ConnectionStates.Connecting;
			Hostname = hostname;
			Port = port;

			bool result = await Task.Run(() =>
			{
				try
				{
					Client = new TcpClient();
					Client.Connect(hostname, port);
				}
				catch
				{
					return false;
				}

				int timeout = 1000 * 60;
				int sum = 0;
				while (sum < timeout)
				{
					if (Client.Connected)
					{
						ConnectionState = Defines.ConnectionStates.Connected;
						return true;
					}

					System.Threading.Thread.Sleep(100);
					sum += 100;
				} // end while

				return false;
			});

			if (result && ConnectionState == Defines.ConnectionStates.Connected)
			{
				tbl_log_app.Instance.Insert(LogDevice, $"connected ({hostname}:{port})");
				WriteLog($"connected ({hostname}:{port})");

				Publish(PushDataDTO.PushDataTypes.Open, $"client connected : {Client.Connected}", -1);
				
				CheckConnection();

				RunReceive();
			}
			else
			{
				Disconnect();
			}
		}

		/// <summary>
		/// check connection
		/// </summary>
		private void CheckConnection()
		{
			// check connected
			Task.Factory.StartNew(() =>
			{
				while (Client != null && Client.Client.Connected)
				{
					System.Threading.Thread.Sleep(10);
				} // end while

				Disconnect();

				Console.Out.WriteLine(">>>>>>>>>> disconnected");
			});
		}

		/// <summary>
		/// disconnect
		/// </summary>
		public override void Disconnect()
		{
			ConnectionState = Defines.ConnectionStates.Disconnected;

			try
			{
				if (Client != null)
				{
					Client.Close();
				}
			}
			catch (Exception e)
			{
				Console.Out.WriteLine(e.ToString());
			}
			finally
			{
				if (Client != null)
				{
					Client = null;
					Publish(PushDataDTO.PushDataTypes.Close, "client is disconnected", -1);
					
					tbl_log_app.Instance.Insert(LogDevice, $"disconnected ({Hostname}:{Port})");
					WriteLog($"disconnected ({Hostname}:{Port})");
				}

				Hostname = "";
				Port = 0;
			}
		}

		/// <summary>
		/// receive
		/// </summary>
		/// <param name="len"></param>
		/// <returns></returns>
		protected byte[] Receive(int len)
		{
			if (len <= 0) return null;

			try
			{
				byte[] stream = new byte[len];
				int sum = 0;
				while (sum < len)
				{
					int rnt = Client.Client.Receive(stream, sum, len - sum, SocketFlags.None);
					if (rnt <= 0) return null;

					sum += rnt;
				} // end while

				return stream;
			}
			catch (Exception e)
			{
				BaseLib.Helper.LogHelper.Debug($"{ChannelNo}", e.ToString());
				return null;
			}
		}

		/// <summary>
		/// receive byte
		/// </summary>
		/// <returns></returns>
		protected byte ReceiveByte()
		{
			byte[] stream = new byte[1];
			int rcnt = Client.Client.Receive(stream, 0, stream.Length, SocketFlags.None);
			
			return stream[0];
		}

		/// <summary>
		/// receive current buffer all
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		protected int Receive(ref byte[] buffer)
		{
			return Client.Client.Receive(buffer);
		}

		/// <summary>
		/// send buffer
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public int Send(byte[] buffer)
		{
			try
			{
				if (Client == null)
				{
					Disconnect();
					return 0;
				}	
				else
				{
					int snd = Client.Client.Send(buffer);
					return snd;
				}
			}
			catch (Exception e)
			{
				Console.Out.WriteLine(e.ToString());
				Disconnect();
				return 0;
			}
		}

		#endregion
	}
}
