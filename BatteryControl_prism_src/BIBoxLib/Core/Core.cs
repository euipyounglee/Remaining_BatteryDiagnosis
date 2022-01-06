using BaseLib.Data.DTO;
using BaseLib.Defines;
using BaseLib.Helper;
using BaseLib.Pubsub;
using BIBoxLib.Data.DTO;
using BIBoxLib.Defines;
using I7565H1Lib.Core;
using I7565H1Lib.Data.DTO;
using SQLManager.Data;
using SQLManager.Data.Query;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBoxLib.Core
{
	public class Core : AI7565H1
	{
		#region variables

		public const byte MODE = 0;
		public const byte RTR = 0;
		public const byte DLC = 8;

        // Modified {
        public static int CHECK_MODE_OFF = -1;
        public static int CHECK_MODE_START = 0;
        public static int CHECK_MODE_ON = 1;
        public static int CHECK_MODE_STOP = 2;
        // Modified }

        #endregion

        #region constructor

        public Core(Type sdk, int channelNo, int lastMbmsId) : base(sdk, LogDev.BI_BOX, channelNo)
		{
			LastMbmsId = lastMbmsId;
			CurrentTaskInfo = new CurrentTaskInfoDTO("", 0, 0, 0);
			CellVoltages = new Dictionary<byte, List<BIBoxCellVoltageDTO>>();
			CellTemperatures = new Dictionary<byte, List<BIBoxCellTemperatureDTO>>();

            CheckMode = CHECK_MODE_OFF;
            MaxVoltage = 4.2;
            MinVoltage = 2.8;
		}

		#endregion

		#region property

		private int LastMbmsId { get; set; }

		public CurrentTaskInfoDTO CurrentTaskInfo { get; set; }

		public Dictionary<byte, List<BIBoxCellVoltageDTO>> CellVoltages { get; set; }

		public Dictionary<byte, List<BIBoxCellTemperatureDTO>> CellTemperatures { get; set; }

        public int CheckMode { get; set; }

        public double MinVoltage { get; set; }

        public double MaxVoltage { get; set; }

        public string ModuleConsist { get; set; }

		#endregion

		#region method

		public override bool Connect(string portName)
		{
			bool result = OpenCAN(portName, 500000);
			if (result)
			{
				Task.Run(() =>
				{
					while (ConnectionState == BaseLib.Defines.ConnectionStates.Connected)
					{
						if (GetRXMsgCnt(1) > 0)
						{
							RecvCANMsg(1);
							System.Threading.Thread.Sleep(1);
						}
						else
						{
							System.Threading.Thread.Sleep(10);
						}
					} // end while

					ConnectionState = BaseLib.Defines.ConnectionStates.Disconnected;
				});
			}
			return result;
		}

		public override void Disconnect()
		{
			CloseCAN();
		}

		public void SendCANMsg(byte CANNo, uint CANId, BIBoxCommand command, string runId, int taskId, int taskSeq, int taskStepNo)
		{
			CurrentTaskInfo = new CurrentTaskInfoDTO(runId, taskId, taskSeq, taskStepNo);

			var stream = BuildSpec((byte)command);
			WriteLog($"[Send] CANID: 0x{string.Format("{0:X}", CANId)}, {StringHelper.ByteArrayToHexString(stream)}");

			SendCANMsg(CANNo, MODE, RTR, DLC, CANId, stream);
			Publish(PushDataDTO.PushDataTypes.Log, $"send CAN as {StringEnum.GetStringValue(command)}");
			WriteLog($"send CAN as {StringEnum.GetStringValue(command)}");
		}

		public void GetCellVoltage(string runId, int taskId, int taskSeq, int taskStepNo)
		{
            if ( CheckMode != CHECK_MODE_OFF )
            {
                //CheckMode = CHECK_MODE_STOP;

                for (int loop = 0; loop < 10; loop ++)
                {
                    WriteLog($"GetCellTemperature() CheckMode = [{CheckMode}]");

                    if (CheckMode == CHECK_MODE_OFF)
                    {
                        break;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(200);
                    }
                }
            }

            if (CheckMode == CHECK_MODE_OFF)
            {
                uint canId = 0x211;
                CellVoltages.Clear();
                SendCANMsg(1, canId, BIBoxCommand.GetCellVoltage, runId, taskId, taskSeq, taskStepNo);
            }
            else
            {
                WriteLog($"[BI] GetCellVoltage, CheckMode is not CHECK_MODE_OFF");
            }
		}

		public void GetCellTemperature(string runId, int taskId, int taskSeq, int taskStepNo)
		{
            if (CheckMode != CHECK_MODE_OFF)
            {
                //CheckMode = CHECK_MODE_STOP;

                for (int loop = 0; loop < 10; loop++)
                {
                    WriteLog($"GetCellTemperature() CheckMode = [{CheckMode}]");

                    if (CheckMode == CHECK_MODE_OFF)
                    {
                        break;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(200);
                    }
                }
            }

            if (CheckMode == CHECK_MODE_OFF)
            {
                uint canId = 0x211;
                CellTemperatures.Clear();
                SendCANMsg(1, canId, BIBoxCommand.GetCellTemperature, runId, taskId, taskSeq, taskStepNo);
            }
		}

        public void CheckOverVoltageStart(string runId, int taskId, int taskSeq, int taskStepNo, double min_voltage, double max_voltage, String moduleConsist)
        {
            CheckMode = CHECK_MODE_ON;

            MinVoltage = min_voltage;
            MaxVoltage = max_voltage;
            ModuleConsist = moduleConsist;


            uint canId = 0x211;
            CellVoltages.Clear();
            SendCANMsg(1, canId, BIBoxCommand.GetCellVoltage, runId, taskId, taskSeq, taskStepNo);
        }

        public void CheckOverVoltageStop()
        {
            WriteLog($"CheckOverVoltageStop() [{CheckMode}]");

            if ( CheckMode == CHECK_MODE_ON )
            {
                WriteLog($"CheckOverVoltageStop() set CHECK_MODE_STOP");
                CheckMode = CHECK_MODE_STOP;
            }
        }

		public void SetConfiguraton(byte CANNo, uint CANId, byte moduleCount, byte cellCount, byte tempType, byte tempCount)
		{
			byte[] data = new byte[] { 0xFE, moduleCount, cellCount, tempType, tempCount, 0x00, 0x00, 0x00 };
			SendCANMsg(CANNo, MODE, RTR, DLC, CANId, data);
		}

		public void RecvCANMsg(byte CANNo)
		{
			byte mode = 0;
			byte rtr = 0;
			byte dlc = 0;
			uint canId = 0;
			uint timeL = 0;
			uint timeH = 0;

			byte[] data = base.RecvCANMsg(CANNo, ref mode, ref rtr, ref dlc, ref canId, ref timeL, ref timeH);
			if (data == null)
			{
				Disconnect();
				return;
			}

			// canid error
			if (canId == 0)
			{
				WriteLog($"[BI][Recv] CANID is 0x00");
				Disconnect();
				return;
			}

			Parse(canId, data);

			string hexString = StringHelper.ByteArrayToHexString(data);
			WriteLog($"[BI][Recv] CANID: 0x{string.Format("{0:X}", canId)}, {hexString}");

			// hex print
			Publish(PushDataDTO.PushDataTypes.Hex, new CanSourceDTO
			{
				LogDt = DateTime.Now,
				CanId = $"0x{string.Format("{0:X4}", canId)}",
				Mode = mode,
				RTR = rtr,
				DLC = dlc,
				MessageTime = $"{string.Format("{0:0.0000}", (double)((timeH << 32) + timeL) / 10000)} sec",
				HexPrint = hexString
			});
		}

		#endregion

		#region method (spec)

		private void SaveCellVoltageData(uint canId, string runId, int taskId, int taskSeq)
		{
			foreach (var key in CellVoltages.Keys)
			{
				List<BIBoxCellVoltageDTO> values;
				if (CellVoltages.TryGetValue(key, out values))
				{
					var dto = new SQLManager.Data.DTO.tbl_log_bi_data_DTO();

					for (int i = 0; i < values.Count; ++i)
					{
						var value = values.ElementAt(i);

						if (i == 0)
						{
							dto.log_dt = DateTime.Now;
							dto.run_id = runId;
							dto.task_id = taskId;
							dto.task_seq = taskSeq;
							dto.can_id = canId;
							dto.mbms_id = value.MBMSId;
						}

						// 셀 지정
						switch (value.Seq)
						{
							case 1:
								dto.voltage1 = value.Cell1;
								dto.voltage2 = value.Cell2;
								dto.voltage3 = value.Cell3;
								break;
							case 4:
								dto.voltage4 = value.Cell1;
								dto.voltage5 = value.Cell2;
								dto.voltage6 = value.Cell3;
								break;
							case 7:
								dto.voltage7 = value.Cell1;
								dto.voltage8 = value.Cell2;
								dto.voltage9 = value.Cell3;
								break;
							case 10:
								dto.voltage10 = value.Cell1;
								dto.voltage11 = value.Cell2;
								dto.voltage12 = value.Cell3;
								break;
							case 13:
								dto.voltage13 = value.Cell1;
								dto.voltage14 = value.Cell2;
								dto.voltage15 = value.Cell3;
								break;
							default:
								return;
						} // end switch
					} // end for

					new tbl_log_bi_data().Insert(dto);
				} // end if
			} // end foreach
		}

		/// <summary>
		/// save or update cell temperature data
		/// </summary>
		/// <param name="canId"></param>
		/// <param name="data"></param>
		private void SaveCellTemperatureData(uint canId, string runId, int taskId, int taskSeq)
		{
			foreach (var key in CellTemperatures.Keys)
			{
				List<BIBoxCellTemperatureDTO> values;
				if (CellTemperatures.TryGetValue(key, out values))
				{
					for (int i = 0; i < values.Count; ++i)
					{
						var value = values.ElementAt(i);
						bool ret = new tbl_log_bi_data().Insert(new SQLManager.Data.DTO.tbl_log_bi_data_DTO
						{
							log_dt = DateTime.Now,
							run_id = runId,
							task_id = taskId,
							task_seq = taskSeq,
							can_id = canId,
							mbms_id = value.MBMSId,
							temperature1 = value.Temperature1,
							temperature2 = value.Temperature2
						});

                        if ( ret == false)
                        {
                            new tbl_log_bi_data().UpdateTemperature(new SQLManager.Data.DTO.tbl_log_bi_data_DTO
                            {
                                log_dt = DateTime.Now,
                                run_id = runId,
                                task_id = taskId,
                                task_seq = taskSeq,
                                can_id = canId,
                                mbms_id = value.MBMSId,
                                temperature1 = value.Temperature1,
                                temperature2 = value.Temperature2
                            });
                        }
					} // end for
				} // end if
			} // end foreach
		}

        private (bool isOverVolt, bool isUnderVolt) CheckCellOverVoltage( uint canId )
        {
            // 모듈별 셀 구성 정보
            string[] splited = ModuleConsist.Split(',');
            List<int> moduleList = new List<int>();
            foreach (string s in splited)
            {
                moduleList.Add(Convert.ToInt32(s));
            }


            bool isOverVoltage = false;
            bool isUnderVoltage = false;
            foreach (var key in CellVoltages.Keys)
            {
                List<BIBoxCellVoltageDTO> values;
                if (CellVoltages.TryGetValue(key, out values))
                {
                    var dto = new SQLManager.Data.DTO.tbl_log_bi_data_DTO();

                    for (int i = 0; i < values.Count; ++i)
                    {
                        var value = values.ElementAt(i);

                        int cell_count = 0;
                        if (moduleList.Count >= value.MBMSId)
                        {
                            cell_count = moduleList[value.MBMSId - 1];
                        }

                        StringBuilder sb1 = new StringBuilder();
                        if (cell_count > 0)
                        {
                            sb1.Append(string.Format("CheckCellVoltage, [CAN ID 0x{0:X4}][MBMS ID #{1:D2}] ", canId, value.MBMSId));
                        }
                        // Over Voltage 체크
                        if ( cell_count >= ((value.Seq - 1) + 1) )   // cell_count >= ( mbms_id - 1 + 셀 순번 1~3 )
                        {
                            if ((value.Cell1 / 1000.0) > MaxVoltage)
                            {
                                isOverVoltage = true;
                            }
                            else if ((value.Cell1 / 1000.0) < MinVoltage)
                            {
                                isUnderVoltage = true;
                            }
                        }

                        if ((cell_count >= ((value.Seq - 1) + 2)) )
                        {
                            if ((value.Cell2 / 1000.0) > MaxVoltage)
                            {
                                isOverVoltage = true;
                            }
                            else if ( (value.Cell2 / 1000.0) < MinVoltage )
                            {
                                isUnderVoltage = true;
                            }
                        }

                        if ((cell_count >= ((value.Seq - 1) + 3)) )
                        {
                            if ((value.Cell3 / 1000.0) > MaxVoltage)
                            {
                                isOverVoltage = true;
                            }
                            else if ( (value.Cell3 / 1000.0) < MinVoltage )
                            {
                                isUnderVoltage = true;
                            }
                        }

                        if (cell_count > 0)
                        {
                            sb1.Append(string.Format("Cell {0:D2}: {1}V, ", (value.Seq - 1) + 1, value.Cell1 / 1000.0));
                            sb1.Append(string.Format("Cell {0:D2}: {1}V, ", (value.Seq - 1) + 2, value.Cell2 / 1000.0));
                            sb1.Append(string.Format("Cell {0:D2}: {1}V, ", (value.Seq - 1) + 3, value.Cell3 / 1000.0));

                            sb1.Append(string.Format("isOverVoltage = {0}, ", isOverVoltage));
                            sb1.Append(string.Format("isUnderVoltage = {0} ", isUnderVoltage));

                            WriteLog(sb1.ToString());
                        }
                       
                    } // end for
                } // end if
            } // end foreach
            return (isOverVoltage, isUnderVoltage );
        }


		/// <summary>
		/// build spec
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		private byte[] BuildSpec(byte command)
		{
			return new byte[] { command, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
		}

		//private int GetEndCondition()
		//{
		//	return 
		//}

		/// <summary>
		/// parse
		/// </summary>
		/// <param name="canId"></param>
		/// <param name="data"></param>
		private void Parse(uint canId, byte[] data)
		{
			switch (data[7])
			{
				case 0x51:
				case 0x52:
				case 0x53:
				case 0x54:
				case 0x55:
					{
						var dto = new BIBoxCellVoltageDTO(canId, data);
						List<BIBoxCellVoltageDTO> value;
						if (!CellVoltages.TryGetValue(dto.MBMSId, out value))
						{
							value = new List<BIBoxCellVoltageDTO>();
							value.Add(dto);
							CellVoltages.Add(dto.MBMSId, value);
						}
						else
						{
							value.Add(dto);
						}

						if (dto.MBMSId == LastMbmsId && data[7] == 0x55)
						{
                            if (CheckMode == CHECK_MODE_ON || CheckMode == CHECK_MODE_STOP )
                            {
                                ( bool isOverVolt, bool isUnderVolt ) = CheckCellOverVoltage(canId);
                                if (isOverVolt == true)
                                {
                                    Publish(PushDataDTO.PushDataTypes.Data, new TaskInfoDTO(TaskStates.PauseCellOverVoltage));
                                    CheckMode = CHECK_MODE_OFF;
                                }
                                else if (isUnderVolt == true)
                                {
                                    Publish(PushDataDTO.PushDataTypes.Data, new TaskInfoDTO(TaskStates.PauseCellUnderVoltage));
                                    CheckMode = CHECK_MODE_OFF;
                                }
                                else
                                {
                                    if (CheckMode == CHECK_MODE_ON)
                                    {
                                        CheckOverVoltageStart(CurrentTaskInfo.RunId, CurrentTaskInfo.TaskId, CurrentTaskInfo.TaskSeq, CurrentTaskInfo.TaskStepNo, MinVoltage, MaxVoltage, ModuleConsist);
                                    }
                                }

                                if (CheckMode == CHECK_MODE_STOP)
                                {
                                    CheckMode = CHECK_MODE_OFF;
                                }
                            }
                            else
                            // Modified }
                            {
								if (!CurrentTaskInfo.IsEmpty)
								{
									SaveCellVoltageData(canId, CurrentTaskInfo.RunId, CurrentTaskInfo.TaskId, CurrentTaskInfo.TaskSeq);
									CellVoltages.Clear();
								}
								Publish(PushDataDTO.PushDataTypes.Data, new TaskInfoDTO(TaskStates.CompletedBIVolt));
                            }
						} // end if

						StringBuilder sb = new StringBuilder();
						sb.Append(string.Format("[CAN ID 0x{0:X4}][MBMS ID #{1:D2}] ", canId, dto.MBMSId));
						sb.Append(string.Format("Cell {0:D2}: {1}V, ", dto.Seq + 0, dto.Cell1 / 1000.0 ));
						sb.Append(string.Format("Cell {0:D2}: {1}V, ", dto.Seq + 1, dto.Cell2 / 1000.0 ));
						sb.Append(string.Format("Cell {0:D2}: {1}V, ", dto.Seq + 2, dto.Cell3 / 1000.0 ));

						WriteLog(sb.ToString());
					}
					break;
				case 0x61:
					{
						var dto = new BIBoxCellTemperatureDTO(canId, data);
						List<BIBoxCellTemperatureDTO> value;
						if (!CellTemperatures.TryGetValue(dto.MBMSId, out value))
						{
							value = new List<BIBoxCellTemperatureDTO>();
							value.Add(dto);
							CellTemperatures.Add(dto.MBMSId, value);
						}
						else
						{
							value.Add(dto);
						}

						if (dto.MBMSId == LastMbmsId)
						{
							if (!CurrentTaskInfo.IsEmpty)
							{
								SaveCellTemperatureData(canId, CurrentTaskInfo.RunId, CurrentTaskInfo.TaskId, CurrentTaskInfo.TaskSeq);
								CellTemperatures.Clear();
							}

							Publish(PushDataDTO.PushDataTypes.Data, new TaskInfoDTO(TaskStates.CompletedBITemp));
						} // end if

						StringBuilder sb = new StringBuilder();
						sb.Append(string.Format("[CAN ID 0x{0:X4}][MBMS ID #{1:D2}] ", canId, dto.MBMSId));
						sb.Append($"Temperature1:{dto.Temperature1 / 10.0}℃,  Temperature2:{dto.Temperature2 / 10.0}℃");

						WriteLog(sb.ToString());
					}
					break;
				case 0x71:
					{
						var dto = new BIBoxModuleVoltageDTO(canId, data);
						Publish(PushDataDTO.PushDataTypes.Data, dto);

						StringBuilder sb = new StringBuilder();
						sb.Append(string.Format("[CAN ID 0x{0:X4}][MBMS ID #{1:D2}] ", canId, dto.MBMSId));
						sb.Append($"Voltage:{dto.PackVoltage / 100.0}V");

						WriteLog(sb.ToString());
					}
					break;
				default:
					switch (data[0])
					{
						case 0xF1:
							{
								var dto = new BIBoxStatusDTO(canId, data);
								Publish(PushDataDTO.PushDataTypes.Data, dto);

								//WriteLog(tbl_log.LogLevels.Info, tbl_log.LogDev.BMS,
								//	$"[MBMS ID #{dto.MBMSId}] Module Count : {dto.ModuleCount}, Cell Count : {dto.CellCount}, " +
								//	$"Temp.Type : {dto.TempType}, Temp.Count : {dto.TempCount}");
							}
							break;
						case 0xFC:
							{
								var dto = new BIBoxConfigurationDTO(canId, data);
								Publish(PushDataDTO.PushDataTypes.Data, dto);

								//WriteLog(tbl_log.LogLevels.Info, tbl_log.LogDev.BMS,
								//	$"[MBMS ID #{dto.MBMSId}] Module Count : {dto.ModuleCount}, Cell Count : {dto.CellCount}, " +
								//	$"Temp.Type : {dto.TempType}, Temp.Count : {dto.TempCount}");
							}
							break;
						default:
							Publish(PushDataDTO.PushDataTypes.Log, $"unknown CAN ID : {canId}");
							break;
					}
					break;
			} // end switch
		}

		#endregion
	}
}
