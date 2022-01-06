using BaseLib.Helper;
using BaseLib.Pubsub;
using SerialPortLib;
using SQLManager.Data;
using SQLManager.Defines;
using ST5520Lib.Data.DTO;
using ST5520Lib.Defines;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST5520Lib.Core
{
	public class Core : ASerialPortBase<string>
	{
		#region constructor

		public Core(LogDev logDev, int channelNo) : base(logDev, channelNo)
		{
			SendQueue = new Queue<QueryCommand>();
			ResponseQueue = new Queue<string>();
			ReceivedData = new List<byte>();
		}

		#endregion

		#region property

		private const int TEST_TIMEOUT = 1000 * 10;

		private const int TEST_TICK = 10;

        private const int TEST_OHM_LIMIT = 1000 * 1000 * 50;        // 50M Ohm


        private List<byte> ReceivedData { get; set; }

		private Queue<QueryCommand> SendQueue { get; set; }

		private Queue<string> ResponseQueue { get; set; }

		#endregion

		#region method


		public void ClearBuffer()
		{
			ReceivedData.Clear();
		}


		public override bool Connect(string comPort)
		{
			SendQueue.Clear();
			ResponseQueue.Clear();

			bool result = Open(comPort, 9600, 8, StopBits.One, Parity.None);
			Publish(PushDataDTO.PushDataTypes.Open, $"open {comPort} {(ConnectionState == BaseLib.Defines.ConnectionStates.Connected ? "success" : "failure")}");
			WriteLog($"open {comPort} {(ConnectionState == BaseLib.Defines.ConnectionStates.Connected ? "success" : "failure")}");
			return result;
		}
		

		public override void Disconnect()
		{
			SendQueue.Clear();
			ResponseQueue.Clear();

			ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;
			Publish(PushDataDTO.PushDataTypes.Close, "Closed");
			WriteLog("Closed");
		}


		public override void OnSerialDataReceivedEvent(List<byte> stream)
		{
			ReceivedData = ReceivedData.Concat(stream).ToList();

			if (ReceivedData.Count < 2) return;

			if (ReceivedData.ElementAt(ReceivedData.Count - 2) == 0x0D && ReceivedData.ElementAt(ReceivedData.Count - 1) == 0x0A)
			{
				string data = System.Text.Encoding.Default.GetString(ReceivedData.ToArray<byte>()).Trim('\0');
				data = data.Replace("\r\n", "");

				WriteLog($"[Recv] {data}");

				if (SendQueue.Count > 0)
				{
					ResponseQueue.Enqueue(data);
				}
				ClearBuffer();
			} // end if
		}


		public async Task<bool> SendNonQueryCommand(NonQueryCommand cmd)
		{
			string sendcmd = StringEnum.GetStringValue(cmd);
			WriteLog($"[Send] {sendcmd}");
			WriteLine(sendcmd);

			await Task.Delay(1000);

			return true;
		}


		public string SendQueryCommand(QueryCommand cmd)
		{
			SendQueue.Enqueue(cmd);

			string sendcmd = StringEnum.GetStringValue(cmd);
			WriteLog($"[Send] {sendcmd}");
			WriteLine(sendcmd);

			int timeout = 1000 * 10;
			while (timeout > 0)
			{
				if (ResponseQueue.Count > 0)
				{
					var dto = new ResponseDTO(SendQueue.Dequeue(), ResponseQueue.Dequeue());
					Publish(PushDataDTO.PushDataTypes.Data, dto);
					WriteLog($"{dto.Query} -> {dto.Data}");
					return dto.Data;
				}
				else
				{
					timeout -= 10;
					System.Threading.Thread.Sleep(10);
				}
			} // end while

			return null;
		}


		public async Task<bool> Measure(bool isOverflow)
		{
			return await Task.Factory.StartNew(() =>
			{
                try
                {
                    int timeout = TEST_TIMEOUT;
                    decimal value = 0;
                    while (timeout > 0)
                    {
                        var queryResult = SendQueryCommand(ST5520Lib.Defines.QueryCommand.Measure);
                        if (queryResult != null)
                        {
                            value = ToOhm(queryResult);
                            WriteLog($"ST5520 Measure -> {value}, isOverflow = {isOverflow}");

                            if (value == -1)
                            {
                                return false;
                            }
                            else if (value > 0)
                            {
                                if (isOverflow && value > TEST_OHM_LIMIT)
                                {
                                    return true;
                                }
                                else if (!isOverflow && value < TEST_OHM_LIMIT)
                                {
                                    return true;
                                }
                            }
                        }

                        System.Threading.Thread.Sleep(TEST_TICK);
                        timeout -= TEST_TICK;
                    } // end while
                }
                catch (Exception e)
                {
                    WriteLog($"ST5520 Exception -> {e}");
                    Console.Out.WriteLine(e.ToString());
                }
                return false;
			});
		}


		public decimal ToOhm(string value)
		{
            decimal number = -1;
            try
            {
                number = Decimal.Parse(value, System.Globalization.NumberStyles.AllowExponent | System.Globalization.NumberStyles.AllowDecimalPoint);
                //number = Decimal.ToInt32(decValue);
                Console.WriteLine("{0} --> {1}", value, number);
            }
            catch (OverflowException e)
            {
                Console.WriteLine("{0}: {1}", e.GetType().Name, value);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
            return number;
        }

		#endregion
	}
}
