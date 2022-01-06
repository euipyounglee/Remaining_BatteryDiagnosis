using BaseLib.Pubsub;
using SerialPortLib;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZebraScannerLib.Core
{
	public class Core : ASerialPortBase<string>
	{
		#region property

		#endregion

		#region method

		public Core(int channelNo) : base(LogDev.DS3678, channelNo)
		{

		}

		public override bool Connect(string comPort)
		{
			bool result = Open(comPort, 9600, 8, StopBits.One, Parity.None);
			Publish(PushDataDTO.PushDataTypes.Open, $"open {comPort} {(ConnectionState == BaseLib.Defines.ConnectionStates.Connected ? "success" : "failure")}", -1);
			WriteLog($"open {comPort} {(ConnectionState == BaseLib.Defines.ConnectionStates.Connected ? "success" : "failure")}");
			return result;
		}
		
		public override void Disconnect()
		{
			ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;
			Publish(PushDataDTO.PushDataTypes.Close, "Closed", -1);
			WriteLog("Closed");
		}

		public override void OnSerialDataReceivedEvent(List<byte> stream)
		{
			string data = System.Text.Encoding.Default.GetString(stream.ToArray<byte>()).Trim('\0');
			data = data.Replace("\r\n", "");
			if (data.Length > 0)
			{
				Publish(PushDataDTO.PushDataTypes.Data, data, -1);
				WriteLog($"[Recv] {data}");
			}
		}

		#endregion
	}
}
