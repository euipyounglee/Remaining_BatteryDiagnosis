using ACIRLib.Data.DTO;
using BaseLib.Defines;
using BaseLib.Helper;
using BaseLib.Pubsub;
using SQLManager.Data.Query;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACIRLib.Core
{
	public class Core : BaseLib.Net.ATcpClientBase
	{
		#region variables

		public const byte STX = 0x55;

		public const byte ETX = 0xAA;

		#endregion

		#region constructor

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="channelNo"></param>
		public Core(int channelNo) : base(LogDev.ACIR, channelNo)
		{
			CurrentTaskMode = TaskInfoDTO.TaskModes.None;
		}

		#endregion

		#region property

		private TaskInfoDTO.TaskModes CurrentTaskMode { get; set; }

		private string RunId { get; set; }

		private int TaskId { get; set; }

		private int TaskSeq { get; set; }

        private int TaskStepNo { get; set; }

        private int TaskSubStepNo { get; set; }

        #endregion

        #region method

        /// <summary>
        /// init task parameters
        /// </summary>
        private void InitTask()
		{
			RunId = "";
			TaskId = 0;
			TaskSeq = 0;
            TaskStepNo = 0;
            TaskSubStepNo = 0;
        }

		/// <summary>
		/// build data
		/// </summary>
		/// <param name="stx"></param>
		/// <param name="cmd"></param>
		/// <returns></returns>
		private byte[] BuildData(byte stx, byte cmd, int dataLen)
		{
			byte[] data = Receive(4 + dataLen);
			if (data != null)
			{
				var pkt = new byte[2 + data.Length];
				pkt[0] = stx;
				pkt[1] = cmd;
				Array.Copy(data, 0, pkt, 2, data.Length);

				return pkt;
			}
			return null;
		}

		/// <summary>
		/// looped receive
		/// </summary>
		protected override void RunReceive()
		{
			try
			{
				Task.Factory.StartNew(() =>
				{
					byte stx = 0;
					byte cmd = 0;

					byte[] buffer = new byte[1024];

					while (ConnectionState == BaseLib.Defines.ConnectionStates.Connected)
					{
						try
						{
							//Receive(ref buffer);

							stx = ReceiveByte();

							if (stx != 0x55)
							{
								System.Threading.Thread.Sleep(10);
								continue;
							}

							cmd = ReceiveByte();
							switch (cmd)
							{
								case 0x81:
									{
										byte[] stream = BuildData(stx, cmd, StatusDataDTO.DATA_LENGTH);
										if (stream != null)
										{
											var dto = new StatusDataDTO(stream);
											WriteLog(dto.Source);

											// publish to subscribers
											Publish(PushDataDTO.PushDataTypes.Data, dto, TaskSeq);
										}
									}
									break;
								case 0x82:
									{
										byte[] stream = BuildData(stx, cmd, MeasureDataAckDTO.DATA_LENGTH);
										if (stream != null)
										{
											var dto = new MeasureDataAckDTO(stream);
											if (TaskId > 0 && TaskSeq > 0)
											{
												new tbl_log_acir_measuredata().Insert(new SQLManager.Data.DTO.tbl_log_acir_measuredata_DTO
												{
													log_dt = DateTime.Now,
													run_id = RunId,
													task_id = TaskId,
													task_seq = TaskSeq,
													hostname = Hostname,
													port = (ushort)Port,
													type = dto.BatteryType,
													value = dto.Value,
													step_no = (byte)TaskStepNo,
													current_no = dto.CurrentNo,
													total_no = dto.TotalNo,
													mode = dto.Mode,
													voltage = dto.Voltage,
													temperature = dto.Temp,
													hz = dto.Hz,
													re = dto.Re,
													im = dto.Im,
													reserved = dto.Reserved,
													reserved2 = dto.Reserved2
												});
											}
											WriteLog(dto.Source);
											Publish(PushDataDTO.PushDataTypes.Data, dto, TaskSeq);

											if (CurrentTaskMode == TaskInfoDTO.TaskModes.Single)
											{
												CurrentTaskMode = TaskInfoDTO.TaskModes.None;
												Publish(PushDataDTO.PushDataTypes.Data, new TaskInfoDTO(TaskStates.Completed, TaskInfoDTO.TaskModes.Single), TaskSeq);
												InitTask();
											}
										}
									}
									break;
								case 0x83:
									{
										byte[] stream = BuildData(stx, cmd, MeasureResultAckDTO.DATA_LENGTH);
										if (stream != null)
										{
											var dto = new MeasureResultAckDTO(stream);
											if (TaskId > 0 && TaskSeq > 0)
											{
												new tbl_log_acir_measureresult().Insert(new SQLManager.Data.DTO.tbl_log_acir_measureresult_DTO
												{
													log_dt = DateTime.Now,
													run_id = RunId,
													task_id = TaskId,
													task_seq = TaskSeq,
													hostname = Hostname,
													port = (ushort)Port,
													type = dto.BatteryType,
													value = dto.Value,
                                                    step_no = (byte)TaskStepNo,
                                                    current_no = dto.CurrentNo,
													total_no = dto.TotalNo,
													mode = dto.Mode,
													voltage = dto.Voltage,
													temperature = dto.Temp,
													acir = dto.ACIR,
													rs = dto.Rs,
													rp = dto.Rp,
													reserved = dto.Reserved,
													reserved2 = dto.Reserved2
												});
											}
											WriteLog(dto.Source);
											Publish(PushDataDTO.PushDataTypes.Data, dto, TaskSeq);

											if (CurrentTaskMode == TaskInfoDTO.TaskModes.Spectrum)
											{
												CurrentTaskMode = TaskInfoDTO.TaskModes.None;
												Publish(PushDataDTO.PushDataTypes.Data, new TaskInfoDTO(TaskStates.Completed, TaskInfoDTO.TaskModes.Spectrum), TaskSeq);
												InitTask();
											}
										}
									}
									break;
								default:
									Console.Out.WriteLine($"[Unknown] command : {cmd}");
									break;
							} // end switch

							System.Threading.Thread.Sleep(10);
						}
						catch
						{
							Disconnect();
						}
					} // end while
				});
			}
			catch
			{
				Disconnect();
			}
		}

		/// <summary>
		/// start measure
		/// </summary>
		public void StartMeasure(byte mode, float frequency, string runId, int taskId, int taskSeq, int taskStepNo, int taskSubStepNo)
		{
			RunId = runId;
			TaskId = taskId;
			TaskSeq = taskSeq;
            TaskStepNo = taskStepNo;
            TaskSubStepNo = taskSubStepNo;

            switch (mode)
			{
				case 0x00: 
					CurrentTaskMode = TaskInfoDTO.TaskModes.Single;
					break;
				case 0x01:
					CurrentTaskMode = TaskInfoDTO.TaskModes.Spectrum;
					break;
				default:
					CurrentTaskMode = TaskInfoDTO.TaskModes.None;
					break;
			} // end switch
			
			var stream = new MeasureDataDTO(0, mode, frequency).ToStream();
			Send(stream);
		}



		#endregion

	}
}
