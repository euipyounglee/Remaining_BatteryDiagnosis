//using PneCtsLib.Core;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BatteryGateway.Program;

namespace BatteryGateway
{


    [StructLayout(LayoutKind.Explicit)]
    struct WORDConverter
    {
        [FieldOffset(0)]
        public uint Value;

        [FieldOffset(0)]
        public ushort LOWORD;

        [FieldOffset(2)]
        public ushort HIWORD;
    }


    //EMG CODE 구조체 생성 API의 CTS_EMG_DATA 구조체 참고.
    [StructLayout(LayoutKind.Sequential)]
    public struct CTS_EMG_DATA
    {
        [MarshalAs(UnmanagedType.I2, SizeConst = 1)]//길이 2
        public Int16 Code;		    	     /**< EMG 코드 */

        [MarshalAs(UnmanagedType.I2, SizeConst = 1)]//길이 2
        public Int16 Value;		    	     /**< EMG 값 */

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]//길이 4
        public int reserved;   		     /**< 예비 공간 */

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]//길이128
        public byte[] szName;           /**< EMG 이름 */
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DEBUG_TEST
    {
        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public int nTest;
        [MarshalAs(UnmanagedType.R4, SizeConst = 1)]
        public float fTest;
    }


    //ksj 20200728 : CTS_MD_SYSTEM_DATA 구조체 선언
    [StructLayout(LayoutKind.Sequential)]
    public struct CTS_MD_SYSTEM_DATA
    {
        [MarshalAs(UnmanagedType.U4, SizeConst = 1)]//길이 4
        public UInt32 nModuleID;	   	     /**< 모듈 ID */

        [MarshalAs(UnmanagedType.U4, SizeConst = 1)]//길이 4
        public UInt32 nSystemType;

        [MarshalAs(UnmanagedType.U4, SizeConst = 1)]//길이 4
        public UInt32 nProtocolVersion;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]//길이128
        public byte[] szModelName;

        [MarshalAs(UnmanagedType.U4, SizeConst = 1)]//길이 4
        public UInt32 nOSVersion;

        [MarshalAs(UnmanagedType.U2, SizeConst = 1)]//길이 2
        public UInt16 wVoltageRange;

        [MarshalAs(UnmanagedType.U2, SizeConst = 1)]//길이 2
        public UInt16 wCurrentRange;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]//길이 4 * 5 = 20
        public UInt32[] nVoltageSpec;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]//길이 4 * 5 = 20
        public UInt32[] nCurrentSpec;

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]//길이 1
        public byte byCanCommType;

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]//길이 1
        public byte byTypeData;

        [MarshalAs(UnmanagedType.U2, SizeConst = 1)]//길이 2
        public UInt16 wInstalledBoard;

        [MarshalAs(UnmanagedType.U2, SizeConst = 1)]//길이 2
        public UInt16 wChannelPerBoard;

        [MarshalAs(UnmanagedType.U4, SizeConst = 1)]//길이 4
        public UInt32 nInstalledChCount;

        [MarshalAs(UnmanagedType.U4, SizeConst = 1)]//길이 4
        public UInt32 nTotalJigNo;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]//길이 4 * 16 = 64
        public UInt32[] awBdinJig;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]//길이 4 * 4 = 16
        public UInt32[] reserved;

    }



    enum PS_STEP
    {
        PS_STEP_NONE = 0, //Idle
        PS_STEP_CHARGE,  //충전
        PS_STEP_DISCHARGE,//방전
        PS_STEP_REST,//휴지
        PS_STEP_OCV, //OCV
        PS_STEP_IMPEDANCE,//임피던스
        PS_STEP_END,//완료
        PS_STEP_ADV_CYCLE,//작업중
        PS_STEP_LOOP,//작업중
        PS_STEP_PATTERN//Pattern

    }


    enum rRET
    {
        CTS_NACK				=	0,	/*!< 오류 (Error) */
        CTS_ACK					=	1,	/*!< 정상 (Normal) */
        CTS_TIMEOUT				=	2,	/*!< 시간초과 (Time over) */
        CTS_SIZE_MISMATCH		=	3,	/*!< Body Size 불일치 (Size mismatch) */
        CTS_RX_BUFF_OVER_FLOW	=	4,	/*!< 수신 버퍼 오버플로우 (Receive buffer overflow) */
        CTS_TX_BUFF_OVER_FLOW	=	5,	/*!< 송신 버퍼 오버플로우 (Transmit buffer overflow) */
        CTS_CONNECTED			=	6,	/*!< 접속 성공 (Connection success) */
        CTS_NOT_CONNECTED		=	7,	/*!< 접속 되어 있지 않음 (Not connected) */
        CTS_FILE_NOT_EXIST		=	8,	/*!< 해당 경로에 파일이 없음 (Not exist file) */
        CTS_FILE_OPENED			=	9,	/*!< 파일이 Open되어 있음 (File open fail) */
        CTS_CAN_LIN_TYPE_INVALID	=10,//	/*!< CAN Rx/Tx, LIN File Type이 맞지 않음 (Type mismatch) */
        CTS_CHANNEL_RUN			=	11,	/*!< 현재 동작중인 채널임 (Channel is running) */
        CTS_FAIL				= CTS_CHANNEL_RUN +1 // 0xFFFFFFFF	/*!< 실패 (Failure) */
    }

    

    [StructLayout(LayoutKind.Sequential)]
    public struct CTS_SIMPLE_TEST_INFO
    {
        // 시험 조건
        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nStepType;             //   스텝. Charge:1, Discharge:2, Rest:3, OCV:4  

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]//길이 4
        public Int32 nMode;             //   모드. CCCV:1, CC:2 CV:3 CP:6   

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nRefVoltage;             // <  설정 전압 mV   

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nRefCurrent;             //   설정 전압 mA   

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nRefPower;             //   설정 파워 mW 

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nRecordTime;               //  기록 시간 (1/10초) 예) 1초:10 , 20초:200, 0.1초: 1 


        //종료 조건
        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nCutoffCondTime;            //  종료 시간 (초) 

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nCutoffCondVolt;            //  종료 전압 mV 

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nCutoffCondCurrent;         //  종료 전류 mA 

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nCutoffCondAh;              //  종료 용량 mAh 


        //안전 조건
        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nSafetyVoltageHigh;         //  전압 상한 mV 

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nSafetyVoltageLow;          //  전압 하한 mV 

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nSafetyCurrentHigh;         //  전류 상한 mA 

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nSafetyCurrentLow;          //  전류 하한 mA 

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nSafetyAhHigh;              //  용량 상한 mAh 

        [MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public Int32 nSafetyAhLow;               //  용량 하한 mAh 


        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]//길이 = 12
        public Int32[] nReserved;                   // 미사용


    }



    //======================================================================

    public class ClassPneCtsLib
    {


#if x64
        private const string STR_DllNAME = "PSServerAPI64.dll";
#else
        private const  string STR_DllNAME = "PSServerAPI.dll";
#endif



        // 디버깅용 Test API 정의 ////////////////////////////////////////////////////////////////////////////////
        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern IntPtr ctsApiDebugPtrStruct(ref DEBUG_TEST debug1);

        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern DEBUG_TEST ctsApiDebugStruct(DEBUG_TEST stEmg); //API 함수 정의

        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern int ctsApiDebugInteger(int nA); //API 함수 정의

        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern StringBuilder ctsApiDebugString(char[] szString);

        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern IntPtr ctsApiDebugPtrStruct2(ref CTS_EMG_DATA debug1);

        // ctsAPI 정의 ////////////////////////////////////////////////////////////////////////////////
        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern int ctsServerCreate(int nInstalledModuleNo, IntPtr hWnd);

        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern int ctsServerStart();

        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern int ctsServerClose();

        // CallBack API 정의
        [DllImport(STR_DllNAME, CallingConvention = CallingConvention.StdCall)] //PSServerAPI.DLL 링크
        public static extern int CallbackConnected(CALLBACK_CONNECTED callback);
        public delegate int CALLBACK_CONNECTED(int nModuleID, ref CTS_MD_SYSTEM_DATA sysinfo); //ksj 20200728 : callback 함수 처리를 위한 대리자 deleagate 선언.


        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern int ctsSendSimpleTest(uint nModuleID, int nBitLCh, int nBitHCh, int nStepCount, CTS_SIMPLE_TEST_INFO[] pStepInfo);

        //////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern void ctsSetLogPath(string szLogPath);

        [DllImport("Kernel32.dll")]
        private static extern IntPtr  GetConsoleWindow();


//        public delegate void dCallbackChData(UInt32 nModIDandChIdex, ref CTS_VARIABLE_CH_DATA ChData);


        // CallBack API 정의
        [DllImport(STR_DllNAME, CallingConvention = CallingConvention.StdCall)] //PSServerAPI.DLL 링크
        public static extern void CallbackChData(CALLBACK_BACKGETCHDATA handler);
        public delegate void CALLBACK_BACKGETCHDATA(uint nModIDandChIdex, ref CTS_VARIABLE_CH_DATA ChData); // : callback 함수 처리를 위한 대리자 deleagate 선언.





        ScriptScope _scope;
        string g_strPNEPath;

        public ClassPneCtsLib(ScriptScope scope)
        {
            _scope = scope;

            CALLBACK_CONNECTED CB_Connected = new CALLBACK_CONNECTED(ctsConnected); //connect  콜백
            CallbackConnected(CB_Connected);


#if true
            CALLBACK_BACKGETCHDATA CB_BackGetChData = new CALLBACK_BACKGETCHDATA(TestCallBackGetChData); 
            CallbackChData(CB_BackGetChData);
#endif

            g_strPNEPath = jsonParsingValue("PNE_charger","path");

        }

        public int connect()
        {
            int result = (int)rRET.CTS_NACK;// false;
#if x64
            Console.WriteLine("x64,PSServerAPI64.dll ...");
         //   MessageBox.Show("PSServerAPI64.dll", "Loadding");
#else
            Console.WriteLine("x86,PSServerAPI.dll ...");
          //  MessageBox.Show("PSServerAPI.dll" ,"Loadding");
#endif


            try
            {

                if ("" == g_strPNEPath) return (int)rRET.CTS_FILE_NOT_EXIST; ;// result;

                IntPtr hWnd = User32Wrapper.GetConsoleWindow();
                int rtn = ctsServerCreate(1, hWnd);


                if ((int)rRET.CTS_ACK == rtn)
                {
                    if((int)rRET.CTS_ACK == ServerStart())
                    {
                        //로그 정보를 보기 위해 출력 화면 띄움
                        ConsoleVisible(SW_SHOW);
                        result = (int)rRET.CTS_ACK;// true;
                    }

                    Console.WriteLine(":" + rtn.ToString());
                }


            }
            catch(Exception ex)
            {
               //result = "false" + ex.Message;
               MessageBox.Show(ex.Message, "Connect Error");
            }

            Console.WriteLine("PNE 결과:{0}", result);
            return result;
        }

        private int ServerStart()
        {

            int rtn = ctsServerStart();

            // MessageBox.Show(string.Format("rtn : {0}", rtn));

            Console.WriteLine("결과: " + rtn.ToString());

            return rtn;
        }

       
        private async Task<int> SimepeTest(string path)
        {
            string root = System.Windows.Forms.Application.StartupPath;

            string LogRoot = "";// root + "\\PNE";

            if (path.Contains(":"))
            {
                LogRoot = path;
            }
            else
            {
                LogRoot = root + path;// "\\PNE";
            }

            //Log file 생성및 스케쥴 파일 생성
            ctsSetLogPath(LogRoot);
         //   Console.WriteLine(string.Format("path ={0}", LogRoot));

            int nModuleNum = 1;
            int nStepCount = 6;
            CTS_SIMPLE_TEST_INFO[] SimpleSch = new CTS_SIMPLE_TEST_INFO[nStepCount];

            SimpleSch[0].nStepType = (int)PS_STEP.PS_STEP_OCV;

            //휴지 스텝
            SimpleSch[1].nStepType = (int)PS_STEP.PS_STEP_REST; //REST

            SimpleSch[1].nRecordTime = 10;
            SimpleSch[1].nCutoffCondTime = 3;

            SimpleSch[2].nStepType = (int)PS_STEP.PS_STEP_REST; //REST
            SimpleSch[2].nRecordTime = 1;
            SimpleSch[2].nCutoffCondTime = 3;

            SimpleSch[3].nStepType = (int)PS_STEP.PS_STEP_REST; //REST
            SimpleSch[3].nRecordTime = 101;
            SimpleSch[3].nCutoffCondTime = 3;

            SimpleSch[4].nStepType = (int)PS_STEP.PS_STEP_REST; //REST
            SimpleSch[4].nRecordTime = 1;
            SimpleSch[4].nCutoffCondTime = 3;

            SimpleSch[5].nStepType = (int)PS_STEP.PS_STEP_REST; //REST
            SimpleSch[5].nRecordTime = 205;
            SimpleSch[5].nCutoffCondTime = 3;

            String strOut = "1";
#if false
            MessageBox.Show("ctsSendSimpleTest" + string.Format(", CH={0}",strOut), "Start~!!!");
#endif
            int errCode;
            errCode = ctsSendSimpleTest((uint)nModuleNum, Int32.Parse(strOut), 0, nStepCount, SimpleSch);

            if ((int)rRET.CTS_ACK == errCode )
            {
                Console.WriteLine(string.Format("path ={0}", LogRoot));

                // 정상
                Console.WriteLine(string.Format("OK - code ={0}", errCode.ToString()));
                //Console.WriteLine("OK.....");

            }
            else
            {
                Console.WriteLine(string.Format("error- code ={0}", errCode.ToString()));
            }


            return errCode;
        }

        private int ConsoleVisible(int nCmdShow)
        {
            IntPtr hWnd = User32Wrapper.GetConsoleWindow();
            User32Wrapper.ShowWindow(hWnd, nCmdShow);

            return User32Wrapper.IsWindowVisible(hWnd);
        }

        private void ServerClose()
        {
            int rtn = ctsServerClose();

            MessageBox.Show(string.Format("code ={0}", rtn.ToString()), "OK");
        }

        //ksj 20200728 : delegate 호출 메소드 선언 (접속 이벤트)
        public int ctsConnected(int nModuleID, ref CTS_MD_SYSTEM_DATA sysinfo)
        {
            string str;
            string strModelName = Encoding.Default.GetString(sysinfo.szModelName);

            str = string.Format("ctsConnected!! {0},{1},{2}",
                                        sysinfo.nModuleID,
                                        sysinfo.nProtocolVersion,
                                        strModelName
                                        );

            int rtn = 0;

          if ("" != str)
            {
                getPythonCallFunc(str);


#if true
                //1. JSON 경로 

                var mydelegate = new Action<object>(delegate (object param)
                {
                    Console.WriteLine(param.ToString());
                    aSyncSimepeTest(param.ToString());
                    
                });

                if ("" != g_strPNEPath)
                {
                    mydelegate.Invoke(g_strPNEPath);
                }

                // Or: Started as Thread:
                new System.Threading.Thread(delegate (object param) {
                    //So something
                }).Start();


#else

                if ("" != g_strPNEPath) { 
                    aSyncSimepeTest(g_strPNEPath);// "c:\\PNE3");
                }
#endif
            }

            Console.Write(str + "\n"); // 통신 연결됨---- OK


            return 0;
        }

        public string jsonParsingValue(string key, string subkey)
        {
            dynamic  dobj =  jsonParsingWebSocketValue(key);

            string value = "";
            if(null != dobj)
            {
                if (dobj.ContainsKey(subkey))
                {
                    value = dobj[subkey];
                }

            }
         
            return value;
        }

       public  dynamic jsonParsingWebSocketValue(string key)
       {
            CJsonParser cjson = CJsonParser.Instatce(); 
            dynamic dobj = cjson.getObject(key);
            return dobj;
        }



        public async void aSyncSimepeTest(string path)
        {

            Console.WriteLine("=======================================aSyncSimepeTest:");//
                                                                                         //+ nReuslt.ToString());
            await Task.Delay(1000);// 대기...

          int nReuslt = await SimepeTest(path);


          Console.WriteLine("aSyncSimepeTest:" + nReuslt.ToString());

          if ((int)rRET.CTS_ACK == nReuslt)
          {
#if false
                ConsoleVisible(SW_HIDE);
#else
                //1.실제 정보 출력이 보여지면 화면을 닫기 (CallBack)

#endif

            }

        }


        private void TestCallBackGetChData(UInt32 nModIDandChIdex, ref CTS_VARIABLE_CH_DATA ChData)
        {

            try
            {
#if true
                Console.WriteLine("::1111");

                var wc = new WORDConverter
                {
                    Value = (uint)nModIDandChIdex
                };
                int nChIndex = wc.HIWORD;       // 0 : 채널 1, 1 : 채널 2, unused
                int nModuleID = wc.LOWORD;


                Console.WriteLine("::" + nChIndex.ToString());
#else
                Console.WriteLine("::1111");
#endif

                //BaseLib.Helper.LogHelper.Debug($"0", $"[TRACDE] HandleCallbackChData(), wc.HIWORD = {wc.HIWORD}");
                throw new AccessViolationException();
            }
            catch(Exception ex)
            {

                Console.WriteLine("ex" + ex.Message);// nChIndex.ToString());
            }

        }

        void getPythonCallFunc(string strMsg)
        {
            if (null != _scope)
            {
                try
                {
                    var getPythonFunc = _scope.GetVariable<Func<string, string>>("getPythonFunc");

                    Console.WriteLine(":" + getPythonFunc(strMsg +"\nStart!!!"));

                    Console.WriteLine(":");
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error");
                }
            }

        }

    }//class
}//namespace
