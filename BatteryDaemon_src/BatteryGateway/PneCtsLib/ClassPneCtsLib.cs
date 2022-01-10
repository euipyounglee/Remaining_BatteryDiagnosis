//using PneCtsLib.Core;
using BatteryGateway.Common;
using BatteryGateway.PneCtsLib;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BatteryGateway.Program;

namespace BatteryGateway.PneCtsLib
{

 
    

    //======================================================================

    public class ClassPneCtsLib : PSServerAPI
    {


        public const int INSTALLED_MODULE_NO = 1;

        ScriptScope _scope;
        string g_strPNEPath;


        CALLBACK_CONNECTED CB_Connected;
        CALLBACK_BACKGETCHDATA CB_BackGetChData;

        static int chNumber = 0;

        public ClassPneCtsLib(ScriptScope scope, int chNo)
        {
            _scope = scope;
            chNumber = chNo;
        }

        private int init()
        {


            int nResult = 1;
            try
            {
                CB_Connected = new ClassPneCtsLib.CALLBACK_CONNECTED(HandleCallbackConnected); //connect  콜백
                ClassPneCtsLib.CallbackConnected(CB_Connected);

                CB_BackGetChData = new ClassPneCtsLib.CALLBACK_BACKGETCHDATA(HandleCallbackChData);
                ClassPneCtsLib.CallbackChData(CB_BackGetChData);

                g_strPNEPath = jsonParsingValue("PNE_charger", "path");

            }
            catch (Exception ex)
            {
                nResult = 0;
                var rootpath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                string  path = Path.Combine(rootpath, IntPtr.Size == 8 ? "x64" : "x86");

                string strFilePath = path + "\\" + STR_DllNAME;

                Console.WriteLine(ex.Message);

                string errMSG = ex.Message;

                if (errMSG.Contains(STR_DllNAME))
                {
                    if (File.Exists(strFilePath) == false)
                    {
                        MessageBox.Show(string.Format( "파일존재 확인\n{0}\n{1}" , strFilePath, ex.Message), "경고-File Not Found");
                    }
                    else
                    {
                        MessageBox.Show(string.Format("파일존재 확인후. 프로그램 재 실행해 주세요\n{0}", ex.Message), "경고-API Dll Load Fail");
                    }
                }
                else
                {
                    MessageBox.Show(ex.Message, "경고");
                }
            }

            return nResult;
        }


        public static int Close()
        {

            int nRtn = ClassPneCtsLib.ctsServerClose();
            Console.WriteLine("ctsServerClose:" + nRtn.ToString());
            return nRtn;// 0;
        }

        public int ServerListen()
        {
            int result = (int)rRET.CTS_NACK;// false;
#if x64
            Console.WriteLine("x64,PSServerAPI64.dll ...");
#else
            Console.WriteLine("x86,PSServerAPI.dll ...");
#endif
            if (1 == init())
            {

                try
                {

                    if ("" == g_strPNEPath || null == g_strPNEPath)
                    {
                        if (DialogResult.No == MessageBox.Show("설정 파일을 찾을수 없습니다.\n계속 하시겠습니까?", "경고!!!", MessageBoxButtons.YesNo))
                        {
                            return (int)rRET.CTS_FILE_NOT_EXIST; ;
                        }
                    }

                    IntPtr hWnd = User32Wrapper.GetConsoleWindow();

                    int nRtn = ClassPneCtsLib.ctsServerCreate(INSTALLED_MODULE_NO, hWnd);
                    ConsoleVisible(SW_SHOW);
                    Console.WriteLine("ctsServerCreate ..." + nRtn.ToString());

                    if ((int)rRET.CTS_ACK == nRtn)
                    {
                        nRtn = ServerStart();

                        Console.WriteLine("ServerStart ..." + nRtn.ToString());

                        if ((int)rRET.CTS_ACK == nRtn)
                        {
                            //Console.WriteLine("ServerStart ...");
                            //로그 정보를 보기 위해 출력 화면 띄움
                            // ConsoleVisible(SW_SHOW);
                            result = (int)rRET.CTS_ACK;// true;
                        }

                        Console.WriteLine("wait connect:" + nRtn.ToString());
                    }


                }
                catch (Exception ex)
                {
                    //result = "false" + ex.Message;
                    MessageBox.Show(ex.Message, "Connect Error");
                }
            }

          //  Console.WriteLine("PNE 결과:{0}", result);
            return result;
        }

        private int ServerStart()
        {

            int nRtn = ClassPneCtsLib.ctsServerStart();//서버 시작

            // MessageBox.Show(string.Format("rtn : {0}", rtn));

            Console.WriteLine("ctsServerStart 결과: " + nRtn.ToString());

            return nRtn;
        }

        /*********************************************************************
        //외부에서 JSON 문법으로 받으면 변경 시켜서 보내기 해야 할듯 하다.
        // 수신 받으면 JSON을 파싱해서 넣기
        *********************************************************************/
        private static async Task<int> SimepeTest(int nBitLCh, string path)
        {

            Console.WriteLine("SimepeTest :begine ");// + nRtn.ToString());

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

       //     PSServerAPI.chNo = 1;

            //Log file 생성및 스케쥴 파일 생성
            ctsSetLogPath(LogRoot);
            Console.WriteLine(string.Format("path ={0}", LogRoot));

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

            int errCode=0;
            try
            {
#if true
            String strOut =  string.Format("{0}",nBitLCh);// "1";

            errCode = ctsSendSimpleTest((uint)nModuleNum, Int32.Parse(strOut), 0, nStepCount, SimpleSch);
#else
               errCode = ctsSendSimpleTest((uint)nModuleNum, nBitLCh , 0, nStepCount, SimpleSch);
#endif
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

            if (rRET.CTS_ACK == (rRET)errCode )
            {
                // 정상
                Console.WriteLine(string.Format("code ={0}", errCode.ToString()));

              //  MessageBox.Show(string.Format("code ={0}", errCode.ToString()), "OK");
                Console.WriteLine("func SimepeTest OK.....");

                //  ConsoleVisible(SW_HIDE);

            }
            else
            {
                MessageBox.Show(string.Format("code ={0}", errCode.ToString()), "Error");

            }


            return  errCode;
        }

        private static int ConsoleVisible(int nCmdShow)
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

        public int HandleCallbackConnected(int nModuleID, ref CTS_MD_SYSTEM_DATA data)
        {


            ////////////////////////////////////////////////////////////////////////////////
            string str;
            string strModelName = Encoding.Default.GetString(data.szModelName);


            Console.WriteLine("통신 연결 준비됨!!!!"); // 통신 연결됨---- OK

            str = string.Format("HandleCallbackConnected!! {0},{1},{2}",
                                        data.nModuleID,
                                        data.nProtocolVersion,
                                        strModelName
                                        );

            //ModuleID = data.nModuleID;
            //SystemType = data.nSystemType;
            //  ProtocolVersion = data.nProtocolVersion;

            Console.WriteLine("nModuleID:" + data.nModuleID.ToString());
            Console.WriteLine("nSystemType:" + data.nSystemType.ToString());
            Console.WriteLine("nProtocolVersion:" + data.nProtocolVersion.ToString());


            //ModelName = new sbyte[data.szModelName.Length];
            //Array.Copy(data.szModelName, 0, ModelName, 0, data.szModelName.Length);

         //   OSVersion = data.nOSVersion;
         //   VoltageRange = data.wVoltageRange;
         //   CurrentRange = data.wCurrentRange;

            Console.WriteLine("nOSVersion:" + data.nOSVersion.ToString());
            Console.WriteLine("wVoltageRange:" + data.wVoltageRange.ToString());
            Console.WriteLine("wCurrentRange:" + data.wCurrentRange.ToString());


            //VoltageSpec = new uint[data.nVoltageSpec.Length];
            //Array.Copy(data.nVoltageSpec, 0, VoltageSpec, 0, data.nVoltageSpec.Length);

            //CurrentSpec = new uint[data.nCurrentSpec.Length];
            //Array.Copy(data.nCurrentSpec, 0, CurrentSpec, 0, data.nCurrentSpec.Length);

            //CanCommType = data.byCanCommType;

            //TypeData = new byte[data.byTypeData.Length];
            //Array.Copy(data.byTypeData, 0, TypeData, 0, data.byTypeData.Length);

            //InstalledBoard = data.wInstalledBoard;
            //ChannelPerBoard = data.wChannelPerBoard;
          //  InstalledChCount = data.nInstalledChCount;
           // TotalJigNo = data.nTotalJigNo;

            Console.WriteLine("wInstalledBoard:" + data.wInstalledBoard.ToString());
            Console.WriteLine("wChannelPerBoard:" + data.wChannelPerBoard.ToString());
            Console.WriteLine("nInstalledChCount:" + data.nInstalledChCount.ToString());
            Console.WriteLine("nTotalJigNo:" + data.nTotalJigNo.ToString());


            //BdInJig = new uint[data.awBdInJig.Length];
            //Array.Copy(data.awBdInJig, 0, BdInJig, 0, data.awBdInJig.Length);

            //Reserved = new uint[data.reserved.Length];
            //Array.Copy(data.reserved, 0, Reserved, 0, data.reserved.Length);


#if true
            //임시- 외부에서 보내는것을 확인 하기 위함에 추가 시킴
            // 외부 에서 보낼때 확인함
            if ("" != str)
            {
                getPythonCallFunc(str);

                asyncSchedule();
            }
#else
            //스케쥴 보낼수 있는 시뮬레이터가 준비가 되었음
            //1. 파이썬 버튼 활성화 시키기, PNE-충방전기Test
            //2. Control에 준비 되었다는 ACK 를 보내준다.
            //3. BatteyControl 에서 스케쥴를 받을 준비가 됨.

#endif



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


        //static  int  chNo = 0;

        /*****************************************************************************************************
        //ConTrol에서 값이 전달해야 할듯 하다. GateWay 에서는 SimpleTest 스케줄 Self로 보내는것이 아니구나
        *****************************************************************************************************/

        public static async void asyncSchedule()
        {

            Console.WriteLine("syncSchedule : begine ...");
            await Task.Delay(1000);

            int nBitLCh = chNumber;// 1; //ch =1

            int nRtn  = await SimepeTest(nBitLCh, "\\PNE");

            Console.Write("asyncSchedule : " + nRtn.ToString() + "\n"); // 통신 연결됨---- OK

        }

        bool bschedule_OK = false;

        private  void HandleCallbackChData(UInt32 nModIDandChIdex, ref CTS_VARIABLE_CH_DATA ChData)
        {


            var wc = new WORDConverter
            {
                Value = (uint)nModIDandChIdex
            };
            int nChIndex = wc.HIWORD;       // 0 : 채널 1, 1 : 채널 2, unused
            int nModuleID = wc.LOWORD;

           // Console.WriteLine("::" + lVoltage);// nChIndex.ToString())o;
            Console.WriteLine("chNo:" + ChData.chData.chNo);

            //   var sVarChResultData = new PSServerAPI.CTS_VARIABLE_CH_DATA(ChData);


            //    var currentTaskInfo = GetCurrentTaskInfo((byte)nChIndex);   // nModIDandChIdex --> nChIndex, 0-base
            //  string str = sVarChResultData.ChData.StateToString();

            //   Console.Out.WriteLine($"[HandleCallbackChData] State : {str}, Voltage : {sVarChResultData.ChData.Voltage / 1000000.0f}V, Current : {sVarChResultData.ChData.Current / 1000000.0f}A, Code : {sVarChResultData.ChData.ChCode}");


            //BaseLib.Helper.LogHelper.Debug($"0", $"[TRACDE] HandleCallbackChData(), wc.HIWORD = {wc.HIWORD}");



            if (PS_STATE.standby == (PS_STATE)ChData.chData.chState)
            {
                if (ClassPneCtsLib.chNumber == ChData.chData.chNo)
                {
                    if (false == bschedule_OK)
                    {
                        //    asyncSchedule();
                        bschedule_OK = true;

                    }
                }
                Console.WriteLine("===================================");// + ChData.chData.chNo);
                Console.WriteLine("chNo:" + ChData.chData.chNo);
                Console.WriteLine("state:" + ChData.chData.chState);
                Console.WriteLine("chStepType:" + ChData.chData.chStepType);
                Console.WriteLine("chCode:" + ChData.chData.chCode);
                Console.WriteLine("lVoltage:" + string.Format("{0}V", ChData.chData.lVoltage/ 1000000.0f));


            }
            else
            {
                Console.WriteLine("state:" + ChData.chData.chState);
                Console.WriteLine("chStepType:" + ChData.chData.chStepType);
                Console.WriteLine("chCode:" + ChData.chData.chCode);
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
