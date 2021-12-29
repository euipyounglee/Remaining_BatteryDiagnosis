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



        public delegate void dCallbackChData(UInt32 nModIDandChIdex, ref CTS_VARIABLE_CH_DATA ChData);

        [DllImport(STR_DllNAME)]
        public static extern void CallbackChData(dCallbackChData handler);



        //        private dCallbackChData CallbackChData { get; set; }

        ScriptScope _scope;
        public ClassPneCtsLib(ScriptScope scope)
        {
            _scope = scope;
        }

        public bool connect()
        {
            var result = false;
#if x64
            Console.WriteLine("x64");
            MessageBox.Show("PSServerAPI64.dll", "Loadding");
#else
            Console.WriteLine("x86");
            MessageBox.Show("PSServerAPI.dll" ,"Loadding");
#endif

            try
            {
                //IntPtr hWnd = GetConsoleWindow();

                IntPtr hWnd =  Process.GetCurrentProcess().MainWindowHandle;

                int rtn = ctsServerCreate(1, hWnd);


                if (1 == rtn)
                {
                    if(1 == ServerStart())
                    {

                        CALLBACK_CONNECTED CB_Connected = new CALLBACK_CONNECTED(ctsConnected);
                        CallbackConnected(CB_Connected);

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
            return result;// false;// result;
        }

        private int ServerStart()
        {

            int rtn = ctsServerStart();

            // MessageBox.Show(string.Format("rtn : {0}", rtn));

            Console.WriteLine("결과: " + rtn.ToString());

            return rtn;
        }

        private static async void SimepeTest(string path)
        {
            string root = System.Windows.Forms.Application.StartupPath;
#if false
            ctsSetLogPath("C:\\PNE1");
#else

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
#endif

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
#if true
            MessageBox.Show("ctsSendSimpleTest" + string.Format(", CH={0}",strOut), "Start~!!!");
#endif
            int errCode;
            errCode = ctsSendSimpleTest((uint)nModuleNum, Int32.Parse(strOut), 0, nStepCount, SimpleSch);

            if (errCode == 1)
            {
                // 정상
                MessageBox.Show(string.Format("code ={0}", errCode.ToString()), "OK");
                Console.WriteLine("OK.....");
            }
            else
            {
                MessageBox.Show(string.Format("code ={0}", errCode.ToString()), "Error");

            }


            return;// errCode;
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
                getPytghonFunc(str);

#if flase
                rtn = SimepeTest("c:\\PNE4");

                if (1 == rtn)
                {
                   var  CallbackChData = new dCallbackChData(HandleCallbackChData);

                }
#else

                MyAsyncFunc();
#endif
            }

            Console.Write(str + "\n"); // 통신 연결됨---- OK



            return 0;
        }

       
        public static async void MyAsyncFunc()
        {
            await Task.Delay(5000);
            Console.WriteLine("End MyAsyncFunc");

             SimepeTest("c:\\PNE3");
            //rtn = SimepeTest("c:\\PNE4");
        }


        //public  async Task<int> SumTwoOperationsAsync()
        //{
        //    var firstTask = GetOperationOneAsync();
        //    var secondTask = GetOperationTwoAsync();

        //    return await firstTask + await secondTask;
        //}


        private void HandleCallbackChData(UInt32 nModIDandChIdex, ref CTS_VARIABLE_CH_DATA ChData)
        {


            var wc = new WORDConverter
            {
                Value = (uint)nModIDandChIdex
            };
            int nChIndex = wc.HIWORD;       // 0 : 채널 1, 1 : 채널 2, unused
            int nModuleID = wc.LOWORD;

            Console.WriteLine("::" + nChIndex.ToString());
            //BaseLib.Helper.LogHelper.Debug($"0", $"[TRACDE] HandleCallbackChData(), wc.HIWORD = {wc.HIWORD}");



        }

        void getPytghonFunc(string strMsg)
        {
            if (null != _scope)
            {
                try
                {
                    var getPythonFuncResult = _scope.GetVariable<Func<string, string>>("getPythonFunc");

                    Console.WriteLine(":" + getPythonFuncResult(strMsg +"\nStart!!!"));

                    Console.WriteLine(":");
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error");
                }
            }

        }

    }//class
}//namespace
