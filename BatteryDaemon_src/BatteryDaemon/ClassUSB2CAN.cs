using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using mVCI_CAN_DotNET;
//using VxCAN_DotNET;

//using RelayBoxLib.Core;
//using BaseLib.Helper;
using BaseLib.Helper;
using SharedLib;
using SharedLib.Core;
using RelayBoxLib.Defines;
using RelayBoxLib.Core;
using System.Runtime.InteropServices;
using VCI_CAN_DotNET;//as I7651h;
using VxCAN_DotNET;

using System.Threading;
using System.Reflection;
using System.IO;

namespace BatteryDaemon
{
   

    public static class CAN_VCII765H
    {

        public static int openCAN(byte devPort, byte devtype, uint CAN1_baud,uint CAN2_baud)
        {
            int nResult = VCI_SDK.VCI_OpenCAN_NoStruct(devPort, devtype, CAN1_baud, CAN2_baud);

            return nResult;
        }

        public static int sendCANMsg(byte CANNo, byte mode, byte rtr, byte dlc, uint CANId, byte[] data)
        {
            int ret = 0;
            //	# Plus
             ret = VCI_SDK.VCI_SendCANMsg_NoStruct(CANNo, mode, rtr, dlc, CANId, data);

            return ret;

        }

        public static ulong ver()
        {
            ulong ret = 0;
            ret = VCI_SDK.VCI_Get_DllVer();
            return ret;
        }
        public static int recvCANMsg(byte CANNo,ref byte mode, ref byte rtr,ref byte dlc, ref uint CANId,ref uint TimeL,ref uint TimeH, byte[] data)
        {
            int ret = 0;
            ret = VCI_SDK.VCI_RecvCANMsg_NoStruct(CANNo, ref mode,ref rtr, ref dlc, ref CANId, ref TimeL, ref TimeH, data);
            return ret;
        }

        public static int closeCAN(byte devPort)
        {
            int ret = 0;
            ret = VCI_SDK.VCI_CloseCAN(devPort);
            return ret;
        }

    }


    public static class CAN_VxCII765H
    {

        public static uint Ver(char[] ver)
        {
            uint  ret = 0;
           
            ret = VxCAN_DotNET.VxCAN_DotNET.VxCAN_DLL_Ver(ver);
            return ret;
        }



        public static int Open(byte devPort,  uint dw_baudRate)
        {
            
            uint nResult = 0;
            nResult = VxCAN_DotNET.VxCAN_DotNET.VxCAN_OpenCAN(devPort, dw_baudRate);

            return (int)nResult;
        }

        public static int Send(byte devPort, uint CANId, byte mode, byte rtr, byte len_dlc,  byte[] data)
        {
            uint nResult = 0;
            nResult = VxCAN_DotNET.VxCAN_DotNET.VxCAN_Send(devPort, CANId, mode, rtr, len_dlc, data);
            return (int)nResult;
        }

        public static int Send_Log(byte devPort, uint CANId, byte mode, byte rtr, byte len_dlc, byte[] data, string logfilePath)
        {
            uint nResult = 0;
            nResult = VxCAN_DotNET.VxCAN_DotNET.VxCAN_Send_And_Log(devPort, CANId, mode, rtr, len_dlc, data, logfilePath);
            return (int)nResult;
        }


        public static int Receive(byte devPort,ref uint CANId,ref byte mode, ref byte rtr,ref byte len_dlc, byte[] data, ref double messageTime)
        {
            uint nResult = 0;
            nResult = VxCAN_DotNET.VxCAN_DotNET.VxCAN_Receive(devPort, ref CANId, ref mode, ref rtr, ref len_dlc, data, ref messageTime);
            return (int)nResult;
        }
        public static int Receive_Log(byte devPort, ref uint CANId, ref byte mode, ref byte rtr, ref byte len_dlc, byte[] data, ref double messageTime,string logfilePath)
        {
            uint nResult = 0;
            nResult = VxCAN_DotNET.VxCAN_DotNET.VxCAN_Receive_And_Log(devPort, ref CANId, ref mode, ref rtr, ref len_dlc, data, ref messageTime, logfilePath);
            return (int)nResult;
        }

      
        public static int Close(byte devPort)
        {
            uint nResult = 0;
            nResult = VxCAN_DotNET.VxCAN_DotNET.VxCAN_CloseCAN(devPort);//.HW_UARTCAN_COMPort_ComPortNotOpen(.VxCAN_OpenCAN(devPort, dw_baudRate);

            return (int)nResult;
        }

        public static uint GetError(uint error , char[] errDesc )
        {
            uint nResult = 0;
            nResult = VxCAN_DotNET.VxCAN_DotNET.VxCAN_GetErrorString(error, errDesc);
            return nResult;
        }

    }


   

    class ClassUSB2CAN //: I7565H1Lib.Core.AI7565H1
    {

        RelayBoxLib.Core.Core relayCore = null;
        const int MODNUM = 2;

        ////Declare multi-module object for I-7565-H1/H2


        //[1] Global Functions
        string[] ErrMsg =
        {
            "No_Err",               "DEV_ModName_Err",          "DEV_ModNotExist_Err",
            "DEV_PortNotExist_Err", "DEV_PortInUse_Err",        "DEV_PortNotOpen_Err",
            "CAN_ConfigFail_Err",   "CAN_HARDWARE_Err",         "CAN_PortNo_Err",
            "CAN_FIDLength_Err",    "CAN_DevDisconnect_Err",    "CAN_TimeOut_Err",
            "CAN_ConfigCmd_Err",    "CAN_ConfigBusy_Err",       "CAN_RxBufEmpty",
            "CAN_TxBufFull"
        };



        public enum DevTypes
        {
            I_7565_H1 = 1,
            I_7565_H2 = 2
        }

        public ClassUSB2CAN()
        {

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            path = Path.Combine(path, IntPtr.Size == 8 ? "x64" : "x86");

            BaseLib.Helper.LogHelper.log4net_DebugWrite("USB2CAN");//.log4net_Init();

            //var a = SharedPreferences.Instance;
            relayCore = new RelayBoxLib.Core.Core();
        }

        public string connect_1(string strCOM)
        {
            //  I7651h.VCI_Set_MOD_Ex();
            byte devPort = Convert.ToByte(strCOM.Replace("COM", ""));


            byte[] Mod_CfgData = new byte[512];

            //Listen Only Mode
            Mod_CfgData[0] = 0;                     //CAN1 => 0:Disable, 1:Enable
            Mod_CfgData[1] = 0;                     //CAN2 => 0:Disable, 1:Enable



            char[] chVer = new char[10];// ();

            ulong nver = CAN_VxCII765H.Ver(chVer);

            //   int nResult = VCI_SDK.VCI_OpenCAN_NoStruct(devPort, 1, 250000, 250000);
            int nResult = CAN_VCII765H.openCAN(devPort, 1, 250000, 250000);

            byte CANNo = 1;
            byte mode = 1;
            byte rtr = 0;
            byte dlc = 8;

            uint CANId = 0xCFA00D0;
            byte[] data = new byte[] { 0, 1, 0, 0, 0, 0, 0, 1 };

            //	# Plus
           
            int ret = CAN_VxCII765H.Send(CANNo, CANId, mode, rtr, dlc,  data);

            Thread.Sleep(3000);
            data[1] = 0x02;
            data[7] = 0x02;

     
            ret = CAN_VxCII765H.Send(CANNo, CANId, mode, rtr, dlc, data);

            Thread.Sleep(3000);

            data[1] = 0x00;
            data[7] = 0x03;

          
            ret = CAN_VxCII765H.Send(CANNo, CANId, mode, rtr, dlc,  data);

            Thread.Sleep(3000);

         
            ret = CAN_VxCII765H.Close(devPort);

            return "";
        }

        public string connect(string strCOM)
        {

           
            //  I7651h.VCI_Set_MOD_Ex();
            byte devPort = Convert.ToByte(strCOM.Replace("COM", ""));


            byte[] Mod_CfgData = new byte[512];

            //Listen Only Mode
            Mod_CfgData[0] = 0;                     //CAN1 => 0:Disable, 1:Enable
            Mod_CfgData[1] = 0;                     //CAN2 => 0:Disable, 1:Enable

            // int nResult = I7651h.VCI_Set_MOD_Ex(Mod_CfgData);


            ulong ver = CAN_VCII765H.ver();

            int nResult = CAN_VCII765H.openCAN(devPort, 1, 250000, 250000);

            if (0 == nResult)
            {
                byte CANNo = 1;
                byte mode = 1;
                byte rtr = 0;
                byte dlc = 8;

                uint CANId = 0xCFA00D0;
                byte[] data = new byte[] { 0, 1, 0, 0, 0, 0, 0, 1 };

                //	# Plus
                //   int ret = VCI_SDK.VCI_SendCANMsg_NoStruct(CANNo, mode, rtr, dlc, CANId, data);// =[0, 1, 0, 0, 0, 0, 0, 1])
                int ret = CAN_VCII765H.sendCANMsg(CANNo, mode, rtr, dlc, CANId, data);

                Thread.Sleep(3000);
                data[1] = 0x02;
                data[7] = 0x02;

                //   ret = VCI_SDK.VCI_SendCANMsg_NoStruct(CANNo, mode, rtr, dlc, CANId, data);//=[0, 2, 0, 0, 0, 0, 0, 2])
                ret = CAN_VCII765H.sendCANMsg(CANNo, mode, rtr, dlc, CANId, data);

                Thread.Sleep(3000);

                data[1] = 0x00;
                data[7] = 0x03;

                // ret = VCI_SDK.VCI_SendCANMsg_NoStruct(CANNo, mode, rtr, dlc, CANId, data);//=[0, 2, 0, 0, 0, 0, 0, 2])
                ret = CAN_VCII765H.sendCANMsg(CANNo, mode, rtr, dlc, CANId, data);

                Thread.Sleep(3000);

                //  ret = VCI_SDK.VCI_CloseCAN(devPort);
                ret = CAN_VCII765H.closeCAN(devPort);
            }

            return "";

        }

        public string connect_ok(string strCOM)
        {
            string strResult = "";



          //  RelayBoxLib.Core.Core relayCore = null;
            bool bConnectState = false;

            try
            {



                bConnectState = relayCore.connect(strCOM);

                if (bConnectState)
                {
                     byte ChannelNum = 2;

                     int nResult = DoRelayBoxInsulationCheck(ChannelNum);

                    if (nResult == 0)
                    {
                        strResult = "RelayBox-OK";

                        relayCore.CloseCAN();
                    }
                    else
                    {

                        strResult = string.Format( "err-RelayBox:{0}",nResult);
                    }

                }
               
            }
            catch(Exception ex)
            {
                strResult = ex.Message;
                Console.WriteLine(ex.ToString());
            }


            return strResult;
        }

        private int  DoRelayBoxInsulationCheck(byte ChannelNum)
        {

           // byte ChannelNum = 2;

            if (relayCore.CurrentInfo.GetCurrentMode(ChannelNum) == RelayBoxModes.Error)
            {
                if (!relayCore.SetMode(ChannelNum, RelayBoxModes.Off))
                {
                //    return "error:RelayBoxMode.Off";
                    return -1;
                    
                }

            }
            //1. Set Pluse Mode
            if (!relayCore.SetMode(ChannelNum, RelayBoxLib.Defines.RelayBoxModes.Plus))
            {

                BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #4");
                relayCore.SetIsolationCheckRunning(ChannelNum, false);
               // return false;
                return 1;//
            }
            //2. start ST5520 test
            System.Threading.Thread.Sleep(1000 * 1);


            BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #6");

            // 3. measure (not overflow)
         //   measureResultPlus = Devices.ST5520Instance.Measure(true);


            // 4. Set Minus Mode
            if (!relayCore.SetMode(ChannelNum, RelayBoxModes.Minus))
            {
                BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #9");

                relayCore.SetIsolationCheckRunning(ChannelNum, false);
                return 2;// false;
            }

            BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #10");

            // 5. start ST5520 test
          //  result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StartTest);
            System.Threading.Thread.Sleep(1000 * 1);

            BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #11");

            // 6. measure (not overflow)
          //  measureResultMinus = Devices.ST5520Instance.Measure(true);

            BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #12");

        //    result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);

            BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #13");

            // 8. Set Off
            if (!relayCore.SetMode(ChannelNum, RelayBoxModes.Off))
            {
                BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #14");

                System.Threading.Thread.Sleep(1000 * 10);
                relayCore.SetIsolationCheckRunning(ChannelNum, false);
                return 3;// false;
            }




            return 0;

        }

        public void PortOpen_BtnFunc()
        {
            //btn_OpenCAN.Enabled = false;
            //cbo_ComPort.Enabled = false;
            //cbo2_ComPort.Enabled = false;
            //cbo_DevType.Enabled = false;
            //cbo2_DevType.Enabled = false;
            //cbo_C1Baud.Enabled = false;
            //cbo_C2Baud.Enabled = false;
            //cbo_CANNo.Enabled = true;
            //btn_SendCANMsg.Enabled = true;
            //btn_RecvCANMsg.Enabled = true;
            //btn_EnHWCycTxMsg.Enabled = true;
            //btn_DisHWCycTxMsg.Enabled = true;
            //btn_CANStatus.Enabled = true;
            //btn_ClrErrLED.Enabled = true;
            //btn_ModInfo.Enabled = true;
            //btn_RstMod.Enabled = true;
        }

        public void PortClose_BtnFunc()
        {
            //btn_OpenCAN.Enabled = true;
            //cbo_ComPort.Enabled = true;
            //cbo2_ComPort.Enabled = true;
            //cbo_DevType.Enabled = true;
            //cbo2_DevType.Enabled = true;
            //cbo_C1Baud.Enabled = true;
            //if (cbo_DevType.SelectedIndex == 0)
            //{
            //    cbo_C2Baud.Enabled = false;
            //}
            //else
            //{
            //    cbo_C2Baud.Enabled = true;
            //}
            ////cbo_CANNo.Enabled = false;
            //btn_SendCANMsg.Enabled = false;
            //btn_RecvCANMsg.Enabled = false;
            //btn_EnHWCycTxMsg.Enabled = false;
            //btn_DisHWCycTxMsg.Enabled = false;
            //btn_CANStatus.Enabled = false;
            //btn_ClrErrLED.Enabled = false;
            //btn_ModInfo.Enabled = false;
            //btn_RstMod.Enabled = false;
        }


        public void ShowErrMsg(int ErrNo)
        {
            MessageBox.Show(ErrMsg[ErrNo], "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public void Show_CmdStatus(string Res)
        {
          //  this.lbl_CmdStatus.Text = "Cmd_Status :  " + Res;
            MessageBox.Show(Res, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OpenCAN(string strCOM, uint baud)
        {
            UInt32 ret = 0;
            byte devPort = Convert.ToByte(strCOM.Replace("COM", ""));

            //ret = VxCAN_DotNET.VxCAN_DotNET.VxCAN_OpenCAN(devPort, baud);// (UInt32)VxCAN_DotNET.VxCAN_DotNET.CAN_BaudRate.BR_1000K);
            //if (ret == 0)
            //{
            //    MessageBox.Show("Open CAN Port 0 done!!");
            //}
         //   VxCAN_port = (Byte)dataGridView1.CurrentRow.Index;
        }

        private void btn_OpenCAN(string strCOM, uint baud)
        {
            int Ret = 0;
            byte[] Mod_CfgData = new byte[512];

            //Listen Only Mode
            Mod_CfgData[0] = 0;                     //CAN1 => 0:Disable, 1:Enable
            Mod_CfgData[1] = 0;                     //CAN2 => 0:Disable, 1:Enable

            try
            {

                //I7565H1H2_Mod[0].mVCI_Set_MOD_Ex(Mod_CfgData);

                  byte devPort = Convert.ToByte(strCOM.Replace("COM", ""));

                //I7565H1H2_Mod[0].mVCI_Set_MOD_Ex(Mod_CfgData);

                //Ret = I7565H1H2_Mod[0].mVCI_OpenCAN_NoStruct(devPort, (byte)DevTypes.I_7565_H1, baud, baud);


            //  int Ret = (int)VCI_OpenCAN_NoStruct.Invoke(SDK, new object[] { devPort, (byte)DevTypes.I_7565_H1, baud, baud });
             }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //    BaseLib.Helper.log.Debug("Main() started");

                BaseLib.Helper.LogHelper.log.Debug(ex.Message);// "Main() started");
                BaseLib.Helper.LogHelper.log.Debug(ex.ToString());// "Main() started");
            }
            

            if (Ret != 0)
                ShowErrMsg(Ret);
            else
            {
                Show_CmdStatus("Mod1_OpenCAN Success");
                PortOpen_BtnFunc();
            }

            //ComPort[1] = (byte)(cbo2_ComPort.SelectedIndex + 1);
            //Ret = I7565H1H2_Mod[1].mVCI_OpenCAN_NoStruct(ComPort[1], (byte)(cbo2_DevType.SelectedIndex + 1),
            //     (uint)(Convert.ToSingle(cbo_C1Baud.Text) * 1000),
            //     (uint)(Convert.ToSingle(cbo_C2Baud.Text) * 1000));
            ////Ret = VCI_SDK.VCI_OpenCAN_NoStruct((byte)(cbo_ComPort.SelectedIndex + 1), (byte)(cbo_DevType.SelectedIndex+1),
            ////    (uint)(Convert.ToSingle(cbo_C1Baud.Text) * 1000),
            ////    (uint)(Convert.ToSingle(cbo_C2Baud.Text) * 1000));
            //if (Ret != 0)
            //    ShowErrMsg(Ret);
            //else
            //{
            //    //Show_CmdStatus("Mod2_OpenCAN Success");
            //    PortOpen_BtnFunc();
            //}
        }

    }
}
