using BaseLib.Data.DTO;
using BaseLib.Defines;
using BaseLib.Pubsub;
using PneCtsLib.Data.DTO;
using PneCtsLib.Defines;
using SQLManager.Data.DTO;
using SQLManager.Data.Query;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.CompilerServices;

namespace PneCtsLib.Core
{
	public class Core : BaseLib.Pubsub.ASubscribableBase<object>
	{
		#region variables

		public const int INSTALLED_MODULE_NO = 1;

		public const int CONNECTION_TIMEOUT = 1000 * 30;
#if False
        private int[] requstTaskSeq = { -1, -1, -1 };   // { unused, ch1, ch2 }

        private int[] requestTaskStepNo = { -1, -1, -1 };   // { unused, ch1, ch2 }
#else
        private int[] requstTaskSeq = { -1, -1, -1 };   // { ch1, ch2, unused }

        private int[] requestTaskStepNo = { -1, -1, -1 };   // { ch1, ch2, unused }
#endif
		private DateTime lastRequestTimeSendSch;

        #endregion

        #region delegate

        public delegate void dVariableChDataListChanged(CTS_VARIABLE_CH_DATA_DTO dto);

		public delegate void dVariableChDataDListChanged(CTS_VARIABLE_CH_DATA_D_DTO dto);

		public delegate void dVariableChDataFListChanged(CTS_VARIABLE_CH_DATA_F_DTO dto);

#endregion

#region property

		private PSServerAPI.dCallbackConnected CallbackConnected { get; set; }

		private PSServerAPI.dCallbackClosed CallbackClosed { get; set; }

		private PSServerAPI.dCallbackChData CallbackChData { get; set; }

		private PSServerAPI.dCallbackChData_D CallbackChData_D { get; set; }

		private PSServerAPI.dCallbackChData_F CallbackChData_F { get; set; }

		private PSServerAPI.dCallbackTestComplete CallbackTestComplete { get; set; }

		private PSServerAPI.dCallbackStackNotify CallbackStackNotify { get; set; }

		private PSServerAPI.dCallbackStepEndDataReceive CallbackStepEndDataReceive { get; set; }

		private PSServerAPI.dCallbackEmergency CallbackEmergency { get; set; }

		private PSServerAPI.dCallbackPause CallbackPause { get; set; }

		private CTS_MD_SYSTEM_DATA_DTO SystemData { get; set; }

		private Dictionary<byte, CurrentTaskInfoDTO> CurrentTaskInfoMap { get; set; }

		public PneCtsDataDTO CtsData { get; set; }

		public bool IsAvailable { get; set; }
		
        public int ChCodeLast1 { get; set; }
        public int ChCodeLast2 { get; set; }
#endregion

#region constructor (single-ton)

		private static Core m_Instance;
		public static Core Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new Core(LogDev.Cycler);
				}
				return m_Instance;
			}
		}

		private Core(LogDev logDev) : base(logDev, 1)
		{
			IsAvailable = true;

			CtsData = new PneCtsDataDTO();
			CurrentTaskInfoMap = new Dictionary<byte, CurrentTaskInfoDTO>();
			ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;

			CallbackConnected = new PSServerAPI.dCallbackConnected(HandleCallbackConnected);
			CallbackClosed = new PSServerAPI.dCallbackClosed(HandleCallBackClosed);
			CallbackChData = new PSServerAPI.dCallbackChData(HandleCallbackChData);
			CallbackChData_D = new PSServerAPI.dCallbackChData_D(HandleCallbackChData_D);
			CallbackChData_F = new PSServerAPI.dCallbackChData_F(HandleCallbackChData_F);
			CallbackTestComplete = new PSServerAPI.dCallbackTestComplete(HandleCallbackTestComplete);
			CallbackStackNotify = new PSServerAPI.dCallbackStackNotify(HandleCallStackNotify);
			CallbackStepEndDataReceive = new PSServerAPI.dCallbackStepEndDataReceive(HandleCallbackStepEndDataReceive);
			CallbackEmergency = new PSServerAPI.dCallbackEmergency(HandleCallbackEmergency);
			CallbackPause = new PSServerAPI.dCallbackPause(HandleCallbackPause);

			PSServerAPI.CallbackConnected(CallbackConnected);
			PSServerAPI.CallbackClosed(CallbackClosed);
			PSServerAPI.CallbackChData(CallbackChData);
			PSServerAPI.CallbackChData_D(CallbackChData_D);
			PSServerAPI.CallbackChData_F(CallbackChData_F);
			PSServerAPI.CallbackTestComplete(CallbackTestComplete);

			// bcz p already has loop of get stack data
			PSServerAPI.CallbackStackNotify(CallbackStackNotify);
			PSServerAPI.CallbackStepEndDataReceive(CallbackStepEndDataReceive);
			PSServerAPI.CallbackEmergency(CallbackEmergency);
			PSServerAPI.CallbackPause(CallbackPause);

            lastRequestTimeSendSch = DateTime.Now;
        }
		
#endregion

#region callback

		private void HandleCallbackConnected(int nModuleID, ref CTS_MD_SYSTEM_DATA sysInfo)
		{
			SystemData = new CTS_MD_SYSTEM_DATA_DTO(sysInfo);

            tbl_log_app.Instance.Insert(LogDev.Cycler, $"module({nModuleID}) was connected (module : {nModuleID}, protocol Ver. : 0x{string.Format("{0:X4}", sysInfo.nProtocolVersion)}, installed ch Num : {sysInfo.nInstalledChCount})");
			WriteLog($"module({nModuleID}) was connected (module : {nModuleID}, protocol Ver. : 0x{string.Format("{0:X4}", sysInfo.nProtocolVersion)}, installed ch Num : {sysInfo.nInstalledChCount})");

            BaseLib.Helper.LogHelper.Debug($"1", $"[Cycler]  Module {nModuleID} is Connected");
            BaseLib.Helper.LogHelper.Debug($"2", $"[Cycler]  Module {nModuleID} is Connected");

            Publish(PushDataDTO.PushDataTypes.Open, $"ModuleID : {nModuleID}, Protocol Ver. : 0x{string.Format("{0:X4}", sysInfo.nProtocolVersion)}, Installed ch Num : {sysInfo.nInstalledChCount}");
			ConnectionState = BaseLib.Defines.ConnectionStates.Connected;

			// 1초마다 스택 확인
			CheckStackData(nModuleID);
		}


		private void HandleCallBackClosed(int nModuleID)
		{
			ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;
			Publish(PushDataDTO.PushDataTypes.Close, $"ModuleID : {nModuleID} is Closed");

			WriteLog($"[Cycler] ModuleID : {nModuleID} is Closed");
            BaseLib.Helper.LogHelper.Debug($"1", $"[Cycler]  {nModuleID} is Closed");
            BaseLib.Helper.LogHelper.Debug($"2", $"[Cycler]  {nModuleID} is Closed");
        }

		//Random rnd = new Random();
		private void HandleCallbackChData(UInt32 nModIDandChIdex, ref CTS_VARIABLE_CH_DATA ChData)
		{
            var wc = new WORDConverter
            {
                Value = (uint)nModIDandChIdex
            };
            int nChIndex = wc.HIWORD;       // 0 : 채널 1, 1 : 채널 2, unused
            int nModuleID = wc.LOWORD;

            //BaseLib.Helper.LogHelper.Debug($"0", $"[TRACDE] HandleCallbackChData(), wc.HIWORD = {wc.HIWORD}");

            if (ConnectionState == BaseLib.Defines.ConnectionStates.Disconnected) return;

			if (SystemData.InstalledChCount == 0) return;

			//ChData.chData.lVoltage = rnd.Next();
			
			var sVarChResultData = new CTS_VARIABLE_CH_DATA_DTO(ChData);
			CtsData.UpdateData(sVarChResultData.ChData);
			Publish(PushDataDTO.PushDataTypes.Data, sVarChResultData);

            var currentTaskInfo = GetCurrentTaskInfo((byte)nChIndex);   // nModIDandChIdex --> nChIndex, 0-base
            string str = sVarChResultData.ChData.StateToString();
			//Console.Out.WriteLine($"[HandleCallbackChData] State : {str}, Voltage : {sVarChResultData.ChData.Voltage / 1000000.0f}V, Current : {sVarChResultData.ChData.Current / 1000000.0f}A, Code : {sVarChResultData.ChData.ChCode}");
		}

		private void HandleCallbackChData_D(UInt32 nModIDandChIdex, ref CTS_VARIABLE_CH_DATA_D ChData)
		{
            if (ConnectionState == BaseLib.Defines.ConnectionStates.Disconnected) return;

			if (SystemData.InstalledChCount == 0) return;

            var wc = new WORDConverter
            {
                Value = (uint)nModIDandChIdex
            };
            int nChIndex = wc.HIWORD;       // 0 : 채널 1, 1 : 채널 2, OK, 0-base
            int nModuleID = wc.LOWORD;      // 1

            // BaseLib.Helper.LogHelper.Debug($"0", $"[TRACDE] HandleCallbackChData_D(), wc.HIWORD = {wc.HIWORD}");

            var sVarChResultData = new CTS_VARIABLE_CH_DATA_D_DTO(ChData);
			CtsData.UpdateData(sVarChResultData.ChData);
			Publish(PushDataDTO.PushDataTypes.Data, sVarChResultData);

            int chdata_chno = sVarChResultData.ChData.ChNo;

            var currentTaskInfo = GetCurrentTaskInfo((byte)nChIndex);   // nModIDandChIdex --> nChIndex , 0-base
            string str = sVarChResultData.ChData.StateToString();

            int nChNum = nChIndex + 1;


            //Console.Out.WriteLine($"[HandleCallbackChData_D] State : {str}, Voltage : {sVarChResultData.ChData.Voltage / 1000000.0f}V, Current : {sVarChResultData.ChData.Current / 1000000.0f}A, Code : {sVarChResultData.ChData.ChCode}");

            float chVolt = sVarChResultData.ChData.Voltage / 1000000.0f;
            if (chVolt > 0.1f)
            {
                BaseLib.Helper.LogHelper.Debug($"{nChNum}", $"[Cycler] ChData ({nModuleID},{nChIndex},{chdata_chno}) "        // ch 1 ==> {1,0,1},   ch2 ==> {1,1,2}
                                                                        + $"taskId : {currentTaskInfo.TaskId}, "
                                                                        + $"State : {str}, "
                                                                        + $"Voltage : {sVarChResultData.ChData.Voltage / 1000000.0f}V, "
                                                                        + $"Current : {sVarChResultData.ChData.Current / 1000000.0f}A, "
                                                                        + $"ChargeAh : {sVarChResultData.ChData.ChargeAh}Ah, "
                                                                        + $"DisChargeAh : {sVarChResultData.ChData.DisChargeAh}Ah, "
                                                                        + $"Capacitance : {sVarChResultData.ChData.Capacitance}A, "
                                                                        + $"Code : {sVarChResultData.ChData.ChCode}, "
                                                                        + $"taskSeq : {requstTaskSeq[nChIndex]}");
            }

        }

        private void HandleCallbackChData_F(UInt32 nModIDandChIdex, ref CTS_VARIABLE_CH_DATA_F ChData)
		{
            if (ConnectionState == BaseLib.Defines.ConnectionStates.Disconnected) return;

			if (SystemData.InstalledChCount == 0) return;

            var wc = new WORDConverter
            {
                Value = (uint)nModIDandChIdex
            };
            int nChIndex = wc.HIWORD;       // 0 : 채널 1, 1 : 채널 2, unused
            int nModuleID = wc.LOWORD;      // 1

            //BaseLib.Helper.LogHelper.Debug($"0", $"[TRACDE] HandleCallbackChData_F(), wc.HIWORD = {wc.HIWORD}");

            var sVarChResultData = new CTS_VARIABLE_CH_DATA_F_DTO(ChData);
			CtsData.UpdateData(sVarChResultData.ChData);
			Publish(PushDataDTO.PushDataTypes.Data, sVarChResultData);

            var currentTaskInfo = GetCurrentTaskInfo((byte)nChIndex);   // nModIDandChIdex --> nChIndex, 0-base
            string str = sVarChResultData.ChData.StateToString();
        }

		private void HandleCallbackTestComplete(int nModuleID, int nChNum)  // nChIndex --> nChNum , 1번 채널 = 1, 1-base
        {
            BaseLib.Helper.LogHelper.Debug($"0", $"[TRACE] HandleCallbackTestComplete(nModuleID = {nModuleID}, nChNum = {nChNum} )");

            byte nChIndex = (nChNum == 1) ? (byte) 0 : (byte) 1;

            // change to available
            IsAvailable = true;

			tbl_log_app.Instance.Insert(LogDev.Cycler, $"test completed (module : {nModuleID}, nChNum : {nChNum})");
			WriteLog($"[Cycler] test completed (module : {nModuleID}, nChNum : {nChNum})");

            var currentTaskInfo = GetCurrentTaskInfo((byte)nChIndex);       // 0-base
            BaseLib.Helper.LogHelper.Debug($"0", $"[Cycler] Complete #1, taskId : {currentTaskInfo.TaskId}, (module : {nModuleID}, nChNum : {nChNum}), current TaskSeq : {currentTaskInfo.TaskSeq}, requst TaskSeq : {requstTaskSeq[nChIndex]}");

            if (currentTaskInfo.TaskId > 0 && currentTaskInfo.TaskSeq > 0)
            {
                BaseLib.Helper.LogHelper.Debug($"0", $"[Cycler] Complete #2, taskId : {currentTaskInfo.TaskId}, (module : {nModuleID}, nChNum : {nChNum}), current TaskSeq : {currentTaskInfo.TaskSeq}, requst TaskSeq : {requstTaskSeq[nChIndex]}");
                currentTaskInfo.Clear();

                var dto = new TaskInfoDTO(TaskStates.Completed, nModuleID, nChNum);
                Publish(PushDataDTO.PushDataTypes.Data, dto, requstTaskSeq[nChIndex]);   // nChNum --> nChIndex
            }
        }


        /*
        [2020.09.23 16:45:08.038]        step end(module : 1, ch idx : 1, step : 2)
        [2020.09.23 16:45:08.086]        step end(module : 1, ch idx : 1, step : 4)
        [2020.09.23 16:45:08.403]        [Cycler]        test completed(module : 1, nChNum : 1)
        [2020.09.23 16:47:03.218]        step end(module : 2, ch idx : 1, step : 2)
        [2020.09.23 16:47:03.301]        step end(module : 2, ch idx : 1, step : 4)
        [2020.09.23 16:47:04.268]        [Cycler]        test completed(module : 1, nChNum : 2)
        */
        private void HandleCallbackStepEndDataReceive(int nModIDandChIdex, int nStepNum)  
		{
			var wc = new WORDConverter
			{
				Value = (uint)nModIDandChIdex
			};
            int nChIndex = wc.HIWORD;       // 0-base, but, ch 1, ch2 모두 1로 출력 (API BUG !!!!)
            int nModuleID = wc.LOWORD;      // 1-base

            BaseLib.Helper.LogHelper.Debug($"0", $"[TRACDE] HandleCallbackStepEndDataReceive(), wc.HIWORD = {wc.HIWORD}");
            BaseLib.Helper.LogHelper.Debug($"0", $"[TRACDE] HandleCallbackStepEndDataReceive(), nChIndex = {nChIndex}");

            tbl_log_app.Instance.Insert(LogDev.Cycler, $"step end (module : {nModuleID}, ch idx : {nChIndex}, step : {nStepNum})");
			WriteLog($"step end (module : {nModuleID}, ch idx : {nChIndex}, step : {nStepNum})");
            BaseLib.Helper.LogHelper.Debug($"0", $"[Cycler] step end (module : {nModuleID}, ch idx : {nChIndex}, step : {nStepNum})");

            //Publish(PushDataDTO.PushDataTypes.Log, $"Step End ch idx : {nChIndex}, End step : {nStepNum}");
		}

		private void HandleCallbackEmergency(int nModuleID, ref CTS_EMG_DATA emgData)
		{
			var dto = new CTS_EMG_DATA_DTO(emgData);

            BaseLib.Helper.LogHelper.Debug($"1", $"[Cycler] Module {nModuleID} Emergency");
            BaseLib.Helper.LogHelper.Debug($"2", $"[Cycler] Module {nModuleID} Emergency");

            Publish(PushDataDTO.PushDataTypes.Log, $"Emergency!!! (module : {nModuleID}, errCode : {dto.Code})");
        }

		private void HandleCallStackNotify(int nModuleID, int nChNum)  // nChIndex --> nChNum (1번 채널 값이 1로 나옴), 1-base
        {
            BaseLib.Helper.LogHelper.Debug($"0", $"[TRACE] HandleCallStackNotify(nModuleID = {nModuleID}, ch = {nChNum} )");

            byte nChIndex = (nChNum == 1) ? (byte)0 : (byte)1;

            // stack data popup
            int nStackSize = PSServerAPI.ctsGetStackedDataSize(nModuleID, nChIndex);  // <--- 0-base

            BaseLib.Helper.LogHelper.Debug($"0", $"[TRACE] HandleCallStackNotify(), nStackSize = {nStackSize} )");

            Publish(PushDataDTO.PushDataTypes.Log, $"Stack Notify (ModuleID : {nModuleID}, ch : {nChNum}, stack size : {nStackSize})");
		}

		private void HandleCallbackPause(int nModuleID, int nChNum)  // nChIndex --> nChNum, 1-base
        {
            BaseLib.Helper.LogHelper.Debug($"{nChNum}", $"[TRACE] HandleCallbackPause(nModuleID = {nModuleID}, nChNum = {nChNum} )");
            BaseLib.Helper.LogHelper.Debug($"{nChNum}", $"[TRACE] HandleCallbackPause(), requstTaskSeq = {requstTaskSeq[0]}, {requstTaskSeq[1]}, {requstTaskSeq[2]}");
            Console.Out.WriteLine($"[TRACE] HandleCallbackPause(), requstTaskSeq = {requstTaskSeq[0]}, {requstTaskSeq[1]}, {requstTaskSeq[2]}");

            tbl_log_app.Instance.Insert(LogDev.Cycler, $"paused (module : {nModuleID}, ch : {nChNum})");
			WriteLog($"paused (module : {nModuleID}, ch : {nChNum})");

            if (nChNum == 2 && ChCodeLast2 != 0)
            {
                var dto = new TaskInfoDTO(TaskStates.Pause, nModuleID, nChNum, ChCodeLast2, requstTaskSeq[1]);
                Publish(PushDataDTO.PushDataTypes.Data, dto);
            }
            else if (nChNum == 1 && ChCodeLast1 != 0)
            {
                var dto = new TaskInfoDTO(TaskStates.Pause, nModuleID, nChNum, ChCodeLast1, requstTaskSeq[0]);
                Publish(PushDataDTO.PushDataTypes.Data, dto);
            }
		}

#endregion

#region method


		private CurrentTaskInfoDTO GetCurrentTaskInfo(byte nChIndex)    // channelNo --> nChIndex, 0-base
        {
			CurrentTaskInfoDTO dto;
			if (!CurrentTaskInfoMap.TryGetValue(nChIndex, out dto))
			{
				dto = new CurrentTaskInfoDTO("", 0, 0, 0);
				CurrentTaskInfoMap.Add(nChIndex, dto);
			}
			return dto;
		}

		public override bool Connect(object data = null)
		{
			//PSServerAPI.ctsSetLogPath($@"{System.IO.Directory.GetCurrentDirectory()}\log".ToCharArray());
			CurrentTaskInfoMap = new Dictionary<byte, CurrentTaskInfoDTO>();
			// API 이용하여 Server 초기화
			int nRtn = PSServerAPI.ctsServerCreate(INSTALLED_MODULE_NO, IntPtr.Zero);
			if (nRtn == 0)
			{
				Publish(PushDataDTO.PushDataTypes.Log, $"ctsServerCreate rtn: {nRtn}", -1, LogLevels.E);

				tbl_log_app.Instance.Insert(LogDev.Cycler, $"ctsServerCreate api was failed ({nRtn})", LogLevels.E);
				WriteLog($"ctsServerCreate api was failed ({nRtn})");

				return false;
			}
			Publish(PushDataDTO.PushDataTypes.Log, $"ctsServerCreate rtn: {nRtn}");
			tbl_log_app.Instance.Insert(LogDev.Cycler, "ctsServerCreate is success");
			WriteLog("ctsServerCreate is success");

#if true
//L.E.P
            Console.Write("PS Server Start");
            //서버 시작
            nRtn = PSServerAPI.ctsServerStart();
			if (nRtn == 0)
			{
				Publish(PushDataDTO.PushDataTypes.Log, $"ctsServerStart rtn : {nRtn}", -1, LogLevels.E);
				tbl_log_app.Instance.Insert(LogDev.Cycler, $"ctsServerStart api was failed ({nRtn})", LogLevels.E);
				WriteLog($"ctsServerStart api was failed ({nRtn})");
				return false;
			}
			Publish(PushDataDTO.PushDataTypes.Log, $"ctsServerStart rtn : {nRtn}");
			tbl_log_app.Instance.Insert(LogDev.Cycler, "ctsServerStart is success");
			WriteLog("ctsServerStart is success");

			ConnectionState = BaseLib.Defines.ConnectionStates.Connecting;
			Publish(PushDataDTO.PushDataTypes.Open, "waiting...");
			tbl_log_app.Instance.Insert(LogDev.Cycler, "ctsServer is waiting");
			WriteLog("ctsServer is waiting");

			Task.Factory.StartNew(() =>
			{
				int timeout = CONNECTION_TIMEOUT;
				while (timeout > 0)
				{
					if (ConnectionState != BaseLib.Defines.ConnectionStates.Connected)
					{
						System.Threading.Thread.Sleep(10);
						timeout -= 10;
					}
					else
					{
						break;
					}
				} // end while

				if (timeout <= 0)
				{
					Disconnect();
				}
			});
#endif
            return true;
		}
		
		public override void Disconnect()
		{
			ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;
			CurrentTaskInfoMap.Clear();

			try
			{
				PSServerAPI.ctsServerClose();
			}
			catch
			{

			}

			Publish(PushDataDTO.PushDataTypes.Close, "ctsServer stopped");

			tbl_log_app.Instance.Insert(LogDev.Cycler, "ctsServer was stopped");
			WriteLog("ctsServer is waiting");
		}

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool SendSchedule(string filepath, byte nChIndex, int nBitLCh, string runId, int taskId, int taskSeq, int taskStepNo)  // channelNo --> nChIndex
        {
            requstTaskSeq[nChIndex] = taskSeq;          // 1-base --> 0-base
            requestTaskStepNo[nChIndex] = taskStepNo;   // 1-base --> 0-base

            int nChNum = nChIndex + 1;
            BaseLib.Helper.LogHelper.Debug($"{nChNum}", $"SendSchedule(), nChIndex = {nChIndex}, requstTaskSeq = {requstTaskSeq[nChIndex]}, requestTaskStepNo = {requestTaskStepNo[nChIndex]}");

            if (nChIndex == 1)
            {
                ChCodeLast2 = 0;
            }
            else
            {
                ChCodeLast1 = 0;
            }

 
            TimeSpan ElapsedTime = DateTime.Now - lastRequestTimeSendSch;

            TimeSpan waitTime = new TimeSpan(00, 00, 15);
            if (ElapsedTime < waitTime) // 10sec 이전에 실행되었으면
            {
                int sec = (int)(waitTime - ElapsedTime).TotalSeconds;

                lastRequestTimeSendSch = DateTime.Now;

                BaseLib.Helper.LogHelper.Debug($"{nChNum}", $"SendSchedule(), {sec} sec wait ");

                System.Threading.Thread.Sleep(sec * 1000);
            }
            else
            {
                lastRequestTimeSendSch = DateTime.Now;
            }
            BaseLib.Helper.LogHelper.Debug($"{nChNum}", $"SendSchedule(), send schedule file ");

            ////////////////////////////////////////////// 스케쥴 전송
            int errCode = PSServerAPI.ctsSendSchedule(INSTALLED_MODULE_NO, nBitLCh, 0, filepath.ToCharArray(), 0, 0);   // nBitLCh = Channel Bit Flag 1~32CH (LSB)
            if (errCode != Constants.CTS_ACK)
			{
				IsAvailable = true;

				var currentTaskInfo = GetCurrentTaskInfo(nChIndex);    // 0-base
                currentTaskInfo.Clear();
				
				Console.Out.WriteLine($"ctsSendSchedule fail.. errcode : [{errCode}]");
				return false;
			}
			else
			{
				IsAvailable = false;

				var currentTaskInfo = GetCurrentTaskInfo(nChIndex);         // 0-base
                currentTaskInfo.Set(runId, taskId, taskSeq, taskStepNo);
				
				Console.Out.WriteLine($"ctsSendSchedule Success");
				return true;
			}
		}

		public void SendPause(int nBitLCh)
		{
			PSServerAPI.ctsSendPause(1, nBitLCh);
		}

		public void SendPauseCancel(int nBitLCh)
		{
			PSServerAPI.ctsSendPauseCancel(1, nBitLCh);
		}


		public void SendStop(byte nChIndex, int nBitLCh)   // channelNo --> nChIndex
        {
			IsAvailable = true;

            Console.Out.WriteLine($"SendStop( nChIndex : {nBitLCh}, nBitLCh : {nBitLCh}");
            Console.Out.WriteLine($"SendStop(), nChIndex = {nChIndex}, requstTaskSeq = {requstTaskSeq[nChIndex]}, requestTaskStepNo = {requestTaskStepNo[nChIndex]}");

            PSServerAPI.ctsSendStop(1, nBitLCh);        // nBitLCh : ch 1 == 1, ch 2 == 2, ch 3 = 4 ..... ch1 & ch 2 == 3

            //var currentTaskInfo = GetCurrentTaskInfo(nChIndex);     // 0-base
            //currentTaskInfo.Clear();
		}

		public void SendStopCancel(int nBitLCh)
		{
			PSServerAPI.ctsSendStopCancel(1, nBitLCh);
		}

		public void SendNextStep(int nBitLCh)
		{
			PSServerAPI.ctsSendNextStep(1, nBitLCh);
		}

		public void SendContinue(int nBitLCh)
		{
			PSServerAPI.ctsSendContinue(1, nBitLCh);
		}

		private void CheckStackData(int nModuleID)
		{
			Task.Factory.StartNew(() =>
			{
				while (ConnectionState == BaseLib.Defines.ConnectionStates.Connected)
				{
					PopStackData(nModuleID, 0);
					PopStackData(nModuleID, 1);

					System.Threading.Thread.Sleep(10);
				} // end while
			});
		}


        private void PopStackData(int nModuleID, int nChIndex)  // nChIndex --> nChNum
        {
			List<CTS_VARIABLE_CH_DATA_DTO> popDataList = null;

            int chFlag = 1 << nChIndex;
			int nStackSize = PSServerAPI.ctsGetStackedDataSize(nModuleID, chFlag);    // channel flag, 3번 channel 설정일 경우 4(bit : 0000 0100) 입력), multi 안됨
            if (nStackSize > 0)
			{
                //BaseLib.Helper.LogHelper.Debug($"0", $"[Cycler] PopStackData(), ch idx = {nChIndex}, nStackSize = {nStackSize}");
                popDataList = new List<CTS_VARIABLE_CH_DATA_DTO>();
			}

			for (int i = 0; i < nStackSize; ++i)
			{
				var stPopData = new CTS_VARIABLE_CH_DATA();
				PSServerAPI.ctsPopSaveData(nModuleID, chFlag, ref stPopData);       // channel flag, 3번 channel 설정일 경우 4(bit : 0000 0100) 입력), multi 안됨
                popDataList.Add(new CTS_VARIABLE_CH_DATA_DTO(stPopData));
			} // end for

			if (nStackSize > 0)
			{
				var currentTaskInfo = GetCurrentTaskInfo((byte)nChIndex);       // 0-base
                //BaseLib.Helper.LogHelper.Debug($"0", $"[Cycler] PopStackData(), ch idx = {nChIndex}, TaskId = {currentTaskInfo.TaskId}, TaskSeq = {currentTaskInfo.TaskSeq}, TaskStepNo = {currentTaskInfo.TaskStepNo}, nStackSize = {nStackSize}");

                if (currentTaskInfo.TaskId > 0 && currentTaskInfo.TaskSeq > 0)
				{
					tbl_log_cts_chdata query = new tbl_log_cts_chdata();
					foreach (var dto in popDataList)
					{
						var data = new tbl_log_cts_chdata_DTO
						{
							run_id = currentTaskInfo.RunId,
							task_id = currentTaskInfo.TaskId,
							task_seq = currentTaskInfo.TaskSeq,

							chno = dto.ChData.ChNo,
							state = dto.ChData.ChState,
							step_type = dto.ChData.ChStepType,
							mode = dto.ChData.ChMode,
							data_select = dto.ChData.ChDataSelect,
							code = dto.ChData.ChCode,
							stepno = dto.ChData.ChStepNo,
							grade_code = dto.ChData.ChGradeCode,
							voltage = dto.ChData.Voltage,
							current = dto.ChData.Current,
							charge_ah = dto.ChData.ChargeAh,
							discharge_ah = dto.ChData.DisChargeAh,
							capacitance = dto.ChData.Capacitance,
							watt = dto.ChData.Watt,
							charge_wh = dto.ChData.ChargeWh,
							discharge_wh = dto.ChData.DisChargeWh,
							step_day = dto.ChData.StepDay,
							step_time = dto.ChData.StepTime,
							total_day = dto.ChData.TotalDay,
							total_time = dto.ChData.TotalTime,
							impedance = dto.ChData.Impedance,
							reserved_cmd = dto.ChData.ChReservedCmd,
							comm_state = dto.ChData.ChCommState,
							output_state = dto.ChData.ChOutputState,
							input_state = dto.ChData.ChInputState,
							aux_count = dto.ChData.AuxCount,
							can_count = dto.ChData.CanCount,
							total_cycle_num = dto.ChData.TotalCycleNum,
							current_cycle_num = dto.ChData.CurrentCycleNum,
							acc_cycle_group_num1 = dto.ChData.AccCycleGroupNum1,
							acc_cycle_group_num2 = dto.ChData.AccCycleGroupNum2,
							acc_cycle_group_num3 = dto.ChData.AccCycleGroupNum3,
							acc_cycle_group_num4 = dto.ChData.AccCycleGroupNum4,
							acc_cycle_group_num5 = dto.ChData.AccCycleGroupNum5,
							multi_cycle_group_num1 = dto.ChData.MultiCycleGroupNum1,
							multi_cycle_group_num2 = dto.ChData.MultiCycleGroupNum2,
							multi_cycle_group_num3 = dto.ChData.MultiCycleGroupNum3,
							multi_cycle_group_num4 = dto.ChData.MultiCycleGroupNum4,
							multi_cycle_group_num5 = dto.ChData.MultiCycleGroupNum5,
							ave_voltage = dto.ChData.AvgVoltage,
							ave_current = dto.ChData.AvgCurrent,
							save_sequence = dto.ChData.SaveSequence,
							cv_day = dto.ChData.CVDay,
							cv_time = dto.ChData.CVTime,
							sync_time1 = dto.ChData.SyncTime.Length > 0 ? dto.ChData.SyncTime[0] : 0,
							sync_time2 = dto.ChData.SyncTime.Length > 1 ? dto.ChData.SyncTime[1] : 0,
							voltage_input = dto.ChData.VoltageInput,
							voltage_power = dto.ChData.VoltagePower,
							voltage_bus = dto.ChData.VoltageBus,
							using_chamber = dto.ChData.UsingChamber,
							record_time_no = dto.ChData.RecordTimeNo,
							out_mux_use = dto.ChData.OutMuxUse,
							out_mux_backup = dto.ChData.OutMuxBackup,
						};
						query.Insert(data);

                        if (nChIndex == 1)
                        {
                            ChCodeLast2 = data.code;
                        }
                        else
                        {
                            ChCodeLast1 = data.code;
                        }
                    } // end foreach
				} // end if
				
				Publish(PushDataDTO.PushDataTypes.Data, popDataList);
			}
		}


		// 절연 채널 인덱스는 0, 1

		public void Insulation(int nChIndex)        // 0-base
        {
			PSServerAPI.ctsSendIso(1, nChIndex, 0, 0);
		}



		// 비절연 채널 인덱스는 0, 1

		public void NonInsulation(int nChIndex)     // 0-base
        {
			PSServerAPI.ctsSendIso(1, nChIndex, 0, 1);
		}

#endregion

	}
}
