using BaseLib.Helper;
using InspectEngineLib.Defines;
using RestApiLib.Data;
using RestApiLib.Defines;
using SQLManager.Data.DTO;
using SQLManager.Data.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiLib.Core
{
	public class Helper
	{



		// 검사시작을 알려주는 API 호출
		public static int SendStartTest(RestApiLogTypes logType, byte channelNo, InspectionTypes inspectionType, string INSPCT_BTRY, pg_btry_info_DTO batteryInfo, string barcode, string biHostName, int taskCount, string loginId)
		{
			int key = 0;

			try
			{
				DateTime INSPCT_BEGIN_DT = DateTime.Now;
				DateTime INSPCT_EXPECT_END_DT;
				string INSPCT_TY = "";
				switch (inspectionType)
				{
					case InspectionTypes.Normal:
						INSPCT_TY = "STANDARD";
						INSPCT_EXPECT_END_DT = INSPCT_BEGIN_DT.AddMinutes(50);
						break;
					case InspectionTypes.Close:
						INSPCT_TY = "PRECISION";
						INSPCT_EXPECT_END_DT = INSPCT_BEGIN_DT.AddMinutes(400);
						break;
					default:
						INSPCT_TY = "FAST";
						INSPCT_EXPECT_END_DT = INSPCT_BEGIN_DT.AddMinutes(10);
						break;
				} // end switch

				key = new pg_btry_srvive_evl_start().Insert(new pg_btry_srvive_evl_start_DTO
				{
					INSPCT_BTRY = INSPCT_BTRY,
					INSPCT_MAKR = $"{batteryInfo.MARK_CODE}",       // Fiexed, Field 'INSPCT_MKKR' in the table pg_btry_srvive_evl_start
                    INSPCT_MODL = batteryInfo.MODEL_CODE,
					INSPCT_CONFIG = batteryInfo.CONFIG,
					BRCD_NO = barcode,
					VHCLE_MAKR_TY = batteryInfo.MAKR_DESC,
					VHCLE_MODL_TY = batteryInfo.MODL_DESC,
					VLTGE = batteryInfo.VLTGE,
					CPCTY = batteryInfo.CPCTY,
					MUMM_VLTGE = batteryInfo.MUMM_VLTGE,
					MXMM_VLTGE = batteryInfo.MXMM_VLTGE,
					BTRY_ENERGY = batteryInfo.BTRY_ENERGY,
					EQPMN_IP = biHostName,
					CHANNEL = $"{channelNo}",
					CNNC_EQPMN = "BI",
					INSPCT_TY = INSPCT_TY,
					INSPCT_BEGIN_DT = string.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}",
						INSPCT_BEGIN_DT.Year, INSPCT_BEGIN_DT.Month, INSPCT_BEGIN_DT.Day,
						INSPCT_BEGIN_DT.Hour, INSPCT_BEGIN_DT.Minute, INSPCT_BEGIN_DT.Second),
					INSPCT_EXPECT_END_DT = string.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}",
						INSPCT_EXPECT_END_DT.Year, INSPCT_EXPECT_END_DT.Month, INSPCT_EXPECT_END_DT.Day,
						INSPCT_EXPECT_END_DT.Hour, INSPCT_EXPECT_END_DT.Minute, INSPCT_EXPECT_END_DT.Second),
					INSPCT_COMPT_STEP = taskCount,
					LOGIN_ID = loginId
				});

				RestApi.SendStartTest(logType, RestApiConfig.Instance.DeviceNo, key);
			}
			catch (Exception e)
			{
				Console.Out.WriteLine(e.ToString());
			}

			return key;
		}

		// 검사 결과 등록 및 상태정보 갱신을 알려주는 API 호출
		public static int SendUpdatePortal(RestApiLogTypes logType, int INSPCT_SN, int detailDtoMode, pg_btry_srvive_evl_detail_DTO detailDto, pg_btry_srvive_evl_check_DTO checkDto)
		{
			int key = 0;

			try
			{
                switch (detailDtoMode)
                {
                case 0: // INSERT
                    key = new pg_btry_srvive_evl_detail().Insert(detailDto);
                    new pg_btry_srvive_evl_check().Insert(checkDto);
                    RestApi.SendUpdateState(logType, RestApiConfig.Instance.DeviceNo, INSPCT_SN);
                    break;
                case 1: // BI Voltage Update
                    key = new pg_btry_srvive_evl_detail().UpdateBIVoltage(detailDto);
                    break;
                case 2: // BI Temperature Update
                    key = new pg_btry_srvive_evl_detail().UpdateBITemperature(detailDto);
                    break;
                }

			}
			catch (Exception e)
			{
				Console.Out.WriteLine(e.ToString());
			}

			return key;
		}

		// 작업 수행단계
		public static void SendStep(RestApiLogTypes logType, pg_btry_srvive_evl_input_DTO dto)
		{
			try
			{
				int key = new pg_btry_srvive_evl_input().Insert(dto);
				RestApi.SendStep(logType, RestApiConfig.Instance.DeviceNo, key);
			}
			catch (Exception e)
			{
				Console.Out.WriteLine(e.ToString());
			}
		}

		// 검사완료 및 검사결과 등록을 알려주는 API 호출
		public static void SendRegResult(RestApiLogTypes logType, int INSPCT_SN, pg_btry_srvive_evl_end_DTO dto)
		{
			try
			{
				new pg_btry_srvive_evl_end().Insert(dto);
				RestApi.SendRegResult(logType, RestApiConfig.Instance.DeviceNo, INSPCT_SN);
			}
			catch (Exception e)
			{
				Console.Out.WriteLine(e.ToString());
			}
		}

	}
}
