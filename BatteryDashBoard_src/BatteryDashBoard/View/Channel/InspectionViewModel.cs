//using BaseLib.Defines;
//using BaseLib.Pubsub;
//using BatteryDashBoard.Defines;
//using InspectEngineLib.Defines;
//using PneCtsLib.Data.DTO;
//using Prism.Commands;
//using RelayBoxLib.Defines;
//using RestApiLib.Core;
//using RestApiLib.Defines;
//using SharedLib.Core;
//using SharedLib.Data.VM;
//using SharedLib.Defines;
//using SQLManager.Data;
//using SQLManager.Data.DTO;
//using SQLManager.Data.Query;
//using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace BatteryDashBoard.View.Channel
{
    public partial class InspectionViewModel : AChannelBaseViewModel
    {

        /// <summary>
        /// 작업 타임아웃
        /// </summary>
        public const int TASK_TIMEOUT_MAX = 1000 * 60 * 5;

        private CancellationTokenSource cancelTokenSourceMain;
        private CancellationTokenSource cancelTokenSource;
        private Task taskTimer;

        //private ConcurrentQueue<(int, int)> m_NextJob;

        //public (int, int) NextJob
        //{
        //    get
        //    {
        //        if (m_NextJob == null )
        //        {
        //            m_NextJob = new ConcurrentQueue<(int, int)>();
        //        }

        //        if (m_NextJob.Count > 0)
        //        {
        //            var re = (-99, -99);
        //            bool ret = m_NextJob.TryDequeue(out re);
        //            if (ret)
        //            {
        //                return re;
        //            }
        //        }
        //        return (-99, -99);
        //    }
        //    set
        //    {
        //        if (m_NextJob == null)
        //        {
        //            m_NextJob = new ConcurrentQueue<(int, int)>();
        //        }
        //        m_NextJob.Enqueue(value);
        //    }
        //}

        /// <summary>
        /// selected inspection type
        /// </summary>
        //public InspectionTypes SelectedInspectionType { get; set; }

        /// <summary>
        /// tasks
        /// </summary>
        //private ObservableCollection<tbl_task_VM> m_InspectionTasks;
        //public ObservableCollection<tbl_task_VM> InspectionTasks
        //{
        //	get
        //	{
        //		if (m_InspectionTasks == null)
        //		{
        //			m_InspectionTasks = new ObservableCollection<tbl_task_VM>();
        //		}
        //		return m_InspectionTasks;
        //	}
        //	set
        //	{
        //		m_InspectionTasks = value;
        //		RaisePropertyChanged("InspectionTasks");
        //	}
        //}

        /// <summary>
        /// runnable tasks
        /// </summary>
        //public ObservableCollection<tbl_task_VM> RunnableSchedule { get; set; }

        /// <summary>
        /// visible tasks
        /// </summary>
        //private ObservableCollection<tbl_task_VM> m_VisibleTasks;
        //public ObservableCollection<tbl_task_VM> VisibleTasks
        //{
        //	get
        //	{
        //		if (m_VisibleTasks == null)
        //		{
        //			m_VisibleTasks = new ObservableCollection<tbl_task_VM>();
        //		}
        //		return m_VisibleTasks;
        //	}
        //	set
        //	{
        //		m_VisibleTasks = value;
        //		RaisePropertyChanged("VisibleTasks");
        //	}
        //}

        /// <summary>
        /// current task
        /// </summary>
        //public tbl_task_VM CurrentTask { get; set; }

        /// <summary>
        /// current task description
        /// </summary>
        private string m_CurrentTaskDescription = "";
        public string CurrentTaskDescription
        {
            get
            {
                return m_CurrentTaskDescription;
            }
            set
            {
                m_CurrentTaskDescription = value;
                //RaisePropertyChanged("CurrentTaskDescription");
            }
        }

        private string m_TimeDescription = "";
        public string TimeDescription
        {
            get
            {
                return m_TimeDescription;
            }
            set
            {
                m_TimeDescription = value;
                //RaisePropertyChanged("TimeDescription");
            }
        }


        private bool m_IsMainTaskRunning = false;
        public bool IsMainTaskRunning
        {
            get
            {
                return m_IsMainTaskRunning;
            }
            set
            {
                m_IsMainTaskRunning = value;
                //RaisePropertyChanged("IsMainTaskRunning");
            }
        }


        private bool m_IsTaskRunning = false;
        public bool IsTaskRunning
        {
            get
            {
                return m_IsTaskRunning;
            }
            set
            {
                m_IsTaskRunning = value;
                //RaisePropertyChanged("IsTaskRunning");
            }
        }

        /// <summary>
        /// 작업 타이머 사용여부
        /// </summary>
        private bool IsTaskTimerEnabled { get; set; }

        //#endregion

        //#region command

        /// <summary>
        /// unloaded
        /// </summary>
        //public DelegateCommand UnloadedCommand
        //{
        //	get
        //	{
        //		return new DelegateCommand(delegate ()
        //		{
        //			Task.Run(() =>
        //			{
        //                      // 절연
        //                      Devices.PNECTSInstance.Insulation(ChannelIndex);
        //                      System.Threading.Thread.Sleep(1000);

        //                      Devices.PNECTSInstance.Insulation(ChannelIndex);
        //                      System.Threading.Thread.Sleep(1000);

        //                      // unsubscribe
        //                      ChannelVMInstance.UnsubscribeTaskProcessingCallback();
        //                      ChannelVMInstance.UnsubscribeTaskCompletedCallback();
        //				ChannelVMInstance.ReceivedACIRMeasureDataCallback = null;
        //                      ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = null;
        //                  });
        //		});
        //	}
        //}

        /// <summary>
        /// cancel and restart
        /// </summary>
        //public DelegateCommand GoToSelectInspectionCommand
        //{
        //	get
        //	{
        //		return new DelegateCommand(delegate ()
        //		{
        //			if (MessageBox.Show("검사를 다시 진행하시겠습니까?", "알림", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
        //			{
        //				return;
        //			}

        //			new tbl_task_run_detail().Delete(ChannelVMInstance.TaskRunInfo.run_id);

        //			ChannelVMInstance.CurrentProgressState = ProgressStateTypes.SelectInspection;
        //		});
        //	}
        //}

        /*
                /// <summary>
                /// change channel popup state
                /// </summary>
                public DelegateCommand<ChannelViewPopupStates?> InspectionViewIsolatePopupCommand
                {
                    get
                    {
                        return new DelegateCommand<ChannelViewPopupStates?>(delegate (ChannelViewPopupStates? value)
                        {
                            if (value != null)
                            {
                                InspectionViewPausePopup = value.Value; // CurrentChannelViewPopupState = value.Value;
                            }
                        });
                    }
                }
        */
        //#endregion

        //#region method

        /// <summary>
        /// do after view loading
        /// </summary>
        public async override void DoLoaded()
        {
            base.DoLoaded();

            // 작업 콜백 등록
            //         ChannelVMInstance.SubscribeTaskProcessingCallback(TaskProcessingCallback);
            //         ChannelVMInstance.SubscribeTaskCompletedCallback(TaskCompletedCallback);
            //ChannelVMInstance.ReceivedACIRMeasureDataCallback = PublichReceivedACIRMeasureData;

            // set flag
            IsTaskRunning = true;

            // init task
            //CurrentTask = null;

            byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

            {
                // RestApi : EVAL_STEP.SCENARIO_SEL - 간편진단 선택
                //var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
                //RestApiLib.Core.Helper.SendStep(restApiLogType, new pg_btry_srvive_evl_input_DTO
                //{
                //    STEP_NO = $"{(int)EVAL_STEP.SCENARIO_SEL}",
                //    CHANNEL = $"{ChannelNum}",

                //    INPUT_VALUE = SelectedInspectionType == InspectionTypes.Simple ? "간편검사" : (SelectedInspectionType == InspectionTypes.Normal ? "표준검사" : "정밀검사"),
                //    LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
                //});
            }

            // load task
            CurrentTaskDescription = "작업 스케쥴을 로드 합니다.";
            //LoadScheduleList(SelectedInspectionType);

            bool result = await Task.Run(() =>
            {
                //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), #1");
                bool ret = false;
                ret = DoRelayBoxInsulationCheck();
                return ret;
            });

            // Relay Box & ST5520 Insulation test
            if (result == false)
            {
                MessageBox.Show("절연저항 측정을 확인하여 주시기 바랍니다.");
                IsTaskRunning = false;
                return;
            }

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, Devices.PNECTSInstance.ConnectionState = {Devices.PNECTSInstance.ConnectionState}");

            // CTS 절연 검사
            //{
                //if (Devices.PNECTSInstance.ConnectionState != ConnectionStates.Connected )
                //{
                //	MessageBox.Show("PNE CTS가 연결되어 있지 않습니다.");
                //	IsTaskRunning = false;
                //	return;
                //}
            //}


            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, search sch file check");

            // 스케쥴 파일 유무 확인
            CurrentTaskDescription = "작업 스케쥴 파일을 검사하고 있습니다.";
            {
                //foreach (var task in InspectionTasks)
                //{
                //	if (task.file_path != null && task.file_path.Length > 2)
                //	{
                //		if (!System.IO.File.Exists(task.file_path))
                //		{
                //			MessageBox.Show($"스케쥴 파일 {task.file_path}을 찾을 수 없습니다.");
                //			IsTaskRunning = false;
                //			return;
                //		}
                //	}
                //} // end foreach
            } // end if

            // 비절연 (두 번 api 호출)
            {
                CurrentTaskDescription = "충방전기 비절연 상태로 전환 중 입니다.";

                //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, #AAA");

                bool ret = await Task.Run(() =>
                {
                    float voltage = 0.0f;

                    //Devices.PNECTSInstance.NonInsulation(ChannelIndex);
                    //System.Threading.Thread.Sleep(3000);

                    //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, #AAA-1");

                    //// TODO : PNE 전압 체크
                    //voltage = Devices.PNECTSInstance.CtsData.GetVoltage(ChannelNum) / 1000000.0f;   // ChannelNum --> ChannelIndex
                    //if ( voltage > 1.0f )
                    //{
                    //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, Cycler NonInsulation check #1, voltage [{voltage}]");
                    //    return true;
                    //}

                    //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, #AAA-2");

                    ////Devices.PNECTSInstance.NonInsulation(ChannelIndex == 1 ? 0 : 1);
                    //Devices.PNECTSInstance.NonInsulation(ChannelIndex);
                    //System.Threading.Thread.Sleep(3000);

                    //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, #AAA-3");

                    //// TODO : PNE 전압 체크
                    //voltage = Devices.PNECTSInstance.CtsData.GetVoltage(ChannelNum) / 1000000.0f;       // ChannelNum --> ChannelIndex
                    //if (voltage > 1.0f)
                    //{
                    //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, Cycler NonInsulation check #2, voltage [{voltage}]");
                    //    return true;
                    //}

                    //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, #AAA-4");
                    return false;
                });

                //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, #BBB");

                if (ret == false)
                {
                    CurrentTaskDescription = "충방전기 비전열 상태를 확인해주세요.";
                }

            }

            // create CTS measure data
            //new tbl_log_cts_measuredata().CleanAndInsert(new tbl_log_cts_measuredata_DTO
            //{
            //	run_id = ChannelVMInstance.TaskRunInfo.run_id
            //});

            // begin task
            //SelectTask(-1,-1);
            //NextJob = (-1, -1);

            IsMainTaskRunning = true;


            cancelTokenSourceMain = new CancellationTokenSource();
            CancellationToken tokenMain = cancelTokenSourceMain.Token;
            taskTimer = Task<object>.Factory.StartNew(() => RunMainTask(), tokenMain);

            {
                //var restApiLogType = (ChannelNum == 1) ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;

                //// RestApi : start 상태 등록 - 시나리오 시작
                //ChannelVMInstance.TaskRunInfo.INSPCT_SN = RestApiLib.Core.Helper.SendStartTest(restApiLogType, ChannelNum
                //                    , SelectedInspectionType, SharedPreferences.Instance.LocalConfig.BatteryTestType
                //                    , SelectedBatteryInfo.Dto, SharedPreferences.Instance.BatteryBarcodeMap[ChannelIndex]
                //                    , Devices.ACIRInstance.Hostname
                //                    , VisibleTasks.Count
                //                    , SharedPreferences.Instance.LoginUser.USER_ID);
            }

            // 기존 저장소와 키 연결, run_id & INSPCT_SN
            //new tbl_task_run().UpdateExtraInfo(ChannelVMInstance.TaskRunInfo.run_id, ChannelVMInstance.TaskRunInfo.INSPCT_SN);
        }

        private bool DoRelayBoxInsulationCheck()
        {
            byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), start");

            //Devices.RelayBoxInstance.IsLogWrite = true;

            //byte ChannelNumOther = (ChannelNum == 1) ? (byte)2 : (byte)1;
            //while (Devices.RelayBoxInstance.IsIsolationCheckRunning(ChannelNumOther) == true)
            //{
            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), wait");
            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RelayBox Insulation check Wait");
            //    System.Threading.Thread.Sleep(1000);
            //}

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), wait passed...");

            //Devices.RelayBoxInstance.SetIsolationCheckRunning(ChannelNum, true);
            //try
            //{
            //    Task<bool> result = null;
            //    Task<bool> measureResultPlus = null;
            //    Task<bool> measureResultMinus = null;

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), GetCurrentMode[{1}] = {Devices.RelayBoxInstance.CurrentInfo.GetCurrentMode(1)}");
            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), GetCurrentMode[{2}] = {Devices.RelayBoxInstance.CurrentInfo.GetCurrentMode(2)}");

            //    // Set Off Mode 
            //    if ( Devices.RelayBoxInstance.CurrentInfo.GetCurrentMode(ChannelNum) == RelayBoxModes.Error)
            //    {
            //        BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #1");
            //        if (!Devices.RelayBoxInstance.SetMode(ChannelNum, RelayBoxModes.Off))
            //        {
            //            BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #2");
            //            System.Threading.Thread.Sleep(1000 * 10);
            //            Devices.RelayBoxInstance.SetIsolationCheckRunning(ChannelNum, false);
            //            return false;
            //        }
            //    }

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #3");

            //    // 1. Set Plus Mode
            //    if (!Devices.RelayBoxInstance.SetMode(ChannelNum, RelayBoxModes.Plus))
            //    {
            //        BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #4");
            //        Devices.RelayBoxInstance.SetIsolationCheckRunning(ChannelNum, false);
            //        return false;
            //    }

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #5");

            //    // 2. start ST5520 test
            //    result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StartTest);
            //    System.Threading.Thread.Sleep(1000 * 1);

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #6");

            //    // 3. measure (not overflow)
            //    measureResultPlus = Devices.ST5520Instance.Measure(true);


            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #7");

            //    result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #8");

            //    // 4. Set Minus Mode
            //    if (!Devices.RelayBoxInstance.SetMode(ChannelNum, RelayBoxModes.Minus))
            //    {
            //        BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #9");

            //        Devices.RelayBoxInstance.SetIsolationCheckRunning(ChannelNum, false);
            //        return false;
            //    }

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #10");

            //    // 5. start ST5520 test
            //    result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StartTest);
            //    System.Threading.Thread.Sleep(1000 * 1);

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #11");

            //    // 6. measure (not overflow)
            //    measureResultMinus = Devices.ST5520Instance.Measure(true);

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #12");

            //    result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #13");

            //    // 8. Set Off
            //    if (!Devices.RelayBoxInstance.SetMode(ChannelNum, RelayBoxModes.Off))
            //    {
            //        BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #14");

            //        System.Threading.Thread.Sleep(1000 * 10);
            //        Devices.RelayBoxInstance.SetIsolationCheckRunning(ChannelNum, false);
            //        return false;
            //    }

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #15");

            //    Devices.RelayBoxInstance.SetIsolationCheckRunning(ChannelNum, false);

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #16");

            //    Devices.RelayBoxInstance.IsLogWrite = false;

            //    return true;
            //}
            //catch (Exception e)
            //{
            //    Console.Out.WriteLine(e.ToString());

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"channelNo = {ChannelNum}, DoRelayBoxInsulationCheck(), BBB #17");

            //    var result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);

            //    Devices.RelayBoxInstance.SetIsolationCheckRunning(ChannelNum, false);

            //    Devices.RelayBoxInstance.IsLogWrite = false;

            //    return false;
            //}
            return false;
        }



        /// <summary>
        /// load simple tasks
        /// </summary>
        //private void LoadScheduleList(InspectionTypes taskType)
        //{
        //InspectionTasks.Clear();
        //if (RunnableSchedule == null)
        //{
        //             RunnableSchedule = new ObservableCollection<tbl_task_VM>();
        //}
        //         RunnableSchedule.Clear();

        //if (VisibleTasks == null)
        //{
        //	VisibleTasks = new ObservableCollection<tbl_task_VM>();
        //}
        //VisibleTasks.Clear();

        //List<tbl_task_VM> list = new List<tbl_task_VM>();
        //var result = new tbl_task().GetTasks(StringEnum.GetStringValue(taskType), SelectedBatteryInfo.BTRY_CODE);
        //foreach (var dto in result)
        //{
        //	list.Add(new tbl_task_VM(dto));
        //}

        //InspectionTasks = new ObservableCollection<tbl_task_VM>(list);
        //         RunnableSchedule = new ObservableCollection<tbl_task_VM>(InspectionTasks.Where(p => !p.disabled));
        //VisibleTasks = new ObservableCollection<tbl_task_VM>(InspectionTasks.Where(p => p.visibility));

        //if (InspectionTasks.Count > 0)
        //{
        //	// update task info
        //	ChannelVMInstance.TaskRunInfo.task_id = InspectionTasks.ElementAt(0).task_id;
        //	ChannelVMInstance.TaskRunInfo.task_name = InspectionTasks.ElementAt(0).task_name;
        //	ChannelVMInstance.TaskRunInfo.task_type = StringEnum.GetStringValue(InspectionTasks.ElementAt(0).task_type);

        //	new tbl_task_run().UpdateTaskInfo(ChannelVMInstance.TaskRunInfo);
        //}
        //}

        private object RunMainTask()
        {
            byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;
            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunMainTask() #### START");

            //while (IsMainTaskRunning)
            //{
            //    (int step_no, int task_seq ) = NextJob;

            //    if (step_no == -99 && task_seq == -99 )
            //    {
            //        System.Threading.Thread.Sleep(1000);
            //        continue;
            //    }

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunMainTask() #### {step_no}, {task_seq}");
            //    SelectSchedule(step_no, task_seq);
            //} // end while

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunMainTask() #### END");

            return null;
        }



        /// <summary>
        /// select tasks
        /// </summary>
        private void SelectSchedule(int step_no, int task_seq)
        {
            byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

            //         if (CurrentTask == null)
            //{
            //             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, SelectSchedule({step_no},{task_seq}), CurrentTask is null");
            //             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, SelectSchedule({step_no},{task_seq}), RunnableSchedule.Count() = {RunnableSchedule.Count()}");

            //             // find first task without disabled
            //             if (RunnableSchedule.Count() > 0 )
            //	{
            //                 if (    CurrentTask == null
            //                    || ( CurrentTask != null && CurrentTask.TaskState == TaskStates.Completed ) )
            //                 {
            //                     CurrentTask = RunnableSchedule.ElementAt(0);
            //                     IsTaskRunning = true;

            //                     RunTask(CurrentTask);
            //                 }
            //	}
            //	else
            //	{
            //                 // 검사 완료 처리
            //                 BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, SelectSchedule({step_no},{task_seq}), scenario end #1");

            //                 // 진단 종료
            //                 IsTaskRunning = false;

            //		new tbl_task_run().UpdateTaskCompleted(new tbl_task_run_DTO
            //		{
            //			run_id = ChannelVMInstance.TaskRunInfo.run_id,
            //			completed_dt = DateTime.Now
            //		});

            //		CurrentTaskDescription = "진단 검사가 완료되었습니다.";

            //                 {
            //                     // RestApi : EVAL_STEP.SCENARIO_END  - 검사 완료
            //                     var restApiLogType = (ChannelNum == 1) ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
            //                     RestApiLib.Core.Helper.SendStep(restApiLogType, new pg_btry_srvive_evl_input_DTO
            //                     {
            //                         STEP_NO = $"{(int)EVAL_STEP.SCENARIO_END}",
            //                         CHANNEL = $"{ChannelNum}",
            //                         INPUT_VALUE = "",
            //                         LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
            //                     });
            //                 }
            //	}
            //}
            //else
            //{
            //             // find next task
            //             int idx = RunnableSchedule.IndexOf(CurrentTask) + 1;
            //             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, SelectSchedule({step_no},{task_seq}), find next task, next idx [{idx}]");

            //             if (idx < RunnableSchedule.Count)
            //	{
            //                 // 시나리오가 남아있는 경우
            //                 CurrentTask = RunnableSchedule.ElementAt(idx);
            //		RunTask(CurrentTask);
            //	}
            //	else
            //	{
            //                 // 시나리오가 남아있지 않는 경우
            //                 BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, SelectSchedule({step_no},{task_seq}), scenario end #2");

            //                 //byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

            //                 // finish tasks
            //                 IsTaskRunning = false;

            //		// 진단검사완료
            //		new tbl_log_cts_measuredata().UpdateLast(new tbl_log_cts_measuredata_DTO
            //		{
            //			run_id = ChannelVMInstance.TaskRunInfo.run_id,
            //			last_dt = DateTime.Now,
            //			voltage_last = Devices.PNECTSInstance.CtsData.GetVoltage(ChannelNum),
            //                     current_last = Devices.PNECTSInstance.CtsData.GetCurrent(ChannelNum)
            //                 });

            //		new tbl_task_run().UpdateTaskCompleted(new tbl_task_run_DTO
            //		{
            //			run_id = ChannelVMInstance.TaskRunInfo.run_id,
            //			completed_dt = DateTime.Now
            //		});

            //		CurrentTaskDescription = "진단 검사가 완료되었습니다.";

            //                 {
            //                     var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;

            //                     // RestApi : EVAL_STEP.SCENARIO_END - 시나리오 종료 (검사 완료)
            //                     RestApiLib.Core.Helper.SendStep(restApiLogType, new pg_btry_srvive_evl_input_DTO
            //                     {
            //                         STEP_NO = $"{(int)EVAL_STEP.SCENARIO_END}",
            //                         CHANNEL = $"{ChannelNum}",
            //                         INPUT_VALUE = "",
            //                         LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
            //                     });
            //                 }
            //	}
            //} // end if
        }

        /// <summary>
        /// insert task run detail
        /// </summary>
        /// <param name="task"></param>
        /// <param name="state"></param>
        //private void InsertTaskRunDetail(tbl_task_VM task, string state)
        //{
        //	new tbl_task_run_detail().Insert(new tbl_task_run_detail_DTO
        //	{
        //		run_id = ChannelVMInstance.TaskRunInfo.run_id,
        //		task_seq = task.task_seq,
        //              step_no = task.step_no,
        //              sub_step_no = task.sub_step_no,
        //              device_cd = $"{task.device_cd}",
        //		device_name = task.device_name,
        //		disabled = task.disabled ? "Y" : "N",
        //		file_path = task.file_path,
        //		task_condition = task.task_condition,
        //		task_detail_name = task.task_detail_name,
        //		task_group = task.task_group,
        //		task_tag = task.task_tag,
        //		time_expect = task.time_expect,
        //		state = state,
        //		start_dt = task.BeginTime
        //	});
        //}

        /// <summary>
        /// run task timer
        /// </summary>
        private object RunTaskTimer(int task_id, int task_seq, int timeout)
        {
            byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, TaskTimer({task_id}, {task_seq}, {timeout})");

            //int TICK = 100;         // 100 ms
            //int sum = 0;
            //int count = 0;
            //while (IsTaskTimerEnabled)
            //{
            //    if (count >= 1000) // 100 ms * 10
            //    {
            //        count = 0;

            //        if (CurrentTask != null)
            //        {
            //            CurrentTask.ElapsedTime = DateTime.Now - CurrentTask.BeginTime;
            //        }

            //        TimeDescription = $"{sum/1000} / {timeout/1000} [sec]";
            //    }

            //    System.Threading.Thread.Sleep(TICK);
            //    count += TICK;
            //    sum += TICK;

            //    if (sum >= timeout)
            //    {
            //        BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, TaskTimer({task_id}, {task_seq}), timeout");

            //        TimeDescription = $"{sum / 1000} / {timeout / 1000} [sec]";
            //        if (IsTaskTimerEnabled)
            //        {
            //            IsTaskTimerEnabled = false;
            //            if (cancelTokenSource.Token.IsCancellationRequested == false)
            //            {
            //                TaskCompletedCallback(false, -1, "Timeout", task_seq);
            //            }
            //        }
            //    }
            //} // end while

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, TaskTimer({task_id}, {task_seq}), end");
            //TimeDescription = $"{sum / 1000} / {timeout / 1000} [sec]";

            //return new { Sum = sum };
            return 1;// new { Sum = sum };
        }


        /// <summary>
        /// run task
        /// </summary>
        //      private async void RunTask(tbl_task_VM task)
        //{
        //          byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

        //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), START, ------------------------------------------------------------");

        //IsTaskTimerEnabled = true;

        //int timeout = TASK_TIMEOUT_MAX;

        //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), CHECK, #1");

        //// Fixed, scenario timeout {
        //if ( task.time_expect * 1000 > TASK_TIMEOUT_MAX)
        //{
        //    //k = RunTaskTimer(task.task_id, task.task_seq, (int)(task.time_expect * 1000 * 1.1f));  // +10%
        //    timeout = (int)(task.time_expect * 1000 * 1.1f);
        //}
        //// Fixed, scenario timeout }

        //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), CHECK, #2");

        //// timer cancel....
        //if (taskTimer != null && taskTimer.IsCanceled == false )
        //{
        //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), RunTaskTimer, IsCanceled == false ");
        //    System.Threading.Thread.Sleep(200);

        //    if (taskTimer != null && taskTimer.IsCanceled == false)
        //    {
        //        if (cancelTokenSource != null)
        //        {
        //            BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), RunTaskTimer => cancelTokenSource.Cancel() ");
        //            cancelTokenSource.Cancel();
        //        }
        //    }
        //}

        //cancelTokenSource = new CancellationTokenSource();
        //CancellationToken token = cancelTokenSource.Token;

        //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), CHECK, #3");
        //taskTimer = Task<object>.Factory.StartNew(() => RunTaskTimer(task.task_id, task.task_seq, timeout), token);


        //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), CHECK, #4");
        //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), CHECK, #5, device_cd = {task.device_cd}, device_name = {task.device_name}, detail_name = {task.task_detail_name}");

        //bool isCtsMeasureData1 = false;         // 2C 방전 전 측정
        //bool isCtsMeasureData2 = false;         // 2C 충전 전 측정
        //bool isCtsMeasureData2_before = false;  // soh test code
        //bool isCtsMeasureData2_after = false;   // soh test code
        //bool isBMSMeasureDischarge2C = false;   // 2C 방전 후
        //InspectionTypes inspectType = InspectionTypeConverter.Convert(ChannelVMInstance.TaskRunInfo.task_type);

        // 각  step_no 의 시작시 측정
        //if (task.device_cd == LogDev.Cycler
        //    || task.device_cd == LogDev.ACIR
        //    || task.device_cd == LogDev.LOCAL)
        //{
        //    if (InspectionTypes.Simple == inspectType)
        //    {
        //        switch (CurrentTask.step_no)
        //        {
        //            case 1:         // First Rest.
        //                break;
        //            case 6:
        //                break;
        //            case 7:         // 2C 방전
        //                isCtsMeasureData2_before = true;    // soh 방전 전
        //                isCtsMeasureData1 = true;           // 2C 방전 전 측정
        //                isBMSMeasureDischarge2C = true;     // 2C 방전 후 측정 (방전시)
        //                break;
        //            case 8:
        //                isCtsMeasureData2_after = true;     // soh 방전 후 (simple은 isBMSMeasureDischarge2C 에서 측정한 값을 사용)
        //                break;
        //            case 9:         // 2C 충전
        //                isCtsMeasureData2 = true;           // 2C 충전 전 측정
        //                break;
        //            case 10:        // Last Rest.
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    else if (InspectionTypes.Normal == inspectType)
        //    {
        //        switch (CurrentTask.step_no)
        //        {
        //            case 1:         // First Rest.
        //                isCtsMeasureData2_before = true;
        //                break;
        //            case 6:
        //                break;
        //            case 7:         // 2C 방전
        //                isCtsMeasureData1 = true;       // 2C 방전 전 측정
        //                isBMSMeasureDischarge2C = true; // 2C 방전 후 측정 (방전시)
        //                break;
        //            case 9:         // 2C 충전
        //                isCtsMeasureData2 = true;       // 2C 충전 전 측정
        //                break;
        //            case 12:
        //            case 17:
        //                isCtsMeasureData2_after = true;
        //                break;
        //            case 42:        // Last Rest.
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    else if (InspectionTypes.Close == inspectType)
        //    {
        //        switch (CurrentTask.step_no)
        //        {
        //            case 2:     // 완전 방전
        //                break;
        //            case 4:     // 완전 충전
        //                break;
        //            case 6:     // 완전 방전
        //                isCtsMeasureData2_before = true; // 완전 방전 전
        //                break;
        //            case 8:     // 50% 충전
        //                isCtsMeasureData2_after = true;  // 50% 충전 후
        //                break;
        //            case 16:    // 2C 방전
        //                isCtsMeasureData1 = true;        // 2C 방전 전 측정
        //                isBMSMeasureDischarge2C = true;  // 2C 방전 후 측정 (방전시)
        //                break;
        //            case 18:    // 2C 충전
        //                isCtsMeasureData2 = true;        // 2C 충전 전 측정
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Measure, isCtsMeasureData1 = {isCtsMeasureData1}");
        //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Measure, isCtsMeasureData2 = {isCtsMeasureData2}");
        //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Measure, isCtsMeasureData2_before = {isCtsMeasureData2_before}");
        //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Measure, isCtsMeasureData2_after  = {isCtsMeasureData2_after}");
        //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Measure, isBMSMeasureDischarge2C  = {isBMSMeasureDischarge2C}");


        //bool result = await Task.Factory.StartNew(() =>
        //bool result = await Task.Run(() =>
        //{
        //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), CHECK, #6, device_cd = {task.device_cd}, device_name = {task.device_name}, detail_name = {task.task_detail_name}");

        //    task.TaskState = TaskStates.Running;

        //    //-----------------------------------------------------------------------------------------------------
        //    int ctsVoltage = Devices.PNECTSInstance.CtsData.GetVoltage(ChannelNum);
        //    int ctsCurrent = Devices.PNECTSInstance.CtsData.GetCurrent(ChannelNum);

        //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Measure, step_no {task.step_no},{task.task_seq}, voltage {ctsVoltage}, current {ctsCurrent}");

        //    // 시나리오 시작 전압 측정
        //    if (task.task_seq == 1)
        //    {
        //        BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, check ccc #4-1 ");

        //        new tbl_log_cts_measuredata().UpdateFirst(new tbl_log_cts_measuredata_DTO
        //        {
        //            run_id = ChannelVMInstance.TaskRunInfo.run_id,
        //            first_dt = task.BeginTime,
        //            voltage_first = ctsVoltage,
        //            current_first = ctsCurrent,
        //        });

        //        BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), voltage_first {ctsVoltage}, currentFirst {ctsVoltage}");

        //        BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), BIBox CheckOverVoltageStart()");
        //        Devices.BIBoxInstance.CheckOverVoltageStart(ChannelVMInstance.TaskRunInfo.run_id, task.task_id, task.task_seq, task.step_no
        //                                                   , ChannelVMInstance.TaskRunInfo.CELL_MIN_VLTGE
        //                                                   , ChannelVMInstance.TaskRunInfo.CELL_MAX_VLTGE
        //                                                   , ChannelVMInstance.SelectedBatteryInfo.Dto.CONSIST);

        //    }
        //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), CHECK, #7");

        //    if (isCtsMeasureData1)
        //    {
        //        // 방전 전 측정
        //        new tbl_log_cts_measuredata().UpdateBeforeDischarge(new tbl_log_cts_measuredata_DTO
        //        {
        //            run_id = ChannelVMInstance.TaskRunInfo.run_id,
        //            datetime_before_discharge = DateTime.Now,
        //            voltage_before_discharge = ctsVoltage,
        //            current_before_discharge = ctsCurrent
        //        });

        //        BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq})"
        //                                                    + $", [Cycler] voltage_before_discharge {ctsVoltage}"
        //                                                    + $", current_before_discharge {ctsCurrent}");
        //    }
        //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), CHECK, #8");

        //    if (isCtsMeasureData2)
        //    {
        //        // 충전 전 측정
        //        new tbl_log_cts_measuredata().UpdateBeforeCharge(new tbl_log_cts_measuredata_DTO
        //        {
        //            run_id = ChannelVMInstance.TaskRunInfo.run_id,
        //            datetime_before_charge = DateTime.Now,
        //            voltage_before_charge = ctsVoltage,
        //            current_before_charge = ctsCurrent
        //        });
        //    }

        //    if (isCtsMeasureData2_before)
        //    {
        //        // soh 방전 전 측정
        //        new tbl_log_cts_measuredata().UpdateBeforeDischarge2(new tbl_log_cts_measuredata_DTO
        //        {
        //            run_id = ChannelVMInstance.TaskRunInfo.run_id,
        //            datetime_before_discharge2 = DateTime.Now,
        //            voltage_before_discharge2 = ctsVoltage,
        //            current_before_discharge2 = ctsCurrent
        //        });

        //        BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq})"
        //                                                        + $", [Cycler] voltage_before_discharge2 {ctsVoltage}"
        //                                                        + $", current_before_discharge2 {ctsCurrent}");
        //    }

        //    if (isCtsMeasureData2_after)
        //    {
        //        // soh 방전 후 측정
        //        new tbl_log_cts_measuredata().UpdateAfterDischarge2(new tbl_log_cts_measuredata_DTO
        //        {
        //            run_id = ChannelVMInstance.TaskRunInfo.run_id,
        //            datetime_after_discharge2 = DateTime.Now,
        //            voltage_after_discharge2 = ctsVoltage,
        //            current_after_discharge2 = ctsCurrent
        //        });

        //        BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq})"
        //                                                        + $", [Cycler] voltage_after_discharge2 {ctsVoltage}"
        //                                                        + $", current_after_discharge2 {ctsCurrent}");
        //    }


        //             //-----------------------------------------------------------------------------------------------------
        //             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), CHECK, #10");
        //             switch (task.device_cd)
        //	{
        //                 //-----------------------------------------------------------------------------------------------------
        //                 case LogDev.ACIR:
        //                     BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), ACIR #1");

        //                     if (Devices.ACIRInstance.ConnectionState != ConnectionStates.Connected)
        //			{
        //				TaskCompletedCallback(false, -1, "ACIA 연결 해제됨", task.task_seq);
        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), ACIR disconnected");
        //                         return false;
        //			}

        //                     BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), ACIR #2");
        //                     if (StringEnum.GetStringValue(TaskConditions.Spectrum).Equals(task.task_condition))
        //			{
        //				task.BeginTime = DateTime.Now;
        //				InsertTaskRunDetail(task, "running");

        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), request ACIR Spectrum");
        //                         Devices.ACIRInstance.StartMeasure(0x01, 0, ChannelVMInstance.TaskRunInfo.run_id, task.task_id, task.task_seq, task.step_no, task.sub_step_no);
        //				CurrentTaskDescription = "ACIA 스펙트럼 측정 요청 완료";

        //				return true;
        //			}
        //			else if (StringEnum.GetStringValue(TaskConditions.Single).Equals(task.task_condition))
        //			{
        //				task.BeginTime = DateTime.Now;
        //				InsertTaskRunDetail(task, "running");

        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), request ACIR single");
        //                         Devices.ACIRInstance.StartMeasure(0x00, task.time_expect, ChannelVMInstance.TaskRunInfo.run_id, task.task_id, task.task_seq, task.step_no, task.sub_step_no);
        //				return true;
        //			}
        //			else
        //			{
        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), ACIR unknown");
        //                         return false;
        //			}

        //                 //-----------------------------------------------------------------------------------------------------
        //                 case LogDev.Cycler:
        //                     BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), Cycler #1");

        //                     if (Devices.PNECTSInstance.ConnectionState != ConnectionStates.Connected)
        //			{
        //				TaskCompletedCallback(false, -1, "충방전기 연결 해제됨", task.task_seq);
        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), Cycler disconnected");
        //                         return false;
        //			}

        //                     BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), Cycler #2");
        //                     CurrentTaskDescription = "";

        //                     // LogDev.Cycler, REST 
        //                     if (StringEnum.GetStringValue(TaskConditions.Rest).Equals(task.task_condition))
        //			{
        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Rest] ");
        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, check ccc #1 ");

        //                         task.BeginTime = DateTime.Now;

        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, check ccc #2 ");
        //                         InsertTaskRunDetail(task, "running");

        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), wait {task.time_expect} sec");
        //                         System.Threading.Thread.Sleep(1000 * task.time_expect);     // <---- Rest. running
        //				task.ElapsedTime = DateTime.Now - task.BeginTime;

        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), task is completed [Rest] ");
        //                         TaskCompletedCallback(true, 0, "", task.task_seq);

        //				return true;
        //			}
        //                     else
        //			{
        //                         // LogDev.Cycler, Change, Discharge
        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] ");

        //                         task.BeginTime = DateTime.Now;
        //				InsertTaskRunDetail(task, "running");

        //                         if (StringEnum.GetStringValue(TaskDetailName.Discharge).Equals(task.task_detail_name))   // Discharge
        //                         {
        //                             // LogDev.Cycler, Discharge 
        //                             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Discharge ");
        //                         }
        //                         else if (StringEnum.GetStringValue(TaskDetailName.Charge).Equals(task.task_detail_name))  // Charge
        //                         {
        //                             // LogDev.Cycler, Charge 
        //                             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Charge ");
        //                         }

        //                         // PNE 충방전 스케줄러 전달 (충전/방전)
        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] SendSchedule {task.file_path}");
        //                         bool r = Devices.PNECTSInstance.SendSchedule(task.file_path, ChannelIndex, ChannelFlag, ChannelVMInstance.TaskRunInfo.run_id, task.task_id, task.task_seq, task.step_no);
        //				if (!r)
        //				{
        //                             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] PNE SendSchedule Error");
        //                             task.TaskState = TaskStates.Pause;
        //					task.ElapsedTime = new TimeSpan(0, 0, 0);
        //				}
        //                         else
        //                         {
        //                             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] PNE SendSchedule OK");
        //                         }

        //                         if (StringEnum.GetStringValue(TaskDetailName.Discharge).Equals(task.task_detail_name))
        //                         {
        //                             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Discharge");

        //                             // LogDev.Cycler, Discharge 
        //                             if (isBMSMeasureDischarge2C)   // 2C 방전
        //                             {
        //                                 BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Discharge 2C");
        //                                 // LogDev.Cycler, 10초 방전시 BI 측정
        //                                 Task.Run(() =>
        //                                 {
        //                                     BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Discharge voltage check");

        //                                     float _CtsCurrent = Devices.PNECTSInstance.CtsData.GetCurrent(ChannelNum) / 1000000.0f;
        //                                     for (int loop = 0; loop < 18; loop++)
        //                                     {
        //                                         if (_CtsCurrent < -2.0f)   // todo:배터리 방전 용량 의 97%
        //                                         {
        //                                             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), "
        //                                                                                             + $"[Cycler] Discharge BIBox GetCellVoltage, voltage check {_CtsCurrent}");
        //                                             break;
        //                                         }
        //                                         System.Threading.Thread.Sleep(500);
        //                                         _CtsCurrent = Devices.PNECTSInstance.CtsData.GetCurrent(ChannelNum) / 1000000.0f;
        //                                     }

        //                                     System.Threading.Thread.Sleep(1000 * 5);
        //                                     Devices.BIBoxInstance.GetCellVoltage(ChannelVMInstance.TaskRunInfo.run_id, task.task_id, task.task_seq, task.step_no);

        //                                     ctsVoltage = Devices.PNECTSInstance.CtsData.GetVoltage(ChannelNum);
        //                                     ctsCurrent = Devices.PNECTSInstance.CtsData.GetCurrent(ChannelNum);
        //                                     // 2C 방전 후 측정
        //                                     new tbl_log_cts_measuredata().UpdateAfterDischarge(new tbl_log_cts_measuredata_DTO
        //                                     {
        //                                         run_id = ChannelVMInstance.TaskRunInfo.run_id,
        //                                         datetime_after_discharge = DateTime.Now,
        //                                         voltage_after_discharge = ctsVoltage,
        //                                         current_after_discharge = ctsCurrent
        //                                     });

        //                                     BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), "
        //                                                                                     + $"[Cycler] voltage_after_discharge {ctsVoltage}, "
        //                                                                                     + $"current_after_discharge {ctsCurrent}");

        //                                     BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Discharge BIBox GetCellVoltage() end");
        //                                 });
        //                             }
        //                             else
        //                             {
        //                                 BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Discharge BIBox CheckOverVoltageStart()");
        //                                 Devices.BIBoxInstance.CheckOverVoltageStart(ChannelVMInstance.TaskRunInfo.run_id, task.task_id, task.task_seq, task.step_no
        //                                                                            , ChannelVMInstance.TaskRunInfo.CELL_MIN_VLTGE
        //                                                                            , ChannelVMInstance.TaskRunInfo.CELL_MAX_VLTGE
        //                                                                            , ChannelVMInstance.SelectedBatteryInfo.Dto.CONSIST);
        //                             }
        //                         }
        //                         else if (StringEnum.GetStringValue(TaskDetailName.Charge).Equals(task.task_detail_name))  // Charge
        //                         {
        //                             // LogDev.Cycler, Charge 
        //                             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] Charge BIBox CheckOverVoltageStart()");
        //                             Devices.BIBoxInstance.CheckOverVoltageStart( ChannelVMInstance.TaskRunInfo.run_id, task.task_id, task.task_seq, task.step_no
        //                                                                        , ChannelVMInstance.TaskRunInfo.CELL_MIN_VLTGE
        //                                                                        , ChannelVMInstance.TaskRunInfo.CELL_MAX_VLTGE
        //                                                                        , ChannelVMInstance.SelectedBatteryInfo.Dto.CONSIST );
        //                         }
        //                         else
        //                         {
        //                             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), [Cycler] unknown");
        //                         }
        //                         return r;
        //			}
        //                 //-----------------------------------------------------------------------------------------------------
        //                 case LogDev.BI_BOX:
        //                     BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), BI_BOX #1");
        //                     if (Devices.BIBoxInstance.ConnectionState != ConnectionStates.Connected)
        //			{
        //				TaskCompletedCallback(false, -1, "BI 연결 해제됨", task.task_seq);
        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), BIBoxInstance disconnected");
        //                         return false;
        //			}

        //			if (StringEnum.GetStringValue(TaskConditions.Voltage).Equals(task.task_condition))
        //			{
        //				CurrentTaskDescription = "[BI] 배터리 전압을 측정 중 입니다.";
        //				task.BeginTime = DateTime.Now;

        //				InsertTaskRunDetail(task, "running");

        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), BIBoxInstance GetCellVoltage()");
        //                         Devices.BIBoxInstance.GetCellVoltage(ChannelVMInstance.TaskRunInfo.run_id, task.task_id, task.task_seq, task.step_no);

        //				return true;  // true --> false
        //			}
        //			else if (StringEnum.GetStringValue(TaskConditions.Temperature).Equals(task.task_condition))
        //			{
        //				CurrentTaskDescription = "[BI] 배터리 온도를 측정 중 입니다.";
        //				task.BeginTime = DateTime.Now;

        //				InsertTaskRunDetail(task, "running");

        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), BIBoxInstance GetCellTemperature()");
        //                         Devices.BIBoxInstance.GetCellTemperature(ChannelVMInstance.TaskRunInfo.run_id, task.task_id, task.task_seq, task.step_no);

        //                         return true;  // true --> false
        //                     }
        //			else
        //			{
        //                         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), BIBoxInstance error");
        //                         return false;
        //			}

        //                 //-----------------------------------------------------------------------------------------------------
        //                 default:
        //                     BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), default");

        //                     CurrentTaskDescription = "";
        //			task.TaskState = TaskStates.Pause;

        //			InsertTaskRunDetail(task, "pause");

        //			Console.Out.WriteLine($"device_cd : {task.device_cd} is unknown. task is skipped");
        //                     BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), task is pause, device_cd : {task.device_cd} is unknown. task is skipped");

        //                     IsTaskTimerEnabled = false;

        //			return false;
        //	} // end switch

        //             return true;
        //         });

        //         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), CHECK, #12");
        //         if (!result)
        //{
        //             // 장비 연결 해제등 에러시
        //             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), StartNew result [false] -> call SelectTask()");

        //             //SelectSchedule(task.step_no, task.task_seq);
        //}
        //         else
        //         {
        //             BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), StartNew result [true]");
        //         }
        //         BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SCENARIO, RunTask({task.task_id},{task.step_no},{task.task_seq}), CHECK, #13");

        //         if (  task.TaskState == TaskStates.Pause 
        //            || task.TaskState == TaskStates.PauseCellOverVoltage 
        //            || task.TaskState == TaskStates.PauseCellUnderVoltage)
        //         {
        //             if (PopupPauseSimpleIsOpen == false || PopupIsolateSimpleIsOpen == false)
        //             {
        //                 PopupPauseSimpleIsOpen = true;
        //             }
        //         }
        //}

        /// <summary>
        /// build data for SendUpdateState api
        /// </summary>
        /// <param name="INSPCT_SN"></param>
        /// <param name="taskSeq"></param>
        /// <param name="mode"></param>
        private void SendUpdateState(string runId, int INSPCT_SN, int taskId, int taskSeq, int step_no, int sub_step_no, string mode, string chrg, string condition, TimeSpan sittn, DateTime beginTime)
        {
            byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

            try
            {
                var endTime = beginTime + sittn;

                // build detail data
                //            var detailDto = new pg_btry_srvive_evl_detail_DTO
                //{
                //	INSPCT_SN = INSPCT_SN,
                //	STEP_NO = $"{step_no}",
                //	START_DT = string.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}",
                //		beginTime.Year, beginTime.Month, beginTime.Day,
                //		beginTime.Hour, beginTime.Minute, beginTime.Second
                //	),
                //	END_DT = string.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}",
                //		endTime.Year, endTime.Month, endTime.Day,
                //		endTime.Hour, endTime.Minute, endTime.Second
                //	)
                //};


                //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SendUpdateState, #1");

                // add, cycler data {
                //detailDto.CYCLER_V        = Devices.PNECTSInstance.CtsData.GetVoltage(ChannelNum) / 1000000.0f;
                //detailDto.CYCLER_CURRENT  = Devices.PNECTSInstance.CtsData.GetCurrent(ChannelNum) / 1000000.0f;
                //detailDto.CYCLER_CAPACITY = Devices.PNECTSInstance.CtsData.GetCapacity(ChannelNum) / 1000000.0f;
                // add, cycler data }

                //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SendUpdateState, #2, {detailDto.CYCLER_V}, {detailDto.CYCLER_CURRENT}");

                // build check data
                //            var checkDto = new pg_btry_srvive_evl_check_DTO
                //{
                //	INSPCT_SN = INSPCT_SN,
                //                STEP_SN = step_no,
                //                MODE = mode,
                //	CHRG = chrg,
                //	CND = condition,
                //	EXPECT_TIME = "",
                //	PROGRS_SITTN = string.Format("{0:D2}:{1:D2}:{2:D2}", sittn.Hours, sittn.Minutes, sittn.Seconds),
                //	PROGRS_STTUS = "COMPLETION"
                //};


                //            BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SendUpdateState, #3");

                //            switch (CurrentTask.device_cd)
                //{
                //	case LogDev.ACIR:
                //		{
                //			var rows = new tbl_log_acir_measureresult().Get(runId, taskSeq);
                //                        if (rows.Count > 0)
                //                        {
                //                            var row = rows.ElementAt(0);
                //                            detailDto.ACIA_ACIR = row.acir;
                //                            detailDto.ACIA_RP = row.rp;
                //                            detailDto.ACIA_RS = row.rs;
                //                            detailDto.ACIA_T = row.temperature;
                //                            detailDto.ACIA_V = row.voltage;
                //                        }
                //                        else
                //                        {
                //                            var rowData = new tbl_log_acir_measuredata().Get(runId, taskSeq);
                //                            if (rowData.Count > 0)
                //                            {
                //                                var row_data = rowData.ElementAt(0);
                //                                detailDto.ACIA_RE = row_data.re;
                //                                detailDto.ACIA_T = row_data.temperature;
                //                                detailDto.ACIA_V = row_data.voltage;
                //                            }
                //                        }
                //                    }
                //		break;
                //	case LogDev.Cycler:
                //		if (  StringEnum.GetStringValue(TaskConditions.Discharge).Equals(condition) 
                //                       || StringEnum.GetStringValue(TaskConditions.Charge).Equals(condition))
                //		{
                //			var result = new tbl_log_cts_chdata().GetLastData(runId, taskSeq);
                //			if (result != null)
                //			{
                //                            detailDto.CYCLER_V       = result.voltage / 1000000.0f;
                //                            detailDto.CYCLER_CURRENT = result.current / 1000000.0f;
                //                            detailDto.CYCLER_CAPACITY = (result.current > 0) ? result.charge_ah : result.discharge_ah;
                //                            detailDto.CYCLER_CAPACITY = detailDto.CYCLER_CAPACITY / 1000000.0f;
                //			}

                //                        // 통합서버에 셀전압 업데이트
                //                        // 모듈 구성정보
                //                        string[] splited = ChannelVMInstance.SelectedBatteryInfo.Dto.CONSIST.Split(',');
                //                        List<int> moduleList = new List<int>();
                //                        foreach (string s in splited)
                //                        {
                //                            moduleList.Add(Convert.ToInt32(s));
                //                        }

                //                        var rows = new tbl_log_bi_data().Get(runId, taskSeq);
                //                        int row_idx = 0;
                //                        int offset = 1;
                //                        foreach (var row in rows)
                //                        {
                //                            if (row_idx >= moduleList.Count)
                //                            {
                //                                break;
                //                            }
                //                            //int offset = row.mbms_id * 15 - 14;
                //                            for (int i = 0; i < moduleList[row_idx]; ++i)
                //                            {
                //                                // rest api 쪽 데이터베이스에 96셀이 한계
                //                                if (offset + i > ChannelVMInstance.TaskRunInfo.TYPE_S)
                //                                {
                //                                    break;
                //                                }

                //                                float voltage = (short)row.GetType().GetProperty($"voltage{i + 1}").GetValue(row) / 1000.0f;
                //                                detailDto.GetType().GetProperty($"V{offset + i}").SetValue(detailDto, voltage);

                //                                if (offset + i == 1)
                //                                {
                //                                    float temperature = row.temperature1 / 10.0f;
                //                                    detailDto.T1 = temperature;
                //                                }
                //                            } // end for
                //                            offset += moduleList[row_idx];
                //                            row_idx += 1;
                //                        }

                //                    }
                //                    break;
                //	case LogDev.BI_BOX:
                //		{
                //                        // 통합서버에 셀전압 업데이트 
                //                        // 모듈 구성정보
                //                        string[] splited = ChannelVMInstance.SelectedBatteryInfo.Dto.CONSIST.Split(',');
                //                        List<int> moduleList = new List<int>();
                //                        foreach (string s in splited)
                //                        {
                //                            moduleList.Add(Convert.ToInt32(s));
                //                        }

                //                        var rows = new tbl_log_bi_data().Get(runId, taskSeq);
                //                        int row_idx = 0;
                //                        int offset = 1;
                //                        foreach (var row in rows)
                //			{
                //                            if (row_idx >= moduleList.Count)
                //                            {
                //                                break;
                //                            }
                //                            //int offset = row.mbms_id * 15 - 14;
                //				for (int i = 0; i < moduleList[row_idx]; ++i)
                //				{
                //					// rest api 쪽 데이터베이스에 96셀이 한계
                //					if (offset + i > ChannelVMInstance.TaskRunInfo.TYPE_S)
                //					{
                //						break;
                //					}

                //                                float voltage = (short)row.GetType().GetProperty($"voltage{i + 1}").GetValue(row) / 1000.0f;
                //                                detailDto.GetType().GetProperty($"V{offset + i}").SetValue(detailDto, voltage);

                //                                if (offset + i == 1)
                //                                {
                //                                    float temperature = row.temperature1 / 10.0f;
                //                                    detailDto.T1 = temperature;
                //                                }
                //				} // end for
                //                            offset += moduleList[row_idx];
                //                            row_idx += 1;
                //                        }
                //		}
                //		break;
                //	default:
                //		return;
                //} // end switch

                //var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;

                //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SendUpdateState, #4, ChannelNum = {ChannelNum}, {restApiLogType}, {INSPCT_SN}, {sub_step_no}");
                //Helper.SendUpdatePortal(restApiLogType, INSPCT_SN, sub_step_no, detailDto, checkDto);

                //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"SendUpdateState, #5");
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
        }

        private void BIVoltage2PotalDetail(string runId, int INSPCT_SN, int taskSeq, int step_no, TimeSpan sittn, DateTime beginTime)
        {
            byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

            var endTime = beginTime + sittn;

            //var detailDto = new pg_btry_srvive_evl_detail_DTO
            //{
            //    INSPCT_SN = INSPCT_SN,
            //    STEP_NO = $"{step_no}",
            //};

            // 모듈 구성정보
            //string[] splited = ChannelVMInstance.SelectedBatteryInfo.Dto.CONSIST.Split(',');
            //List<int> moduleList = new List<int>();
            //foreach (string s in splited)
            //{
            //    moduleList.Add(Convert.ToInt32(s));
            //}

            //var rows = new tbl_log_bi_data().Get(runId, taskSeq);
            //int row_idx = 0;
            //int offset = 1;
            //foreach (var row in rows)
            //{
            //    if (row_idx >= moduleList.Count)
            //    {
            //        break;
            //    }
            //    //int offset = row.mbms_id * 15 - 14;
            //    for (int i = 0; i < moduleList[row_idx]; ++i)
            //    {
            //        // rest api 쪽 데이터베이스에 96셀이 한계
            //        if (offset + i > ChannelVMInstance.TaskRunInfo.TYPE_S)
            //        {
            //            break;
            //        }

            //        float voltage = (short)row.GetType().GetProperty($"voltage{i + 1}").GetValue(row) / 1000.0f;
            //        detailDto.GetType().GetProperty($"V{offset + i}").SetValue(detailDto, voltage);

            //        if (offset + i == 1)
            //        {
            //            float temperature = row.temperature1 / 10.0f;
            //            detailDto.T1 = temperature;
            //        }
            //    } // end for
            //    offset += moduleList[row_idx];
            //    row_idx += 1;
            //}

            int key = 0;
            //try
            //{
            //    key = new pg_btry_srvive_evl_detail().UpdateBIVoltage(detailDto);
            //    key = new pg_btry_srvive_evl_detail().UpdateBITemperature(detailDto);
            //}
            //catch (Exception e)
            //{
            //    Console.Out.WriteLine(e.ToString());
            //}
        }



        //public async void TaskProcessingCallback(TaskStates status, int taskSeq)
        //{
        //BaseLib.Helper.LogHelper.Debug($"{ChannelVMInstance.ChannelNum}", $"TaskProcessingCallback( {status},{taskSeq} )");

        //if (CurrentTask.device_cd == LogDev.Cycler)
        //{
        //    if ( status == TaskStates.CompletedBIVolt)
        //    {
        //        BaseLib.Helper.LogHelper.Debug($"{ChannelVMInstance.ChannelNum}", "TaskProcessingCallback, CompletedBIVolt");
        //        // BI 전압 측정후 온도 측정 시작 
        //        Devices.BIBoxInstance.GetCellTemperature(ChannelVMInstance.TaskRunInfo.run_id, CurrentTask.task_id, CurrentTask.task_seq, CurrentTask.step_no);
        //    }
        //    else if (status == TaskStates.CompletedBITemp)
        //    {
        //        BIVoltage2PotalDetail(
        //            ChannelVMInstance.TaskRunInfo.run_id,
        //            ChannelVMInstance.TaskRunInfo.INSPCT_SN,
        //            CurrentTask.task_seq,
        //            CurrentTask.step_no,
        //            CurrentTask.ElapsedTime,
        //            CurrentTask.BeginTime
        //        );
        //    }
        //    else if (status == TaskStates.PauseCellOverVoltage)
        //    {
        //        CurrentTask.TaskState = TaskStates.PauseCellOverVoltage;

        //        TaskCompletedCallback(false, -1, "Cell Over Voltage", taskSeq);
        //        Devices.PNECTSInstance.SendStop(ChannelVMInstance.ChannelIndex, ChannelVMInstance.ChannelNum);

        //        if (PopupPauseSimpleIsOpen == false || PopupIsolateSimpleIsOpen == false)
        //        {
        //            PopupPauseSimpleIsOpen = true;
        //        }
        //    }
        //    else if (status == TaskStates.PauseCellUnderVoltage)
        //    {
        //        CurrentTask.TaskState = TaskStates.PauseCellUnderVoltage;

        //        TaskCompletedCallback(false, -1, "Cell Under Voltage", taskSeq);
        //        Devices.PNECTSInstance.SendStop(ChannelVMInstance.ChannelIndex, ChannelVMInstance.ChannelNum);

        //        if (PopupPauseSimpleIsOpen == false || PopupIsolateSimpleIsOpen == false)
        //        {
        //            PopupPauseSimpleIsOpen = true;
        //        }
        //    }
        //    // Modified, 20201021, 테스트 코드 }
        //}
        //else
        //{
        //    TaskCompletedCallback(true, 0, "", taskSeq);
        //}
        //}

        /// <summary>
        /// task completed callback
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public /*async*/ void TaskCompletedCallback(bool isCompleted, int code, string reason, int TaskSeq)
        {
            byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #START");

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] CheckOverVoltageStop()");
            //Devices.BIBoxInstance.CheckOverVoltageStop();

            if (isCompleted)
            {
                cancelTokenSource.Cancel();
                //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}], TaskTimer Cancel()");
                System.Threading.Thread.Sleep(100);
            }

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] ({CurrentTask.task_id},{CurrentTask.step_no},{CurrentTask.task_seq}), TaskCompleted({isCompleted},{code},{reason},{TaskSeq})");
            //Console.Out.WriteLine($"TRACE : TaskCompletedCallback( {isCompleted},{code},{reason},{TaskSeq} ), {IsTaskTimerEnabled}");

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #1");

            //if ( IsTaskTimerEnabled == false && CurrentTaskDescription.Equals("Timeout") )
            //{
            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #END 1");
            //    return;
            //}

            //if (CurrentTask.task_seq == TaskSeq )
            //{
            //if (CurrentTask.TaskState == TaskStates.Pause
            //   || CurrentTask.TaskState == TaskStates.PauseCellOverVoltage
            //   || CurrentTask.TaskState == TaskStates.PauseCellUnderVoltage)
            //{
            //    if (PopupPauseSimpleIsOpen == false || PopupIsolateSimpleIsOpen == false)
            //    {
            //        PopupPauseSimpleIsOpen = true;
            //    }
            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #END 2");
            //    return;
            //}
            //}

            IsTaskTimerEnabled = false;
            //await Task.Delay(200);

            if (reason != null && reason.Length > 0)
            {
                if (code == 0)
                {
                    CurrentTaskDescription = $"{reason} ";
                }
                else {
                    CurrentTaskDescription = $"{reason} ({code})";
                }
            }


            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #3");

            //         if (isCompleted)
            //{
            //if ( TaskSeq > -1 && CurrentTask.task_seq != TaskSeq )
            //{
            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}], task_seq not match ===> {CurrentTask.task_seq},{TaskSeq}");
            //if (CurrentTask.TaskState != TaskStates.Completed)
            //{
            //    CurrentTaskDescription = "TASK 중지";

            //    BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #END 3");
            //    return;
            //}
            //}

            //            BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #4");

            //            CurrentTask.TaskState = TaskStates.Completed;
            //CurrentTask.ElapsedTime = DateTime.Now - CurrentTask.BeginTime;

            //            BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #8");
            //            new tbl_task_run_detail().Update(new tbl_task_run_detail_DTO
            //{
            //	run_id = ChannelVMInstance.TaskRunInfo.run_id,
            //	task_seq = CurrentTask.task_seq,
            //	state = "completed",
            //	end_dt = DateTime.Now
            //});

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #9");
            //await Task.Delay(1000);

            //if (CurrentTask.step_no > 0)
            //{
            // 타스크 처리 결과 등록
            //SendUpdateState(
            //    ChannelVMInstance.TaskRunInfo.run_id,
            //    ChannelVMInstance.TaskRunInfo.INSPCT_SN,
            //    CurrentTask.task_id,
            //    CurrentTask.task_seq,
            //    CurrentTask.step_no,
            //    CurrentTask.sub_step_no,
            //    CurrentTask.task_detail_name,
            //    $"{CurrentTask.device_cd}",
            //    CurrentTask.task_condition,
            //    CurrentTask.ElapsedTime,
            //    CurrentTask.BeginTime
            //);
            //}

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #10");

            // 다음 타스크
            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}], RunTask({CurrentTask.task_id},{CurrentTask.step_no},{CurrentTask.task_seq}), -> call NextJob()");
            ////SelectTask(CurrentTask.step_no, CurrentTask.task_seq);
            //NextJob = (CurrentTask.step_no, CurrentTask.task_seq);

            //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #11");
            //         }
            //else
            //{
            // Task 완료 - 실패

            //            if (CurrentTask != null)
            //{
            //	//CurrentTask.TaskState = TaskStates.Pause;
            //	CurrentTask.ElapsedTime = new TimeSpan(0, 0, 0);

            //                if (CurrentTask.task_seq == TaskSeq)
            //                {
            //if (CurrentTask.TaskState == TaskStates.Pause)
            //{
            //    if (reason.Equals("충방전기 Pause") )
            //    {
            //        //Devices.PNECTSInstance.SendStop(ChannelVMInstance.ChannelIndex, ChannelVMInstance.ChannelNum);
            //    }

            //    if (PopupPauseSimpleIsOpen == false || PopupIsolateSimpleIsOpen == false)
            //    {
            //        PopupPauseSimpleIsOpen = true;
            //    }
            //    //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #END 4");
            //    return;
            //}
            //}

            //               new tbl_task_run_detail().Update(new tbl_task_run_detail_DTO
            //{
            //	run_id = ChannelVMInstance.TaskRunInfo.run_id,
            //	task_seq = CurrentTask.task_seq,
            //	state = $"{CurrentTask.TaskState}",
            //	end_dt = DateTime.Now
            //});
            //} // end if

            //new tbl_task_run().UpdateTaskCompleted(new tbl_task_run_DTO
            //{
            //	run_id = ChannelVMInstance.TaskRunInfo.run_id,
            //	completed_dt = DateTime.Now
            //});

            IsTaskRunning = false;

            if (CurrentTaskDescription == null || CurrentTaskDescription.Length == 0)
            {
                CurrentTaskDescription = "장비 연결이 해제되어 진단 검사를 중지합니다.";
            }

            //await Task.Delay(1000);
        }

        //BaseLib.Helper.LogHelper.Debug($"{ChannelNum}", $"TaskCompletedCallback [{TaskSeq}] #END");

    //}

    //private void PublichReceivedACIRMeasureData(ACIRLib.Data.DTO.MeasureDataAckDTO dto)
    //{
    //	CurrentTaskDescription = $"[ACIA] {dto.Hz}Hz";
    //}

    //#endregion

    //private bool m_PopupPauseSimpleIsOpen = false;
    //public bool PopupPauseSimpleIsOpen
    //{
    //    get
    //    {
    //        return m_PopupPauseSimpleIsOpen;
    //    }
    //    set
    //    {
    //        m_PopupPauseSimpleIsOpen = value;
    //        //RaisePropertyChanged("PopupPauseSimpleIsOpen");
    //    }
    //}


    //public DelegateCommand PopupIsolateSimpleIsOpenCommand
    //{
    //    get
    //    {
    //        return new DelegateCommand(delegate ()
    //        {
    //            // 분리점검 실행 팝업
    //            PopupIsolateSimpleIsOpen = true;
    //            PopupPauseSimpleIsOpen = false;
    //        });
    //    }
    //}

    //public DelegateCommand PopupNextStepCommand
    //{
    //    get
    //    {
    //        return new DelegateCommand(delegate ()
    //        {
    //            //PopupIsolateSimpleIsOpen = true;
    //            // 다음공정 실행
    //            PopupPauseSimpleIsOpen = false;
    //            PopupIsolateSimpleIsOpen = false;

    //            IsTaskRunning = true;
    //            if (CurrentTask == null)
    //            {
    //                //SelectTask(-1, -1);
    //                NextJob = (-1, -1);
    //            }
    //            else
    //            {
    //                //CurrentTask.TaskState = TaskStates.Completed;
    //                //SelectTask(CurrentTask.step_no, CurrentTask.task_seq);
    //                NextJob = (CurrentTask.step_no, CurrentTask.task_seq);
    //            }
    //        });
    //    }
    //}


    /*
     *  분리점검 팝업오픈
     */
    //private bool m_PopupIsolateSimpleIsOpen = false;
    //public bool PopupIsolateSimpleIsOpen
    //{
    //    get
    //    {
    //        return m_PopupIsolateSimpleIsOpen;
    //    }
    //    set
    //    {
    //        m_PopupIsolateSimpleIsOpen = value;
    //        //RaisePropertyChanged("PopupIsolateSimpleIsOpen");
    //    }
    //}


    //public DelegateCommand PrevPausePopupMoveCommand
    //{
    //    get
    //    {
    //        return new DelegateCommand(delegate ()
    //        {
    //            PopupPauseSimpleIsOpen = true;
    //            PopupIsolateSimpleIsOpen = false;
    //        });
    //    }
    //}

    //public DelegateCommand NextIsolateMoveCommand
    //{
    //    get
    //    {
    //        return new DelegateCommand(delegate ()
    //        {
    //            //PopupIsolateSimpleIsOpen = true;
    //        });
    //    }
    //}

    }

}
