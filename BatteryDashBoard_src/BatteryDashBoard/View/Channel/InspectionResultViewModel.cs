//using InspectEngineLib.Defines;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BatteryDashBoard.View.Channel
{
	public class InspectionResultViewModel : AChannelBaseViewModel
	{
		#region property

		/// <summary>
		/// 진단 결과 원본 데이터
		/// </summary>
		//private InspectionResultDTO InspectionResult { get; set; }

		/// <summary>
		/// 진단 결과 가공 데이터
		/// </summary>
		//private EvaluationResultVM m_EvaluationResult;
		//public EvaluationResultVM EvaluationResult
		//{
		//	get
		//	{
		//		return m_EvaluationResult;
		//	}
		//	set
		//	{
		//		m_EvaluationResult = value;
		//		RaisePropertyChanged("EvaluationResult");
		//	}
		//}

		#endregion

		#region method

		public override void DoLoaded()
		{
			base.DoLoaded();

			Task.Run(() =>
			{
                byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;
                //BaseLib.Helper.LogHelper.Debug($"0", $"InspectionResultViewModel::DoLoaded(), ch = {ChannelNum}, run_id = {ChannelVMInstance.TaskRunInfo.run_id}");

                // 수집 데이터 로드
    //            InspectionResult = InspectionResultDTO.Create(ChannelVMInstance.TaskRunInfo.run_id);
				//if (InspectionResult == null)
				//{
				//	MessageBox.Show("진단이 완료되지 않아서 평가 결과를 생성할 수 없습니다.");
				//	return;
				//}

				// 평가 정보 생성
				//var engine = InspectEngineLib.Core.Engine.Build(InspectionResult);

				// 평가 결과 저장
				//new tbl_task_result().SaveOrUpdate(new tbl_task_result_DTO
				//{
				//	run_id = InspectionResult.TaskInfo.run_id,
				//	evaluation_dt = InspectionResult.TaskInfo.evaluation_dt,
				//	evaluation_type = InspectionResult.TaskInfo.task_name,
				//	time_required = InspectionResult.TaskInfo.completed_dt - InspectionResult.TaskInfo.evaluation_dt,
				//	grade = $"{engine.Grade}",
				//	soc = engine.SOC,
				//	soh = engine.SOH,
				//	sob = engine.SOB,
				//	sop = engine.SOP,
				//	BTRY_CODE = InspectionResult.TaskInfo.BTRY_CODE,
				//	BTRY_TY = InspectionResult.TaskInfo.BTRY_TY,
				//	MAKR_DESC = InspectionResult.TaskInfo.MAKR_DESC,
				//	MARK_CODE = InspectionResult.TaskInfo.MARK_CODE,
				//	MODEL_CODE = InspectionResult.TaskInfo.MODEL_CODE,
				//	MODL_DESC = InspectionResult.TaskInfo.MODL_DESC,
				//	CONFIG = InspectionResult.TaskInfo.CONFIG
				//});

				// 리포트 생성
				//EvaluationResult = new EvaluationResultVM
				//{
				//	Barcode = InspectionResult.TaskInfo.barcode,
				//	BatteryConfig = InspectionResult.TaskInfo.CONFIG,
				//	BatteryMaker = InspectionResult.TaskInfo.MAKR_DESC,
				//	ModelDesc = InspectionResult.TaskInfo.MODL_DESC,
				//	EvaluationDate = InspectionResult.TaskInfo.evaluation_dt,
				//	EvaluationType = InspectionResult.TaskInfo.task_name,
				//	BeginDate = InspectionResult.TaskInfo.evaluation_dt,
				//	EndDate = InspectionResult.TaskInfo.completed_dt,
				//	TimeRequired = InspectionResult.TaskInfo.completed_dt - InspectionResult.TaskInfo.evaluation_dt,
				//	SOC = string.Format("{0:0.00}%", engine.SOC),
				//	SOH = string.Format("{0:0.00}%", engine.SOH),
				//	SOB = string.Format("{0:0.00}%", engine.SOB),
				//	SOP = string.Format("{0:0.00}%", engine.SOP),
				//	Grade = engine.Grade
				//};

				//byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;
				//var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;

                // RestApi : EVAL_STEP.GRADE - 진단 등급 출력
    //            RestApiLib.Core.Helper.SendStep(restApiLogType, new pg_btry_srvive_evl_input_DTO
				//{
				//	STEP_NO = $"{(int)EVAL_STEP.GRADE}",
				//	CHANNEL = $"{ChannelNum}",
				//	INPUT_VALUE = $"SOC:{EvaluationResult.SOC};SOH:{EvaluationResult.SOH};SOB:{EvaluationResult.SOB};SOP:{EvaluationResult.SOP};GRADE:{EvaluationResult.Grade}",
				//	LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
				//});

                // RestApi : 완료 rest api
    //            RestApiLib.Core.Helper.SendRegResult(restApiLogType, InspectionResult.TaskInfo.INSPCT_SN, new pg_btry_srvive_evl_end_DTO
				//{
				//	INSPCT_SN = InspectionResult.TaskInfo.INSPCT_SN,
				//	INSPCT_REQRE_TIME = string.Format("{0:D2}:{1:D2}:{2:D2}", 
				//		EvaluationResult.TimeRequired.Hours, EvaluationResult.TimeRequired.Minutes, EvaluationResult.TimeRequired.Seconds),
				//	INSPCT_END_DT = string.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}",
				//		EvaluationResult.EndDate.Year, EvaluationResult.EndDate.Month, EvaluationResult.EndDate.Day,
				//		EvaluationResult.EndDate.Hour, EvaluationResult.EndDate.Minute, EvaluationResult.EndDate.Second),
				//	INSPCT_GRAD_TY = $"{EvaluationResult.Grade}",
				//	SOC = engine.SOC,
				//	SOH = engine.SOH,
				//	SOB = engine.SOB,
				//	SOP = engine.SOP
				//});
			});
		}

		#endregion
	}
}
