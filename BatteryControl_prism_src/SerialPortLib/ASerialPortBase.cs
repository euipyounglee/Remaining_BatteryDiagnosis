using BaseLib.Net;
using BaseLib.Pubsub;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SerialPortLib
{
	public abstract class ASerialPortBase<T> : BaseLib.Pubsub.ASubscribableBase<T>, ISerialPortBase
	{
		#region property

		private SerialPort SP { get; set; }

		#endregion

		#region method

		public ASerialPortBase(LogDev logDev, int channelNo) : base(logDev, channelNo)
		{
			SP = new SerialPort();
		}

		protected bool Open(string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity)
		{
			Close();

			ConnectionState = BaseLib.Defines.ConnectionStates.Connecting;

			try
			{
				SP = new SerialPort();
				SP.PortName = portName;
				SP.BaudRate = baudRate;
				SP.DataBits = dataBits;
				SP.StopBits = stopBits;
				SP.Parity = parity;

				SP.Open();
				SP.DataReceived += new SerialDataReceivedEventHandler(HandleSerialDataReceivedEvent);

				Console.Out.WriteLine("Open Success");

				ConnectionState = BaseLib.Defines.ConnectionStates.Connected;

				return true;
			}
			catch (UnauthorizedAccessException e)
			{
				MessageBox.Show(e.Message);

				Console.Out.WriteLine("Open failure : " + e.Message);
				ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;
				return false;
			}
			catch (Exception e)
			{
				Console.Out.WriteLine("Open failure : " + e.Message);
				ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;
				return false;
			}
		}

		protected void Close()
		{
			if (SP != null)
			{
				SP.DataReceived -= new SerialDataReceivedEventHandler(HandleSerialDataReceivedEvent);
				SP.Close();
				SP = null;
			}

			Console.Out.WriteLine("Close Success");

			ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;
		}

		public void WriteLine(string text)
		{
			SP.WriteLine(text);
		}

		private void HandleSerialDataReceivedEvent(object sender, SerialDataReceivedEventArgs e)
		{
			List<byte> stream = new List<byte>();
			while (SP.BytesToRead > 0)
			{
				stream.Add((byte)SP.ReadByte());
			}
			OnSerialDataReceivedEvent(stream);
		}

		public virtual void OnSerialDataReceivedEvent(List<byte> stream) { }
		
		#endregion
	}
}
