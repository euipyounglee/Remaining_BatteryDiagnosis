using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BatteryGateway.PneCtsLib
{
    public  class PSServerAPI
    {

#if x64
        protected const string STR_DllNAME = "PSServerAPI64.dll";
#else
        protected const  string STR_DllNAME = "PSServerAPI.dll";
#endif



        // CallBack API 정의
        [DllImport(STR_DllNAME, CallingConvention = CallingConvention.StdCall)] //PSServerAPI.DLL 링크
        public static extern int CallbackConnected(CALLBACK_CONNECTED callback);
        public delegate int CALLBACK_CONNECTED(int nModuleID, ref CTS_MD_SYSTEM_DATA sysinfo); //ksj 20200728 : callback 함수 처리를 위한 대리자 deleagate 선언.



        // CallBack API 정의
        [DllImport(STR_DllNAME, CallingConvention = CallingConvention.StdCall)] //PSServerAPI.DLL 링크
        public static extern void CallbackChData(CALLBACK_BACKGETCHDATA handler);
        public delegate void CALLBACK_BACKGETCHDATA(uint nModIDandChIdex, ref CTS_VARIABLE_CH_DATA ChData); // : callback 함수 처리를 위한 대리자 deleagate 선언.




        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern int ctsServerClose();




        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern int ctsSendSimpleTest(uint nModuleID, int nBitLCh, int nBitHCh, int nStepCount, CTS_SIMPLE_TEST_INFO[] pStepInfo);

        //////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(STR_DllNAME)] //PSServerAPI.DLL 링크
        public static extern void ctsSetLogPath(string szLogPath);

        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();



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





        [StructLayout(LayoutKind.Explicit)]
        public  struct WORDConverter
        {
            [FieldOffset(0)]
            public uint Value;

            [FieldOffset(0)]
            public ushort LOWORD;

            [FieldOffset(2)]
            public ushort HIWORD;
        }


      public  enum PS_STEP
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

        public  enum PS_STATE
        {
            idle = 0, //0x00
            standby = 1,//0x01
            disconnected = 32, //0x20
            connected = 33, //0x21
            ready = 41, //0x29

        }

        public  enum rRET
        {
            CTS_NACK = 0,   /*!< 오류 (Error) */
            CTS_ACK = 1,    /*!< 정상 (Normal) */
            CTS_TIMEOUT = 2,    /*!< 시간초과 (Time over) */
            CTS_SIZE_MISMATCH = 3,  /*!< Body Size 불일치 (Size mismatch) */
            CTS_RX_BUFF_OVER_FLOW = 4,  /*!< 수신 버퍼 오버플로우 (Receive buffer overflow) */
            CTS_TX_BUFF_OVER_FLOW = 5,  /*!< 송신 버퍼 오버플로우 (Transmit buffer overflow) */
            CTS_CONNECTED = 6,  /*!< 접속 성공 (Connection success) */
            CTS_NOT_CONNECTED = 7,  /*!< 접속 되어 있지 않음 (Not connected) */
            CTS_FILE_NOT_EXIST = 8, /*!< 해당 경로에 파일이 없음 (Not exist file) */
            CTS_FILE_OPENED = 9,    /*!< 파일이 Open되어 있음 (File open fail) */
            CTS_CAN_LIN_TYPE_INVALID = 10,/*!< CAN Rx/Tx, LIN File Type이 맞지 않음 (Type mismatch) */
            CTS_CHANNEL_RUN = 11,   /*!< 현재 동작중인 채널임 (Channel is running) */
            CTS_FAIL = CTS_CHANNEL_RUN + 1 // 0xFFFFFFFF	/*!< 실패 (Failure) */
        }



        //EMG CODE 구조체 생성 API의 CTS_EMG_DATA 구조체 참고.
        [StructLayout(LayoutKind.Sequential)]
        public struct CTS_EMG_DATA
        {
            [MarshalAs(UnmanagedType.I2, SizeConst = 1)]//길이 2
            public Int16 Code;                   /**< EMG 코드 */

            [MarshalAs(UnmanagedType.I2, SizeConst = 1)]//길이 2
            public Int16 Value;                  /**< EMG 값 */

            [MarshalAs(UnmanagedType.I4, SizeConst = 1)]//길이 4
            public int reserved;             /**< 예비 공간 */

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
            public Int32 nRecordTime;           //  기록 시간 (1/10초) 예) 1초:10 , 20초:200, 0.1초: 1 


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



    }
}
