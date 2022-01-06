using BaseLib.Helper;
using BaseLib.Pubsub;
using RelayBoxLib.Data.DTO;
using RelayBoxLib.Defines;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayBoxLib.Core
{
	public class Core : I7565H1Lib.Core.AI7565H1
	{
		#region variables

		public const byte MODE = 1;
		public const byte RTR = 0;
		public const byte DLC = 8;

		private const int TEST_TIMEOUT = 1000 * 10;

		private const int TEST_TICK = 10;

		#endregion

		#region single-ton

		private static Core m_Instance;
		public static Core Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new Core();
					m_Instance.Init();
				}
				return m_Instance;
			}
		}

		private Core() : base(typeof(I7565H1Lib.Core.VCI_SDK_FOR_RELAY), LogDev.RELAY_BOX, 1)
		{

		}

		private void Init()
		{
			CommCount = 1;
			CurrentInfo = new RelayBoxInformationDTO(new byte[8]);
		}

		#endregion

		#region property

		public byte CommCount { get; set; }

		public RelayBoxInformationDTO CurrentInfo { get; set; }

        private bool isLogWrite = true;
        public bool IsLogWrite {
            get {
                return isLogWrite;
            }
            set
            { 
                isLogWrite = value; 
            }
        } 
		#endregion

		#region method

		public override bool Connect(string portName)
		{
			bool result = OpenCAN(portName, 250000);
			if (result)
			{
				Task.Run(() =>
				{
					while (ConnectionState == BaseLib.Defines.ConnectionStates.Connected)
					{
						if (GetRXMsgCnt(1) > 0)
						{
							RecvCANMsg(1);
							System.Threading.Thread.Sleep(10);
						}
						else
						{
							System.Threading.Thread.Sleep(10);
						}
					} // end while

					ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;
				});
			}
			return result;
		}

		public override void Disconnect()
		{
			CloseCAN();
		}

		private void SendCANMsg(byte CANNo, uint CANId, byte[] data)
		{
			WriteLog($"[Send] CANID: 0x{string.Format("{0:X}", CANId)}, {StringHelper.ByteArrayToHexString(data)}");
			SendCANMsg(CANNo, MODE, RTR, DLC, CANId, data);
		}

		public void RecvCANMsg(byte CANNo)
		{
			byte mode = 0;
			byte rtr = 0;
			byte dlc = 0;
			uint canId = 0;
			uint timeL = 0;
			uint timeH = 0;

			byte[] data = base.RecvCANMsg(CANNo, ref mode, ref rtr, ref dlc, ref canId, ref timeL, ref timeH);
			if (data == null)
			{
				Disconnect();
				return;
			}

			if (canId == 0x00)
			{
                WriteLog($"[Relay][Recv] CANID is 0x00");
                Disconnect();
				return;
			}


            if ( IsLogWrite )
            {
                WriteLog($"[Recv] CANID: 0x{string.Format("{0:X}", canId)}, {StringHelper.ByteArrayToHexString(data)}");
            }

			switch (canId)
			{
				case 0x18A1EFF3:
					CurrentInfo = new RelayBoxInformationDTO(data);
					Publish(PushDataDTO.PushDataTypes.Data, data);
					break;
				default:
					uint DL = (uint)data[0];
					uint DH = (uint)data[4];

                    break;
			} // end switch

		}

		public bool SetMode(byte channelNo, RelayBoxModes mode)
		{
			uint canId = 0x0CFA00D0;

			byte[] data = new byte[8];
			for (int i = 0; i < data.Length; ++i)
			{
				data[i] = 0;
			}
			data[7] = CommCount++;

			if (channelNo == 1)
			{
				data[0] = (byte)mode;
				if (CurrentInfo.Ch2RelayP) data[1] = (byte)RelayBoxModes.Plus;
				else if (CurrentInfo.Ch2RelayM) data[1] = (byte)RelayBoxModes.Minus;
				else data[1] = (byte)RelayBoxModes.Off;
			}
			else
			{
				if (CurrentInfo.Ch1RelayP) data[0] = (byte)RelayBoxModes.Plus;
				else if (CurrentInfo.Ch1RelayM) data[0] = (byte)RelayBoxModes.Minus;
				else data[0] = (byte)RelayBoxModes.Off;
				data[1] = (byte)mode;
			}

			SendCANMsg(1, canId, data);

			int timeout = TEST_TIMEOUT;
			while (timeout > 0)
			{
				if (CurrentInfo.GetCurrentMode(channelNo) == mode)
				{
					return true;
				}

				System.Threading.Thread.Sleep(TEST_TICK);
				timeout -= TEST_TICK;
			}
			return false;
		}

		bool m_IsIsolationCheckRunning1 = false;
		bool m_IsIsolationCheckRunning2 = false;

		public bool IsIsolationCheckRunning(int channelNo)
        {
            BaseLib.Helper.LogHelper.Debug($"0", $"channelNo = {channelNo}, IsIsolationCheckRunning()");
            if (channelNo == 1)
            {
                BaseLib.Helper.LogHelper.Debug($"0", $" = {channelNo}, m_IsIsolationCheckRunning1 = {m_IsIsolationCheckRunning1}");
                return m_IsIsolationCheckRunning1;
			}
			else
            {
                BaseLib.Helper.LogHelper.Debug($"0", $" = {channelNo}, m_IsIsolationCheckRunning2 = {m_IsIsolationCheckRunning2}");
                return m_IsIsolationCheckRunning2;
			}
		}
		public void SetIsolationCheckRunning(int channelNo, bool value)
		{
            BaseLib.Helper.LogHelper.Debug($"0", $" = {channelNo}, SetIsolationCheckRunning()");
            if (channelNo == 1)
			{
                m_IsIsolationCheckRunning1 = value;
                BaseLib.Helper.LogHelper.Debug($"0", $" = {channelNo}, m_IsIsolationCheckRunning1 = {m_IsIsolationCheckRunning1}");
            }
			else
			{
                m_IsIsolationCheckRunning2 = value;
                BaseLib.Helper.LogHelper.Debug($"0", $" = {channelNo}, m_IsIsolationCheckRunning1 = {m_IsIsolationCheckRunning2}");
            }
		}
		#endregion
	}
}
