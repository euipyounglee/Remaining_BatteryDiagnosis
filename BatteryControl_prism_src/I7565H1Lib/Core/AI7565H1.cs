using BaseLib.Pubsub;
using I7565H1Lib.Data.DTO;
using I7565H1Lib.Defines;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCI_CAN_DotNET;

namespace I7565H1Lib.Core
{
	public abstract class AI7565H1 : BaseLib.Pubsub.ASubscribableBase<string>
	{
		#region constructor

		public AI7565H1(Type sdk, LogDev logDev, int channelNo) : base(logDev, channelNo)
		{
			SDK = Activator.CreateInstance(sdk);

			VCI_Set_MOD_Ex = GetMethod("VCI_Set_MOD_Ex");
			VCI_OpenCAN_NoStruct = GetMethod("VCI_OpenCAN_NoStruct");
			VCI_CloseCAN = GetMethod("VCI_CloseCAN");
			VCI_Get_CANStatus_NoStruct = GetMethod("VCI_Get_CANStatus_NoStruct");
			VCI_Get_MODInfo_NoStruct = GetMethod("VCI_Get_MODInfo_NoStruct");
			VCI_Clr_BufOverflowLED = GetMethod("VCI_Clr_BufOverflowLED");
			VCI_DoEvents = GetMethod("VCI_DoEvents");
			VCI_Rst_MOD = GetMethod("VCI_Rst_MOD");
			VCI_SendCANMsg_NoStruct = GetMethod("VCI_SendCANMsg_NoStruct");
			VCI_Get_RxMsgCnt = GetMethod("VCI_Get_RxMsgCnt");
			VCI_RecvCANMsg_NoStruct = GetMethod("VCI_RecvCANMsg_NoStruct");
		}

		#endregion

		#region property

		private object SDK { get; set; }

		private byte DevPort { get; set; }

		private System.Reflection.MethodInfo VCI_Set_MOD_Ex { get; set; }
		private System.Reflection.MethodInfo VCI_OpenCAN_NoStruct { get; set; }
		private System.Reflection.MethodInfo VCI_CloseCAN { get; set; }
		private System.Reflection.MethodInfo VCI_Get_CANStatus_NoStruct { get; set; }
		private System.Reflection.MethodInfo VCI_Get_MODInfo_NoStruct { get; set; }
		private System.Reflection.MethodInfo VCI_Clr_BufOverflowLED { get; set; }
		private System.Reflection.MethodInfo VCI_DoEvents { get; set; }
		private System.Reflection.MethodInfo VCI_Rst_MOD { get; set; }
		private System.Reflection.MethodInfo VCI_SendCANMsg_NoStruct { get; set; }
		private System.Reflection.MethodInfo VCI_Get_RxMsgCnt { get; set; }
		private System.Reflection.MethodInfo VCI_RecvCANMsg_NoStruct { get; set; }

		#endregion

		#region method

		private System.Reflection.MethodInfo GetMethod(string name)
		{
			return SDK.GetType().GetMethod(name);
		}

		protected bool OpenCAN(string portName, uint baud)
		{
			try
			{
				byte devPort = Convert.ToByte(portName.Replace("COM", ""));

				ConnectionState = BaseLib.Defines.ConnectionStates.Connecting;
				Publish(PushDataDTO.PushDataTypes.Open, $"opening CAN", -1);
				WriteLog($"[{LogDevice}] opening CAN : {portName}");

				byte[] Mod_CfgData = new byte[512];

				//Listen Only Mode
				Mod_CfgData[0] = 0;                     //CAN1 => 0:Disable, 1:Enable
				Mod_CfgData[1] = 0;                     //CAN2 => 0:Disable, 1:Enable
#if true
                //Test-21.11.17
                VCI_Set_MOD_Ex.Invoke(SDK, new object[] { Mod_CfgData });
#endif

                DevPort = devPort;
				int Ret = (int)VCI_OpenCAN_NoStruct.Invoke(SDK, new object[] { devPort, (byte)DevTypes.I_7565_H1, baud, baud });
				if (Ret != 0)
				{
					ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;
					Publish(PushDataDTO.PushDataTypes.Open, $"OpenCAN failure {Constants.GetErrorMessage(Ret)}", -1, LogLevels.E);
					WriteLog($"[{LogDevice}] OpenCAN failure {Constants.GetErrorMessage(Ret)}");

					return false;
				}
				else
				{
					ConnectionState = BaseLib.Defines.ConnectionStates.Connected;
					Publish(PushDataDTO.PushDataTypes.Open, $"OpenCAN Success", -1);
					WriteLog($"[{LogDevice}] OpenCAN Success");

					return true;
				}
			}
			catch (Exception e)
			{
				ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;
				Console.Out.WriteLine(e.ToString());

				return false;
			}
		}

		protected void CloseCAN()
		{
			WriteLog($"[{LogDevice}] CloseCAN : {DevPort}");
			ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;

			int ret = (int)VCI_CloseCAN.Invoke(SDK, new object[] { DevPort });
			if (ret != 0)
			{
				Publish(PushDataDTO.PushDataTypes.Close, $"CloseCAN failure {Constants.GetErrorMessage(ret)}", -1, LogLevels.E);
				WriteLog($"[{LogDevice}] CloseCAN failure {Constants.GetErrorMessage(ret)}");
			}
			else
			{
				Publish(PushDataDTO.PushDataTypes.Close, "CloseCAN Success", -1);
				WriteLog($"[{LogDevice}] CloseCAN Success");
			}
		}

		public CANStatusDTO GetCANStatus(byte canNo)
		{
			var dto = new CANStatusDTO();

			object[] arguments = new object[] { canNo, null, null, null, null, null };
			int ret = (int)VCI_Get_CANStatus_NoStruct.Invoke(SDK, arguments);
			uint CurCANBaud = (uint)arguments[1];
			byte CANReg = (byte)arguments[2];
			byte CANTxErrCnt = (byte)arguments[3];
			byte CANRxErrCnt = (byte)arguments[4];
			byte ModState = (byte)arguments[5];

			if (ret != 0)
			{
				Publish(PushDataDTO.PushDataTypes.Log, $"GetCANStatus {Constants.GetErrorMessage(ret)}", -1, LogLevels.E);
				WriteLog($"GetCANStatus {Constants.GetErrorMessage(ret)}");
			}
			else
			{
				dto = new CANStatusDTO
				{
					CANNo = canNo,
					CurCANBaud = CurCANBaud,
					CANReg = CANReg,
					CANTxErrCnt = CANTxErrCnt,
					CANRxErrCnt = CANRxErrCnt,
					ModState = ModState
				};

				Publish(PushDataDTO.PushDataTypes.Data, dto, -1);

				WriteLog($"[CAN{canNo}_Status] " +
					$"Baudrate: {string.Format("{0:0000.000}", CurCANBaud / 1000)} (Kbps), " +
					$"Register: {string.Format("0x{0:X2}", CANReg)}, " +
					$"TxErrCnt: {string.Format("0x{0:X2}", CANTxErrCnt)}, " +
					$"RxErrCnt: {string.Format("0x{0:X2}", CANRxErrCnt)}, " +
					$"ModState: {string.Format("0x{0:X2}", ModState)}"
				);
			} // end if

			return dto;
		}

		public ModInfoDTO GetModInfo()
		{
			var dto = new ModInfoDTO();

			char[] ModID = new char[12];
			char[] FWVer = new char[12];
			char[] HWSN = new char[16];

			int ret = (int)VCI_Get_MODInfo_NoStruct.Invoke(SDK, new object[] { ModID, FWVer, HWSN });
			if (ret != 0)
			{
				Publish(PushDataDTO.PushDataTypes.Log, $"GetModInfo {Constants.GetErrorMessage(ret)}", -1, LogLevels.E);
				WriteLog($"GetModInfo {Constants.GetErrorMessage(ret)}");
			}
			else
			{
				dto = new ModInfoDTO(new string(ModID).Trim('\0'), new string(FWVer).Trim('\0'), new string(HWSN).Trim('\0'));

				Publish(PushDataDTO.PushDataTypes.Data, dto, -1);

				WriteLog($"ModName= {dto.ModID}, FW_Ver= {dto.FWVer}, HW_SN= {dto.HWSN}");
			}

			return dto;
		}

		public void ClearErrorLED(byte canNo)
		{
			int ret = (int)VCI_Clr_BufOverflowLED.Invoke(SDK, new object[] { canNo });
			if (ret != 0)
			{
				Console.Out.WriteLine(Constants.GetErrorMessage(ret));
			}
			else
			{
				Console.Out.WriteLine($"Clear CAN{canNo} ErrLED OK");
			}
		}

		public void ResetModule()
		{
			Console.Out.WriteLine("Waiting for Reset Module OK ...");

			VCI_DoEvents.Invoke(SDK, new object[] { });
			int ret = (int)VCI_Rst_MOD.Invoke(SDK, new object[] { });
			if (ret == 10)
			{
				Console.Out.WriteLine("Module Reset OK");
			}
			else
			{
				Console.Out.WriteLine(Constants.GetErrorMessage(ret));
			}

			CloseCAN();
		}

		protected void SendCANMsg(byte CANNo, byte mode, byte rtr, byte dlc, uint CANId, byte[] data)
		{
			int ret = (int)VCI_SendCANMsg_NoStruct.Invoke(SDK, new object[] { CANNo, mode, rtr, dlc, CANId, data });
			if (ret != 0)
			{
				Console.Out.WriteLine(Constants.GetErrorMessage(ret));
			}
			else
			{
				Console.Out.WriteLine($"Send CAN {CANNo} Msg OK");

			}
		}

		public uint GetRXMsgCnt(byte canNo)
		{
			object[] arguments = new object[] { canNo, null };
			int ret = (int)VCI_Get_RxMsgCnt.Invoke(SDK, arguments);
			uint RxMsgCnt = (uint)arguments[1];

			if (ret != 0)
			{
				Console.Out.WriteLine(Constants.GetErrorMessage(ret));
			}
			else
			{
				//Console.Out.WriteLine($"CAN{CANNo} RxMsgCnt : {RxMsgCnt}");
			}

			return RxMsgCnt;
		}

		protected byte[] RecvCANMsg(byte CANNo, ref byte mode, ref byte rtr, ref byte dlc, ref uint canId, ref uint timeL, ref uint timeH)
		{
			byte[] data = new byte[8];

			object[] arguments = new object[] { CANNo, null, null, null, null, null, null, data };
			int ret = (int)VCI_RecvCANMsg_NoStruct.Invoke(SDK, arguments);
			mode = (byte)arguments[1];
			rtr = (byte)arguments[2];
			dlc = (byte)arguments[3];
			canId = (uint)arguments[4];
			timeL = (uint)arguments[5];
			timeH = (uint)arguments[6];

			if (ret != 0)
			{
				Console.Out.WriteLine(Constants.GetErrorMessage(ret));
				return null;
			}
			else
			{
				return data;
			}
		}

		#endregion

	}
}
