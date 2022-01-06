using BaseLib.Pubsub;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultimeterLib.Core
{
	public class M34461A : BaseLib.Net.ATcpClientBase
	{
		#region method

		public M34461A(int channelNo) : base(LogDev.MULTIMETER, channelNo)
		{

		}

		/// <summary>
		/// communication
		/// </summary>
		/// <param name="cmd"></param>
		/// <returns></returns>
		public string RunCommand(string cmd)
		{
			int cnt = Send(System.Text.Encoding.ASCII.GetBytes($"{cmd}\n"));
			if (cnt <= 0)
			{
				Disconnect();
				return "";
			}

			if (!cmd.Contains("?"))
			{
				return "";
			}

			byte[] buffer = new byte[1024];
			string result = "";

			while (ConnectionState == BaseLib.Defines.ConnectionStates.Connected)
			{
				cnt = Receive(ref buffer);
				if (cnt <= 0)
				{
					Disconnect();
					return "";
				}

				result += System.Text.Encoding.ASCII.GetString(buffer, 0, cnt);

				if (buffer[cnt - 1] == 0x0A)
				{
					break;
				}
			} // end while

			result = result.Replace("\n", "");
			Publish(PushDataDTO.PushDataTypes.Data, result, -1);

			return result;
		}

		public void SetInit()
		{
			string strCmd = "*IDN?";
			RunCommand(strCmd);
		}

		public void GetVoltage()
		{
			string strCmd = "MEAS:VOLT?";
			RunCommand(strCmd);
		}

		public void GetCurrent()
		{
			string strCmd = "MEAS:CURR?";
			RunCommand(strCmd);
		}

		#endregion
	}
}
