using BaseLib.Defines;
using BaseLib.Helper;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Pubsub
{
	public abstract class ASubscribableBase<T> : ISubscribableBase<T>
	{
		#region delegate

		/// <summary>
		/// delegate of push callback
		/// </summary>
		/// <param name="dto"></param>
		public delegate void dPushCallback(PushDataDTO dto);

		#endregion

		#region property

		/// <summary>
		/// device type
		/// </summary>
		public LogDev LogDevice { get; private set; }

		/// <summary>
		/// log path
		/// </summary>
		public string LogFilePath { get; private set; }

		/// <summary>
		/// module channel no
		/// </summary>
		public int ChannelNo { get; private set; }

		/// <summary>
		/// subscribers
		/// </summary>
		private List<dPushCallback> Subscribers { get; set; }

		/// <summary>
		/// connection state
		/// </summary>
		public ConnectionStates ConnectionState { get; set; }

		#endregion

		#region method

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="logDev"></param>
		public ASubscribableBase(LogDev logDev, int channelNo)
		{
			ChannelNo = channelNo;
			LogDevice = logDev;
			LogFilePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\log\{LogDevice}";
			
			Subscribers = new List<dPushCallback>();
			ConnectionState = ConnectionStates.Disconnected;

			WriteLog($"{logDev} was created");
		}

		/// <summary>
		/// write string log
		/// </summary>
		/// <param name="msg"></param>
		public void WriteLog(string msg)
		{
			if (LogDevice == LogDev.Cycler || LogDevice == LogDev.RELAY_BOX)
			{
				LogHelper.WriteLine($@"{LogFilePath}\{LogDevice}_{string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)}.txt", msg);
			}
			else
			{
				LogHelper.WriteLine($@"{LogFilePath}\{LogDevice}_Channel{ChannelNo}_{string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)}.txt", msg);
			}

			Console.Out.WriteLine(msg);
		}

		/// <summary>
		/// write byte array to hex string log
		/// </summary>
		/// <param name="stream"></param>
		public void WriteLog(byte[] stream)
		{
			if (LogDevice == LogDev.Cycler || LogDevice == LogDev.RELAY_BOX)
			{
				LogHelper.WriteLine($@"{LogFilePath}\{LogDevice}_{string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)}.txt", stream);
			}
			else
			{
				LogHelper.WriteLine($@"{LogFilePath}\{LogDevice}_Channel{ChannelNo}_{string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)}.txt", stream);
			}
		}

		/// <summary>
		/// subscribe
		/// </summary>
		/// <param name="callback"></param>
		public void Subscribe(dPushCallback callback)
		{
			var exist = Subscribers.SingleOrDefault(p => p.Equals(callback));
			if (exist == null)
			{
				Subscribers.Add(callback);
			}
		}

		/// <summary>
		/// unsubscribe
		/// </summary>
		/// <param name="callback"></param>
		public void Unsubscribe(dPushCallback callback)
		{
			Subscribers.Remove(callback);
		}

		/// <summary>
		/// publish
		/// </summary>
		/// <param name="data"></param>
		public void Publish(PushDataDTO.PushDataTypes pushDataType, object data, int reqTaskSeq = -1, LogLevels logLevel = LogLevels.I)
		{
			var dto = new PushDataDTO(pushDataType, LogDevice, data, reqTaskSeq, logLevel);

			var list = Subscribers.ToList();
			var k = list.GetEnumerator();
			while (k.MoveNext())
			{
				try
				{
					k.Current(dto);
				}
				catch
				{
					Subscribers.Remove(k.Current);
				}
			} // end while
		}

		public virtual bool Connect(T data)
		{
			return false;
		}

		public virtual void Disconnect()
		{

		}

		#endregion
	}
}
