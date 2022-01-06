using InspectEngineLib.Data;
using InspectEngineLib.Defines;
using SQLManager.Data;
using SQLManager.Data.DTO;
using SQLManager.Data.Query;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectEngineLib.Core
{
	public class Engine
	{
        #region property

        public float SOC { get; private set; }

        public float SOH { get; private set; }

        public float SOB { get; private set; }

        public float SOP { get; private set; }

        public GradeTypes Grade { get; private set; }

        #endregion

        #region constructor

        private Engine(InspectionResultDTO dto)
        {
            BuildReport(dto);
        }

        public static Engine Build(InspectionResultDTO dto)
        {
            return new Engine(dto);
        }

        #endregion

        #region method


        private Dictionary<TaskConditions, InspectionStepResultDTO> GetBIData(InspectionResultDTO dto, int task_group, TaskGroupTags tag)
        {
            var result = new Dictionary<TaskConditions, InspectionStepResultDTO>();
            result.Add(TaskConditions.Voltage, null);
            result.Add(TaskConditions.Temperature, null);

            Console.Out.WriteLine($"{dto.TaskInfo.run_id}");

            var discharges = dto.TaskStepDataList.Where(p => $"{LogDev.Cycler}".Equals(p.TaskStepInfo.device_cd)
                                                             && StringEnum.GetStringValue(TaskConditions.Discharge).Equals(p.TaskStepInfo.task_condition));
            if (discharges.Count() > 0)
            {
                //var discharge = discharges.ElementAt(0);
#if true
                int idx = 0;
                foreach (InspectionStepResultDTO pp in dto.TaskStepDataList)
                {
                    Console.Out.WriteLine($"[{idx}] task_seq = {pp.TaskStepInfo.task_seq}, task_group = {pp.TaskStepInfo.task_group}, task_tag = {pp.TaskStepInfo.task_tag}, device_cd = {pp.TaskStepInfo.device_cd}, task_contition = {pp.TaskStepInfo.task_condition} ");

                    if (   pp.TaskStepInfo.task_group == task_group
                        && StringEnum.GetStringValue(tag).Equals(pp.TaskStepInfo.task_tag)
                        && $"{LogDev.BI_BOX}".Equals(pp.TaskStepInfo.device_cd)
                        && StringEnum.GetStringValue(TaskConditions.Voltage).Equals(pp.TaskStepInfo.task_condition))
                    {
                        result[TaskConditions.Voltage] = pp;
                        Console.Out.WriteLine("********** ok Voltage");
                    }

                    if (pp.TaskStepInfo.task_group == task_group
                        && StringEnum.GetStringValue(tag).Equals(pp.TaskStepInfo.task_tag)
                        && $"{LogDev.BI_BOX}".Equals(pp.TaskStepInfo.device_cd)
                        && StringEnum.GetStringValue(TaskConditions.Temperature).Equals(pp.TaskStepInfo.task_condition))
                    {
                        result[TaskConditions.Temperature] = pp;
                        Console.Out.WriteLine("********** ok Temperature");
                    }
                    idx += 1;
                }

#else
                var voltages = dto.TaskStepDataList.Where(p => p.TaskStepInfo.task_group == task_group && StringEnum.GetStringValue(tag).Equals(p.TaskStepInfo.task_tag) 
                                                    && $"{LogDev.BI_BOX}".Equals(p.TaskStepInfo.device_cd) 
                                                    && StringEnum.GetStringValue(TaskConditions.Voltage).Equals(p.TaskStepInfo.task_condition));
                if (voltages.Count() > 0)
                {
                    result[TaskConditions.Voltage] = voltages.ElementAt(0);
                }

                var temperatures = dto.TaskStepDataList.Where(p => p.TaskStepInfo.task_group == task_group 
                                                            && StringEnum.GetStringValue(tag).Equals(p.TaskStepInfo.task_tag) 
                                                            && $"{LogDev.BI_BOX}".Equals(p.TaskStepInfo.device_cd) 
                                                            && StringEnum.GetStringValue(TaskConditions.Temperature).Equals(p.TaskStepInfo.task_condition));
                if (temperatures.Count() > 0)
                {
                    result[TaskConditions.Temperature] = temperatures.ElementAt(0);
                }
#endif
            } // end if

            return result;
        }

        private Dictionary<TaskConditions, InspectionStepResultDTO> GetBIData(InspectionResultDTO dto, int step_no)
        {
            var result = new Dictionary<TaskConditions, InspectionStepResultDTO>();
            result.Add(TaskConditions.Voltage, null);
            result.Add(TaskConditions.Temperature, null);

            var stepJobs = dto.TaskStepDataList.Where(p => p.TaskStepInfo.step_no == step_no);
            if (stepJobs.Count() > 0)
            {
                var voltages = dto.TaskStepDataList.Where(p => p.TaskStepInfo.step_no == step_no
                                                         && $"{LogDev.BI_BOX}".Equals(p.TaskStepInfo.device_cd)
                                                         && StringEnum.GetStringValue(TaskConditions.Voltage).Equals(p.TaskStepInfo.task_condition));
                if (voltages.Count() > 0)
                {
                    result[TaskConditions.Voltage] = voltages.ElementAt(0);
                }

                var temperatures = dto.TaskStepDataList.Where(p => p.TaskStepInfo.step_no == step_no
                                                          && $"{LogDev.BI_BOX}".Equals(p.TaskStepInfo.device_cd)
                                                          && StringEnum.GetStringValue(TaskConditions.Temperature).Equals(p.TaskStepInfo.task_condition));
                if (temperatures.Count() > 0)
                {
                    result[TaskConditions.Temperature] = temperatures.ElementAt(0);
                }
            } // end if

            return result;
        }


        private Dictionary<TaskConditions, InspectionStepResultDTO> GetBIData(InspectionResultDTO dto, int step_no, string device_cd)
        {
            var result = new Dictionary<TaskConditions, InspectionStepResultDTO>();
            result.Add(TaskConditions.Voltage, null);
            result.Add(TaskConditions.Temperature, null);

            var stepJobs = dto.TaskStepDataList.Where(p => p.TaskStepInfo.step_no == step_no);
            if (stepJobs.Count() > 0)
            {
                var voltages = dto.TaskStepDataList.Where(p => p.TaskStepInfo.step_no == step_no
                                                         && device_cd.Equals(p.TaskStepInfo.device_cd));
                if (voltages.Count() > 0)
                {
                    result[TaskConditions.Voltage] = voltages.ElementAt(0);
                }

                var temperatures = dto.TaskStepDataList.Where(p => p.TaskStepInfo.step_no == step_no
                                                          && device_cd.Equals(p.TaskStepInfo.device_cd));
                if (temperatures.Count() > 0)
                {
                    result[TaskConditions.Temperature] = temperatures.ElementAt(0);
                }
            } // end if

            return result;
        }

        /// <summary>
        /// 리포트 생성
        /// </summary>
        private void BuildReport(InspectionResultDTO dto)
        {
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", "---------------------------------------------\n");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"INSPCT_SN   = {dto.TaskInfo.INSPCT_SN}");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"BATTRY TYPE = {dto.TaskInfo.BTRY_TY}");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"BATTRY CODE = {dto.TaskInfo.BTRY_CODE}");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"BARCODE     = {dto.TaskInfo.barcode}");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"run_id      = {dto.TaskInfo.run_id}");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"task_id     = {dto.TaskInfo.task_id}");

            // 진단 검사 타입
            InspectionTypes inspectType = InspectionTypeConverter.Convert(dto.TaskInfo.task_type);

            // 배터리 코드
            string BTRY_CODE = dto.TaskInfo.BTRY_CODE;

            // 방전 전 데이터
            Dictionary<TaskConditions, InspectionStepResultDTO> dataBeforeDischarge = null;

            // 방전 후 데이터
            Dictionary<TaskConditions, InspectionStepResultDTO> dataAfterDischarge = null;

            // BI 데이터 가져오기
            switch (inspectType)
            {
                case InspectionTypes.Simple:
                case InspectionTypes.Normal:
                    dataBeforeDischarge = GetBIData(dto, 6);
                    dataAfterDischarge = GetBIData(dto, 7, "Cycler");
                    break;
                case InspectionTypes.Close:
                    dataBeforeDischarge = GetBIData(dto, 15);
                    dataAfterDischarge = GetBIData(dto, 16, "Cycler");
                    break;
                default:
                    break;
            } // end switch



            // 최초 전압
            float firstVoltage = dto.PneCtsMeasureData.voltage_first / 1000000.0f;

            // 최종 전압
            float lastVoltage = dto.PneCtsMeasureData.voltage_last / 1000000.0f;




            float voltageBeforeDischarge = dto.PneCtsMeasureData.voltage_before_discharge / 1000000.0f; // 방전 전 전압
            float voltageAfterDischarge = dto.PneCtsMeasureData.voltage_after_discharge / 1000000.0f; // 방전 후 전압

            float capacityDischarge = 0.0f;

            float voltageBeforeDischarge_soh = dto.PneCtsMeasureData.voltage_before_discharge2 / 1000000.0f;
            float voltageAfterDischarge_soh = dto.PneCtsMeasureData.voltage_after_discharge2 / 1000000.0f;

            int DISCHARGE_STEP_NO = 7;
            int CAPACITY_DISCHARGE_STEP_NO = 0;
            switch (inspectType)
            {
                case InspectionTypes.Simple:
                    {
                        DISCHARGE_STEP_NO = 7;
                        var result = new tbl_log_cts_chdata().GetLastData(dto.TaskInfo.run_id, dto.BatteryInfo.BTRY_CODE, dto.TaskInfo.task_id, DISCHARGE_STEP_NO);
                        if (result != null)
                        {
                            voltageAfterDischarge = result.voltage / 1000000.0f;
                            capacityDischarge = result.discharge_ah / 1000000.0f;

                            voltageBeforeDischarge_soh = voltageBeforeDischarge;
                            voltageAfterDischarge_soh = voltageAfterDischarge;
                        }
                    }
                    break;
                case InspectionTypes.Normal:
                    {
                        DISCHARGE_STEP_NO = 7;
                        var result = new tbl_log_cts_chdata().GetLastData(dto.TaskInfo.run_id, dto.BatteryInfo.BTRY_CODE, dto.TaskInfo.task_id, DISCHARGE_STEP_NO);
                        if (result != null)
                        {
                            voltageAfterDischarge = result.voltage / 1000000.0f;
                            capacityDischarge = result.discharge_ah / 1000000.0f;
                        }
                        DISCHARGE_STEP_NO = 17;

                        var result2 = new tbl_log_cts_chdata().GetLastData(dto.TaskInfo.run_id, dto.BatteryInfo.BTRY_CODE, dto.TaskInfo.task_id, DISCHARGE_STEP_NO);
                        if (result2 != null)
                        {
                            voltageAfterDischarge_soh = result2.voltage / 1000000.0f;
                        }
                    }
                    break;
                case InspectionTypes.Close:
                    DISCHARGE_STEP_NO = 16;  // org
                    CAPACITY_DISCHARGE_STEP_NO = 6;
                    {
                        var result_v = new tbl_log_cts_chdata().GetLastData(dto.TaskInfo.run_id, dto.BatteryInfo.BTRY_CODE, dto.TaskInfo.task_id, DISCHARGE_STEP_NO);
                        if (result_v != null)
                        {
                            voltageAfterDischarge = result_v.voltage / 1000000.0f;
                        }

                        var result_c = new tbl_log_cts_chdata().GetLastData(dto.TaskInfo.run_id, dto.BatteryInfo.BTRY_CODE, dto.TaskInfo.task_id, CAPACITY_DISCHARGE_STEP_NO);
                        if (result_c != null)
                        {
                            capacityDischarge = result_c.discharge_ah / 1000000.0f;
                        }
                    }
                    break;
                default:
                    break;
            } // end switch

            // Portal sync
            {
                if (firstVoltage > 1)
                {
                    var detailDtoFirst = new pg_btry_srvive_evl_detail_DTO
                    {
                        INSPCT_SN = dto.TaskInfo.INSPCT_SN,
                        STEP_NO = $"{1}",
                        CYCLER_V = firstVoltage
                    };
                    int key0 = new pg_btry_srvive_evl_detail().PatchPortalCyclerVoltage(detailDtoFirst);
                }

                // 통합서버 2c 방전전 전압 업데이트
                var detailDto1 = new pg_btry_srvive_evl_detail_DTO
                {
                    INSPCT_SN = dto.TaskInfo.INSPCT_SN,
                    STEP_NO = $"{DISCHARGE_STEP_NO - 1}",
                    CYCLER_V = voltageBeforeDischarge
                };
                int key1 = new pg_btry_srvive_evl_detail().PatchPortalCyclerVoltage(detailDto1);

                if ( inspectType == InspectionTypes.Normal )
                {
                    int SOH_DISCHARGE_STEP_NO = 17;

                    var detailDto2 = new pg_btry_srvive_evl_detail_DTO
                    {
                        INSPCT_SN = dto.TaskInfo.INSPCT_SN,
                        STEP_NO = $"{SOH_DISCHARGE_STEP_NO}",
                        CYCLER_V = voltageAfterDischarge_soh
                    };
                    int key2 = new pg_btry_srvive_evl_detail().PatchPortalCyclerVoltage(detailDto2);
                }
            }

            // 방전 후 전류
            float currentAfterDischarge = 0f;
            var tasksOfDischarge = dto.TaskStepDataList.Where(p => StringEnum.GetStringValue(TaskConditions.Discharge).Equals(p.TaskStepInfo.task_condition));
            if (tasksOfDischarge.Count() > 0)
            {
                int taskSeqOfDischarge = tasksOfDischarge.ElementAt(0).TaskStepInfo.task_seq;
                currentAfterDischarge = new tbl_log_cts_chdata().GetCurrentAfterDischarge(dto.TaskInfo.run_id, taskSeqOfDischarge);
            }
            
            // 방전 후 온도
            float tempAfterDischarge = 0f;
            if (dataAfterDischarge[TaskConditions.Temperature] != null && dataAfterDischarge[TaskConditions.Temperature].BIBoxDataList.Count > 0)
            {
                tempAfterDischarge = dataAfterDischarge[TaskConditions.Temperature].BIBoxDataList.ElementAt(0).temperature1 / 10.0f;
            }

            // calculate each values
            SOC = GetSOC(dto, lastVoltage);
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOC, BTRY_CODE : {dto.BatteryInfo.BTRY_CODE}, voltage : {lastVoltage}");

            SOH = GetSOH(dto, voltageBeforeDischarge_soh, voltageAfterDischarge_soh, currentAfterDischarge, tempAfterDischarge, capacityDischarge);
            SOP = GetSOP(dto, voltageBeforeDischarge, voltageAfterDischarge, currentAfterDischarge, tempAfterDischarge);
            SOB = GetSOB(dto, dataBeforeDischarge[TaskConditions.Voltage], dataAfterDischarge[TaskConditions.Voltage], currentAfterDischarge);
            Console.Out.WriteLine($"SOB = {SOB}");



            // 등급 판정
            float TOTAL = GetTotal(SOC, SOH, SOB, SOP);
            Grade = GenerateGrade(TOTAL);

            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"RESULT, SOC : {string.Format("{0:f2}", SOC)} ({EngineConfig.Instance.Evaluation.SOC}%)" 
                                                                             + $", SOH : {string.Format("{0:f2}", SOH)} ({EngineConfig.Instance.Evaluation.SOH}%)"
                                                                             + $", SOP : {string.Format("{0:f2}", SOP)} ({EngineConfig.Instance.Evaluation.SOP}%)"
                                                                             + $", SOB : {string.Format("{0:f2}", SOB)} ({EngineConfig.Instance.Evaluation.SOB}%)"
                                                                             + $"  ===>  Total : {string.Format("{0:f2}", TOTAL)} [ {Grade} ]");
        }

        private float CheckValidation(float value)
        {
            if (float.IsNaN(value)) return 0.0f;
            if (value <= 0) return 0.0f;
            if (value >= 100) return 100.0f;
            return value;
        }

        private float GetSOC(InspectionResultDTO dto, float voltage)
        {
            return new pg_brty_soc().Calc(dto.BatteryInfo.BTRY_CODE, voltage);
        }

        private float GetSOHForSimple(InspectionResultDTO dto, float voltageBeforeDischarge, float voltageAfterDischarge, float currentAfterDischarge, float tempAfterDischarge)
        {

            float volStep6 = voltageBeforeDischarge;


            float volStep7 = voltageAfterDischarge;


            float current = currentAfterDischarge;


            float ir = (volStep7 - volStep6) / current * 1000;


            float fTemp = tempAfterDischarge;


            float d = dto.BatteryInfo.COEFF_D;
            float ir2 = ir + d * fTemp;



            float a = dto.BatteryInfo.COEFF_A;


            // SOH 계산
            float soh = a * 100.0f / ir2;

            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOH, voltage : [{string.Format("{0:f3}", volStep6)}, {string.Format("{0:f3}", volStep7)}], current : {string.Format("{0:f3}", current)}, temp : {string.Format("{0:f1}", fTemp)}, const A : {a}, const D : {d}");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOH, SOH : {string.Format("{0:f2}", soh)}");

            return soh;
        }

        private float GetSOHForNormal(InspectionResultDTO dto, float voltageBeforeDischarge, float voltageAfterDischarge)
        {
            float capacityNominal = dto.BatteryInfo.CPCTY;
            if (capacityNominal == 0) return 0f;

            float fVolInit = voltageBeforeDischarge;
            float socInit = GetSOC(dto, fVolInit);

            float fVolFinal = Math.Abs(voltageAfterDischarge);
            float socFinal = GetSOC(dto, fVolFinal);

            if (socInit == socFinal) return 0f;

            // SOH 계산
            float soh = (capacityNominal * 0.1f * 2) / (Math.Abs(socInit - socFinal) / 100 * capacityNominal) * 100;

            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOH, capacity : {capacityNominal}");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOH, soc1 : {socInit}, volt : {string.Format("{0:f3}", fVolInit)}");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOH, soc2 : {socFinal}, volt : {string.Format("{0:f3}", fVolFinal)}");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOH, SOH : {string.Format("{0:f2}", soh)}");

            return soh;
        }

        private float GetSOHForClose(InspectionResultDTO dto, float capacityDischarge)
        {
            float capacity = Math.Abs(capacityDischarge);

            // SOH 계산
            float soh = capacity * 100.0f / dto.BatteryInfo.CPCTY;

            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOH, capacity : {capacity} , normal capacity : {dto.BatteryInfo.CPCTY}");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOH, SOH : {string.Format("{0:f2}", soh)}");

            return soh;
        }


        private float GetSOH(InspectionResultDTO dto, float voltageBeforeDischarge, float voltageAfterDischarge, /* SIMPLE ONLY */ float currentAfterDischarge, /* SIMPLE ONLY */ float tempAfterDischarge, /* CLOSE ONLY */ float capacityDischarge)
        {
            float soh = 0f;

            InspectionTypes inspectType = InspectionTypeConverter.Convert(dto.TaskInfo.task_type);
            switch (inspectType)
            {
                case InspectionTypes.Simple:
                    soh = GetSOHForSimple(dto, voltageBeforeDischarge, voltageAfterDischarge, currentAfterDischarge, tempAfterDischarge);
                    break;
                case InspectionTypes.Normal:
                    soh = GetSOHForNormal(dto, voltageBeforeDischarge, voltageAfterDischarge);
                    break;
                case InspectionTypes.Close:
                    soh = GetSOHForClose(dto, capacityDischarge);
                    break;
                default:
                    return soh;
            } // end switch
            
            return CheckValidation(soh);
        }

        private float GetSOB(InspectionResultDTO dto, InspectionStepResultDTO dataBeforeDischarge, InspectionStepResultDTO dataAfterDischarge, float currentAfterDischarge)
        {
            if (dataBeforeDischarge == null || dataBeforeDischarge.BIBoxDataList.Count == 0 || dataAfterDischarge == null || dataAfterDischarge.BIBoxDataList.Count == 0)
            {
                return 0f;
            }

            // 모듈 구성정보
            string[] splited = dto.BatteryInfo.CONSIST.Split(',');
            List<int> moduleList = new List<int>();
            foreach (string s in splited)
            {
                moduleList.Add(Convert.ToInt32(s));
            }

            // 모듈 구성정보가 없으면 0 리턴
            if (moduleList.Count == 0)
            {
                return 0f;
            }

            // 모듈 구성정보와 실제 BI 계측 정보가 다르면 0 리턴
            if (dataBeforeDischarge.BIBoxDataList.Count < moduleList.Count || dataAfterDischarge.BIBoxDataList.Count < moduleList.Count)
            {
                return 0f;
            }
            
            // 배터리 구성에 따라 MBMS 및 내부 셀 카운트 처리
            List<KeyValuePair<float, float>> voltages = new List<KeyValuePair<float, float>>();
            try
            {
                for (int i = 0; i < moduleList.Count; ++i)
                {
                    var bidataBeforeDischarge = dataBeforeDischarge.BIBoxDataList.ElementAt(i);
                    var bidataAfterDischarge = dataAfterDischarge.BIBoxDataList.ElementAt(i);

                    for (int j = 1; j <= moduleList[i]; ++j)
                    {
                        voltages.Add(new KeyValuePair<float, float>(
                            (short)bidataBeforeDischarge.GetType().GetProperty($"voltage{j}").GetValue(bidataBeforeDischarge) / 1000.0f,
                            (short)bidataAfterDischarge.GetType().GetProperty($"voltage{j}").GetValue(bidataAfterDischarge) / 1000.0f
                        ));
                    } // end for
                } // end for
            }
            catch (Exception e)
            {
                Console.Out.WriteLine($"[Error] GetSOB -> {e.ToString()}");
                return 0f;
            }



            float current = currentAfterDischarge; 

            if (voltages.Count == 0 || current == 0) return 0;

            List<float> listIR = new List<float>();
            int no = 1;
            foreach (var voltage in voltages)
            {
                float value = (voltage.Value - voltage.Key) / (current / 2) * 1000;
                listIR.Add(value);


                BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOB, [{string.Format("{0,2}", no)}] voltage [ {string.Format("{0:f3}", voltage.Value)},  {string.Format("{0:f3}", voltage.Key)} ], {string.Format("{0:f3}", (voltage.Value - voltage.Key))}");
                no += 1;
            }


            float avgIR = 0f, sumIR = 0f;
            int countIR = 0;
            for (int i = 0; i < listIR.Count; i++)
            {
                if (voltages.ElementAt(i).Key == 0 || voltages.ElementAt(i).Value == 0)
                {
                    continue;
                }

                ++countIR;
                sumIR += listIR[i];
            }


            if (countIR > 0)
            {
                avgIR = sumIR / countIR;
            }




            List<float> listDeltaIR = new List<float>();
            for (int i=0; i<listIR.Count; ++i)
            {
                if (voltages.ElementAt(i).Key == 0 || voltages.ElementAt(i).Value == 0)
                {
                    listDeltaIR.Add(avgIR * 0.2f);
                    continue;
                }

                listDeltaIR.Add(Math.Abs(avgIR - listIR[i]));
            }


            float maxIRDelta = listDeltaIR.Max();


            if (countIR > 0 && maxIRDelta <= 0)
            {
                return 100.0f;
            }

            // SOB 계산 = (IR 평균값 - 최대 편차량)* 100 / IR 평균값
            float sob = (avgIR - maxIRDelta) * 100 / avgIR;
            float sob_display = sob * 0.97f;



            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOB, avg : {avgIR},  max : {maxIRDelta} ");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOB, SOB : {string.Format("{0:f2}", sob)} ");
            //BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOB, sob_display : {string.Format("{0:f2}", sob_display)} ");

            return sob;
        }

        private float GetSOP(InspectionResultDTO dto, float voltageBeforeDischarge, float voltageAfterDischarge, float currentAfterDischarge, float tempAfterDischarge)
        {
            float volStep6 = voltageBeforeDischarge;
            float volStep7 = voltageAfterDischarge;
            float current = currentAfterDischarge;

            float ir    = (volStep7 - volStep6) / current * 1000;
            float fTemp = tempAfterDischarge;

            float d = dto.BatteryInfo.COEFF_D;
            float b = ir + d * fTemp;







            float newIR = -1 * d * 25.0f + b;






            float c = dto.BatteryInfo.COEFF_C;

            // SOP 계산
            float sop = c / newIR * 100;


            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOP, voltage [{string.Format("{0:f3}", volStep6)}, {string.Format("{0:f3}", volStep7)}], current : {string.Format("{0:f3}", current)}, temp : {string.Format("{0:f1}", fTemp)}, const C : {c}, const D : {d}");
            BaseLib.Helper.LogHelper.GradeLog($"{dto.TaskInfo.channel}", $"GetSOP, SOP : {string.Format("{0:f2}", sop)}");

            return CheckValidation(sop);
        }

        private float GetTotal(float soc, float soh, float sob, float sop)
        {
            if (float.IsNaN(soc) || float.IsNaN(soh) || float.IsNaN(sob) || float.IsNaN(sop))
            {
                return 0;
            }

            float totalBase = ( soc * EngineConfig.Instance.Evaluation.SOC
                              + soh * EngineConfig.Instance.Evaluation.SOH
                              + sob * EngineConfig.Instance.Evaluation.SOB
                              + sop * EngineConfig.Instance.Evaluation.SOP ) / 100.0f;
            return totalBase;
        }

        private GradeTypes GenerateGrade(float value)
        {
            if (value <= 0) return GradeTypes.D;  // F --> D

                 if (value >= EngineConfig.Instance.Grade.A) return GradeTypes.A;
            else if (value >= EngineConfig.Instance.Grade.B) return GradeTypes.B;
            else if (value >= EngineConfig.Instance.Grade.C) return GradeTypes.C;
            else if (value >= EngineConfig.Instance.Grade.D) return GradeTypes.D;
            else return GradeTypes.D;       // E --> D
        }

		#endregion
	}
}
