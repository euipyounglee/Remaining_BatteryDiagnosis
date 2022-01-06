using BaseLib.Defines;
using BaseLib.Pubsub;
using Prism.Commands;
using Prism.Mvvm;
using RestApiLib.Defines;
using SharedLib.Core;
using SharedLib.Data.VM;
using SharedLib.Defines;
using SQLManager.Data;
using SQLManager.Data.DTO;
using SQLManager.Data.Query;
using ST5520Lib.Data.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SharedLib.View
{
	public class ChannelViewModel : BindableBase
	{
		#region property (common)

		public ChannelDevice Devices
		{
			get
			{
				System.Console.WriteLine($"[{ChannelIndex}] ChannelDevice, ElementAt({ChannelIndex})");

				return SharedPreferences.Instance.ChannelDevices.ElementAt(ChannelIndex);
			}
		}

		public ChannelConfigVM ChannelConfig
		{
			get
			{
				byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;
				return ChannelNum == 1 ? SharedPreferences.Instance.LocalConfig.Channel1 : SharedPreferences.Instance.LocalConfig.Channel2;
			}
		}

		private byte m_ChannelIndex;
		public byte ChannelIndex
		{
			get
			{
				System.Console.WriteLine($"[{m_ChannelIndex}] ChannelIndex, get m_ChannelIndex = {m_ChannelIndex}");
				return m_ChannelIndex;
			}
			set
			{
				if (value == 0)
                {
					m_ChannelIndex = 0;

				}
                else
                {
					m_ChannelIndex = 1;
				}
				System.Console.WriteLine($"[{m_ChannelIndex}] ChannelIndex, set m_ChannelIndex = {m_ChannelIndex}");

				RaisePropertyChanged("ChannelIndex");
			}
		}

		public byte ChannelNum
		{
			get
			{
				System.Console.WriteLine($"[{m_ChannelIndex}] ChannelIndex, get m_ChannelIndex = {m_ChannelIndex}");
				if ( ChannelIndex == 0)
					return 1;
				else
					return 2;
			}
		}

		/// <summary>
		/// current progress state
		/// </summary>
		private ProgressStateTypes m_CurrentProgressState = ProgressStateTypes.Start;
		public ProgressStateTypes CurrentProgressState
		{
			get
			{
				return m_CurrentProgressState;
			}
			set
			{
				m_CurrentProgressState = value;
				RaisePropertyChanged("CurrentProgressState");

				switch (value)
				{
					case ProgressStateTypes.Start:
						TaskRunInfo = new tbl_task_run_DTO();
						break;
					default:
						TaskRunInfo.step = $"{value}";
						new tbl_task_run().UpdateStepInfo(TaskRunInfo);
						break;
				} // end switch
			}
		}

		private ChannelViewPopupStates m_CurrentChannelViewPopupState = ChannelViewPopupStates.None;
		public ChannelViewPopupStates CurrentChannelViewPopupState
		{
			get
			{
				return m_CurrentChannelViewPopupState;
			}
			set
			{
				m_CurrentChannelViewPopupState = value;
				RaisePropertyChanged("CurrentChannelViewPopupState");
			}
		}

		public BindableBase CurrentViewInstance { get; set; }

		public pg_btry_info_VM SelectedBatteryInfo
		{
			get
			{
				return SharedPreferences.Instance.SelectedBatteryMap[ChannelIndex];
			}
		}

		private tbl_task_run_DTO m_TaskRunInfo;
		public tbl_task_run_DTO TaskRunInfo
		{
			get
			{
				if (m_TaskRunInfo == null)
				{
					m_TaskRunInfo = new tbl_task_run_DTO();
				}	
				return m_TaskRunInfo;
			}
			set
			{
				m_TaskRunInfo = value;
			}
		}


		#endregion

		#region property (DeviceSettingPopup)

		private ConnectionStates m_IsMultimeterOpened;
		public ConnectionStates IsMultimeterOpened
		{
			get
			{
				return m_IsMultimeterOpened;
			}
			set
			{
				m_IsMultimeterOpened = value;
				RaisePropertyChanged("IsMultimeterOpened");
			}
		}

		/// <summary>
		/// st5520 opened
		/// </summary>
		private ConnectionStates m_IsST5520Opened;
		public ConnectionStates IsST5520Opened
		{
			get
			{
				return m_IsST5520Opened;
			}
			set
			{
				m_IsST5520Opened = value;
				RaisePropertyChanged("IsST5520Opened");
			}
		}

		/// <summary>
		/// acir opened
		/// </summary>
		private ConnectionStates m_IsACIROpened;
		public ConnectionStates IsACIROpened
		{
			get
			{
				return m_IsACIROpened;
			}
			set
			{
				m_IsACIROpened = value;
				RaisePropertyChanged("IsACIROpened");
			}
		}

		/// <summary>
		/// pne cts opened
		/// </summary>
		private ConnectionStates m_IsPNECTSOpened;
		public ConnectionStates IsPNECTSOpened
		{
			get
			{
				return m_IsPNECTSOpened;
			}
			set
			{
				m_IsPNECTSOpened = value;
				RaisePropertyChanged("IsPNECTSOpened");
			}
		}

		/// <summary>
		/// relay box opened
		/// </summary>
		private ConnectionStates m_IsRelayBoxOpened;
		public ConnectionStates IsRelayBoxOpened
		{
			get
			{
				return m_IsRelayBoxOpened;
			}
			set
			{
				m_IsRelayBoxOpened = value;
				RaisePropertyChanged("IsRelayBoxOpened");
			}
		}

		/// <summary>
		/// bi box opened
		/// </summary>
		private ConnectionStates m_IsBIBoxOpened;
		public ConnectionStates IsBIBoxOpened
		{
			get
			{
				return m_IsBIBoxOpened;
			}
			set
			{
				m_IsBIBoxOpened = value;
				RaisePropertyChanged("IsBIBoxOpened");
			}
		}

		/// <summary>
		/// barcode scanner
		/// </summary>
		private ConnectionStates m_IsBarcodeScannerOpened;
		public ConnectionStates IsBarcodeScannerOpened
		{
			get
			{
				return m_IsBarcodeScannerOpened;
			}
			set
			{
				m_IsBarcodeScannerOpened = value;
				RaisePropertyChanged("IsBarcodeScannerOpened");
			}
		}

		/// <summary>
		/// comport list
		/// </summary>
		private ObservableCollection<string> m_ComPorts;
		public ObservableCollection<string> ComPorts
		{
			get
			{
				if (m_ComPorts == null)
				{
					m_ComPorts = new ObservableCollection<string>(SerialPort.GetPortNames());
				}
				return m_ComPorts;
			}
			set
			{
				m_ComPorts = value;
				RaisePropertyChanged("ComPorts");
			}
		}

		#endregion






		#region command (common)

		/// <summary>
		/// loaded
		/// </summary>
		public DelegateCommand LoadedCommand
		{
			get
			{
				return new DelegateCommand(delegate ()
				{
					// 멀티미터 셋업
					Devices.MultimeterInstance.Subscribe(MultimeterPushCallback);
					IsMultimeterOpened = Devices.MultimeterInstance.ConnectionState;

					// 절연저항시험기 셋업
					Devices.ST5520Instance.Subscribe(ST5520PushCallback);
					IsST5520Opened = Devices.ST5520Instance.ConnectionState;
					
					// acir 셋업
					Devices.ACIRInstance.Subscribe(ACIRPushCallback);
					IsACIROpened = Devices.ACIRInstance.ConnectionState;

					// pne cts 셋업
					Devices.PNECTSInstance.Subscribe(PNECTSPushCallback);
					IsPNECTSOpened = Devices.PNECTSInstance.ConnectionState;

					// relay box 셋업
					Devices.RelayBoxInstance.Subscribe(RelayBoxPushCallback);
					IsRelayBoxOpened = Devices.RelayBoxInstance.ConnectionState;
					
					// bi box 셋업
					Devices.BIBoxInstance.Subscribe(BIBoxPushCallback);
					IsBIBoxOpened = Devices.BIBoxInstance.ConnectionState;
					
					// barcode scanner 셋업
					Devices.BarcodeScannerInstance.Subscribe(BarcodeScannerPushCallback);
					IsBarcodeScannerOpened = Devices.BarcodeScannerInstance.ConnectionState;

				});
			}
		}

		/// <summary>
		/// unloaded
		/// </summary>
		public DelegateCommand UnloadedCommand
		{
			get
			{
				return new DelegateCommand(delegate ()
				{
					Devices.MultimeterInstance.Unsubscribe(MultimeterPushCallback);
					Devices.ST5520Instance.Unsubscribe(ST5520PushCallback);
					Devices.ACIRInstance.Unsubscribe(ACIRPushCallback);
					Devices.PNECTSInstance.Unsubscribe(PNECTSPushCallback);
					Devices.RelayBoxInstance.Unsubscribe(RelayBoxPushCallback);
					Devices.BIBoxInstance.Unsubscribe(BIBoxPushCallback);
					Devices.BarcodeScannerInstance.Unsubscribe(BarcodeScannerPushCallback);
				});
			}
		}

		public DelegateCommand<ProgressStateTypes?> ChangeProgressStateCommand
		{
			get
			{
				return new DelegateCommand<ProgressStateTypes?>(delegate (ProgressStateTypes? value)
				{
					if (value != null)
					{
						CurrentProgressState = value.Value;
					}
				});
			}
		}

		public DelegateCommand<ChannelViewPopupStates?> ChangeChannelViewPopupStateCommand
		{
			get
			{
				return new DelegateCommand<ChannelViewPopupStates?>(delegate (ChannelViewPopupStates? value)
				{
					if (value != null)
					{
						CurrentChannelViewPopupState = value.Value;
					}
				});
			}
		}

		public DelegateCommand SaveChannelConfigCommand
		{
			get
			{
				return new DelegateCommand(delegate ()
				{
					XmlHelper.Save(SharedPreferences.Instance.LocalConfig);

					MessageBox.Show("저장했습니다.");
				});
			}
		}


		#endregion

		#region command (DeviceSettingPopup)

		/// <summary>
		/// multimeter open
		/// </summary>
		public DelegateCommand MultimeterOpenCommand
		{
			get
			{
				return new DelegateCommand(delegate ()
				{
					if (IsMultimeterOpened == ConnectionStates.Disconnected)
					{
						Devices.MultimeterInstance.Connect(ChannelConfig.MultimeterHostInfo);
						IsMultimeterOpened = Devices.MultimeterInstance.ConnectionState;
					}
					else
					{
						IsMultimeterOpened = ConnectionStates.Disconnected;
						Devices.MultimeterInstance.Disconnect();
					}
				});
			}
		}

		/// <summary>
		/// st5520 open
		/// </summary>
		public DelegateCommand ST5520OpenCommand
		{
			get
			{
				return new DelegateCommand(delegate ()
				{
					if (IsST5520Opened == ConnectionStates.Disconnected)
					{
						Devices.ST5520Instance.Connect(ChannelConfig.ST5520ComPort);
						IsST5520Opened = Devices.ST5520Instance.ConnectionState;
					}
					else
					{
						IsST5520Opened = ConnectionStates.Disconnected;
						Devices.ST5520Instance.Disconnect();
					}
				});
			}
		}

		/// <summary>
		/// acir open
		/// </summary>
		public DelegateCommand ACIROpenCommand
		{
			get
			{
				return new DelegateCommand(delegate ()
				{
					if (IsACIROpened != ConnectionStates.Connected)
					{
						Devices.ACIRInstance.Connect(ChannelConfig.ACIRHostInfo);
						IsACIROpened = Devices.ACIRInstance.ConnectionState;
					}
					else
					{
						IsACIROpened = ConnectionStates.Disconnected;
						Devices.ACIRInstance.Disconnect();
					}
				});
			}
		}

		/// <summary>
		/// pne cts open
		/// </summary>
		public DelegateCommand PNECTSOpenCommand
		{
			get
			{
				return new DelegateCommand(delegate ()
				{
					if (IsPNECTSOpened == ConnectionStates.Disconnected)
					{
						Devices.PNECTSInstance.Connect();//PNE 장비 연동 호출
						IsPNECTSOpened = Devices.PNECTSInstance.ConnectionState;
					}
					else
					{
						IsPNECTSOpened = ConnectionStates.Disconnected;
						Devices.PNECTSInstance.Disconnect();
					}
				});
			}
		}

		/// <summary>
		/// relay box open
		/// </summary>
		public DelegateCommand RelayBoxOpenCommand
		{
			get
			{
				return new DelegateCommand(delegate ()
				{
					if (IsRelayBoxOpened == ConnectionStates.Disconnected)
					{
						Devices.RelayBoxInstance.Connect(ChannelConfig.RelayControllerComPort);
						IsRelayBoxOpened = Devices.RelayBoxInstance.ConnectionState;
					}
					else
					{
						IsRelayBoxOpened = ConnectionStates.Disconnected;
						Devices.RelayBoxInstance.Disconnect();
					}
				});
			}
		}

		/// <summary>
		/// bi box open
		/// </summary>
		public DelegateCommand BIBoxOpenCommand
		{
			get
			{
				return new DelegateCommand(delegate ()
				{
					if (IsBIBoxOpened == ConnectionStates.Disconnected)
					{
						Devices.BIBoxInstance.Connect(ChannelConfig.BIBoxComPort);
						IsBIBoxOpened = Devices.BIBoxInstance.ConnectionState;
					}
					else
					{
						IsBIBoxOpened = ConnectionStates.Disconnected;
						Devices.BIBoxInstance.Disconnect();
					}
				});
			}
		}

		/// <summary>
		/// barcode scanner open
		/// </summary>
		public DelegateCommand BarcodeScannerOpenCommand
		{
			get
			{
				return new DelegateCommand(delegate ()
				{
					if (IsBarcodeScannerOpened == ConnectionStates.Disconnected)
					{
						Devices.BarcodeScannerInstance.Connect(ChannelConfig.BarcodeScannerComPort);
						IsBarcodeScannerOpened = Devices.BarcodeScannerInstance.ConnectionState;
					}
					else
					{
						IsBarcodeScannerOpened = ConnectionStates.Disconnected;
						Devices.BarcodeScannerInstance.Disconnect();
					}
				});
			}
		}

		#endregion

		#region method (task)

		/// <summary>
		/// delegate of task completed callback
		/// </summary>
		public delegate void dTaskCompleted(bool isCompleted, int code, string reason, int taskSeq);

		/// <summary>
		/// task completed subscribers
		/// </summary>
		private dTaskCompleted TaskCompletedCallback { get; set; }

		/// <summary>
		/// subscribe
		/// </summary>
		/// <param name="callback"></param>
		public void SubscribeTaskCompletedCallback(dTaskCompleted callback)
		{
			TaskCompletedCallback = callback;
		}

		/// <summary>
		/// unsubscribe
		/// </summary>
		/// <param name="callback"></param>
		public void UnsubscribeTaskCompletedCallback()
		{
			TaskCompletedCallback = null;
		}


        //----------------------------------------------------------------
        public delegate void dTaskProcessing(TaskStates status,int taskSeq);

        private dTaskProcessing TaskProcessingCallback { get; set; }

        public void SubscribeTaskProcessingCallback(dTaskProcessing callback)
        {
            TaskProcessingCallback = callback;
        }

        public void UnsubscribeTaskProcessingCallback()
        {
            TaskProcessingCallback = null;
        }

        private void PublishTaskProcessing(TaskStates status, int taskSeq)
        {
            if (TaskProcessingCallback != null)
            {
                try
                {
                    Console.Out.WriteLine($"TRACE : PublishTaskProcessing({status},{taskSeq})");
                    TaskProcessingCallback(status, taskSeq);
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.ToString());
                }
            }
        }
        //----------------------------------------------------------------


        /// <summary>
        /// publish task completed
        /// </summary>
        private void PublishTaskCompleted(bool isCompleted, int code, string reason, int TaskSeq)
		{
			if (TaskCompletedCallback != null)
			{
				try
				{
                    Console.Out.WriteLine($"TRACE : PublishTaskCompleted({isCompleted},{code},{reason},{TaskSeq})");
                    TaskCompletedCallback(isCompleted, code, reason, TaskSeq);
				}
				catch (Exception e)
				{
					Console.Out.WriteLine(e.ToString());
				}
			}
		}

		#endregion

		#region method (acir)

		public delegate void dReceivedACIRMeasureData(ACIRLib.Data.DTO.MeasureDataAckDTO dto);

		public dReceivedACIRMeasureData ReceivedACIRMeasureDataCallback { get; set; }

		private void PublichReceivedACIRMeasureData(ACIRLib.Data.DTO.MeasureDataAckDTO dto)
		{
			if (ReceivedACIRMeasureDataCallback != null)
			{
				ReceivedACIRMeasureDataCallback(dto);
			}
		}

		#endregion // public task completed




		#region callback (multimeter)

		public delegate void dReceivedMultimeterMeasureData(int ch, string data);
		public dReceivedMultimeterMeasureData ReceivedMultimeterMeasureDataCallback { get; set; }

		/// <summary>
		/// handle multimeter push callback
		/// </summary>
		/// <param name="dto"></param>
		private void MultimeterPushCallback(PushDataDTO dto)
		{
			System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
			{
				switch (dto.PushDataType)
				{
					case PushDataDTO.PushDataTypes.Log:
						break;
					case PushDataDTO.PushDataTypes.Open:
						IsMultimeterOpened = Devices.MultimeterInstance.ConnectionState;
						break;
					case PushDataDTO.PushDataTypes.Close:
						IsMultimeterOpened = Devices.MultimeterInstance.ConnectionState;
						break;
					case PushDataDTO.PushDataTypes.Data:
						if (ReceivedMultimeterMeasureDataCallback != null) 
						{
							String strNumber;
							string value = (string) dto.Data;

							if (value.IndexOf("Keysight") == -1)
							{
								try
								{
									Double dValue = Convert.ToDouble(value);
									strNumber = dValue.ToString();
									
									//number = Decimal.Parse(value, System.Globalization.NumberStyles.AllowExponent | System.Globalization.NumberStyles.AllowDecimalPoint);
									//number = Decimal.ToInt32(decValue);
									//Console.WriteLine("{0} --> {1}", value, strNumber);

									strNumber = ( Math.Truncate( dValue * 1000) / 1000).ToString();
									Console.WriteLine("{0} --> {1}", value, strNumber);
				

									int ch = 1;
									ReceivedMultimeterMeasureDataCallback(ch, strNumber);
								}
								catch (OverflowException e)
								{
									Console.WriteLine("{0}: {1}", e.GetType().Name, value);
								}
								catch (Exception e)
								{
									Console.Out.WriteLine(e.ToString());
								}
							}
						}
						break;
					default:
						break;
				} // end switch
			});
		}

        #endregion // multimeter

        #region callback (acia)

        /// <summary>
        /// handle acir push callback
        /// </summary>
        /// <param name="dto"></param>
        private void ACIRPushCallback(PushDataDTO dto)
		{
			if (Application.Current == null) return;
			Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
			{
				switch (dto.PushDataType)
				{
					case PushDataDTO.PushDataTypes.Log:
						break;
					case PushDataDTO.PushDataTypes.Open:
						IsACIROpened = Devices.ACIRInstance.ConnectionState;
						break;
					case PushDataDTO.PushDataTypes.Close:
						IsACIROpened = Devices.ACIRInstance.ConnectionState;
						PublishTaskCompleted(false, -1, "ACIA 연결 해제됨", dto.ReqTaskSeq);
						break;
					case PushDataDTO.PushDataTypes.Data:
						if (dto.Data is ACIRLib.Data.DTO.TaskInfoDTO)
						{
							var data = dto.Data as ACIRLib.Data.DTO.TaskInfoDTO;

							// 검사 완료 시 처리
							if (data.State == TaskStates.Completed)
							{
								PublishTaskCompleted(true, 0, "", dto.ReqTaskSeq);
							}
                        }
						else if (dto.Data is ACIRLib.Data.DTO.MeasureDataAckDTO)
						{
							PublichReceivedACIRMeasureData(dto.Data as ACIRLib.Data.DTO.MeasureDataAckDTO);
						}
						break;
					default:
						break;
				} // end switch
			});
		}

        #endregion // callback (acia)

        #region callback (st5520)

        /// <summary>
        /// handle 절연저항시험기 push callback
        /// </summary>
        /// <param name="dto"></param>
        private void ST5520PushCallback(PushDataDTO dto)
		{
			System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
			{
				switch (dto.PushDataType)
				{
					case PushDataDTO.PushDataTypes.Log:
						break;
					case PushDataDTO.PushDataTypes.Open:
						IsST5520Opened = Devices.ST5520Instance.ConnectionState;
						break;
					case PushDataDTO.PushDataTypes.Close:
						IsST5520Opened = Devices.ST5520Instance.ConnectionState;
						PublishTaskCompleted(false, -1, "절연저항시험기 연결 해제됨", dto.ReqTaskSeq);
						break;
					case PushDataDTO.PushDataTypes.Data:
						{
							if (dto.Data is ResponseDTO)
							{
								var data = dto.Data as ResponseDTO;
							}
						}
						break;
					default:
						break;
				} // end switch
			});
		}

        #endregion // callback (st5520)

        #region callback (PNE CTS)

        /// <summary>
        /// handle pne cts push callback
        /// </summary>
        /// <param name="dto"></param>
        private void PNECTSPushCallback(PushDataDTO dto)
		{
			if (Application.Current == null) return;

			Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
			{
				switch (dto.PushDataType)
				{
					case PushDataDTO.PushDataTypes.Log:
						break;
					case PushDataDTO.PushDataTypes.Open:
						IsPNECTSOpened = Devices.PNECTSInstance.ConnectionState;
						break;
					case PushDataDTO.PushDataTypes.Close:
						IsPNECTSOpened = Devices.PNECTSInstance.ConnectionState;
						PublishTaskCompleted(false, -1, "충방전기 연결 해제됨", dto.ReqTaskSeq);
						break;
					case PushDataDTO.PushDataTypes.Data:
						ParseCTSData(dto);
						break;
					default:
						break;
				} // end switch
			});
		}

		/// <summary>
		/// parse pne cts data
		/// </summary>
		/// <param name="dto"></param>
		private void ParseCTSData(PushDataDTO dto)
		{
			if (dto.Data is PneCtsLib.Data.DTO.TaskInfoDTO)
			{
				var data = dto.Data as PneCtsLib.Data.DTO.TaskInfoDTO;
				//if (data.Channel != ChannelIndex) return;
               if (data.Channel != ChannelNum) return;        // todo: check index...

                // 검사 완료 시 처리
                if (data.State == TaskStates.Completed)
				{
					PublishTaskCompleted(true, 0, "", dto.ReqTaskSeq);
				}
                else if (data.State == TaskStates.Pause)
                {
                    PublishTaskCompleted(false, data.Code, "충방전기 Pause", data.ReqTaskSeq);
                }
            }
            else if (dto.Data is PneCtsLib.Data.DTO.ICTS_VARIABLE_CH_DATA_DTO)
			{
				var data = dto.Data as PneCtsLib.Data.DTO.ICTS_VARIABLE_CH_DATA_DTO;
			}
			else if (dto.Data is List<PneCtsLib.Data.DTO.CTS_VARIABLE_CH_DATA_DTO>)
			{
				
			}
		}

		#endregion

		#region callback (relay box)

		/// <summary>
		/// handle relay box push callback
		/// </summary>
		/// <param name="dto"></param>
		private void RelayBoxPushCallback(PushDataDTO dto)
		{
			switch (dto.PushDataType)
			{
				case PushDataDTO.PushDataTypes.Log:
					break;
				case PushDataDTO.PushDataTypes.Open:
					IsRelayBoxOpened = Devices.RelayBoxInstance.ConnectionState;
					break;
				case PushDataDTO.PushDataTypes.Close:
					IsRelayBoxOpened = Devices.RelayBoxInstance.ConnectionState;
					PublishTaskCompleted(false, -1, "릴레이 컨트롤러 연결 해제됨", dto.ReqTaskSeq);
					break;
				case PushDataDTO.PushDataTypes.Data:
					break;
				default:
					break;
			} // end switch
		}

        #endregion // callback (relay box)

        #region callback (bi box)

        /// <summary>
        /// handle bi box push callback
        /// </summary>
        /// <param name="dto"></param>
        private void BIBoxPushCallback(PushDataDTO dto)
		{
			System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
			{
				switch (dto.PushDataType)
				{
					case PushDataDTO.PushDataTypes.Log:
						break;
					case PushDataDTO.PushDataTypes.Open:
						IsBIBoxOpened = Devices.BIBoxInstance.ConnectionState;
						break;
					case PushDataDTO.PushDataTypes.Close:
						IsBIBoxOpened = Devices.BIBoxInstance.ConnectionState;
						PublishTaskCompleted(false, -1, "BI 연결 해제됨", dto.ReqTaskSeq);
						break;
					case PushDataDTO.PushDataTypes.Data:
						if (dto.Data is BIBoxLib.Data.DTO.TaskInfoDTO)
						{
							var data = dto.Data as BIBoxLib.Data.DTO.TaskInfoDTO;

							// 검사 완료 시 처리
							if (data.State == TaskStates.Completed)
							{
                                BaseLib.Helper.LogHelper.Debug($"{0}", $"BIBoxPushCallback(), #1");
                                PublishTaskCompleted(true, 0, "", dto.ReqTaskSeq);
							}
                            else if (data.State == TaskStates.CompletedBIVolt)
                            {
                                // 2C 방전시 BI 전압 측정 완료 이벤트 // ( 이후 온도 측정 대기 )
                                BaseLib.Helper.LogHelper.Debug($"{0}", $"BIBoxPushCallback(), #2");
                                PublishTaskProcessing(data.State, dto.ReqTaskSeq);
                            }
                            else if (data.State == TaskStates.CompletedBITemp)
                            {
                                // 2C 방전시 BI 온도 측정 완료 이벤트 // ( InspectionViewModel 에서 Task에 따라 판단하여 처리함. )
                                BaseLib.Helper.LogHelper.Debug($"{0}", $"BIBoxPushCallback(), #3");
                                PublishTaskProcessing(data.State, dto.ReqTaskSeq);
                            }
                            else if (data.State == TaskStates.PauseCellOverVoltage)
                            {
                                // 충전시 셀전압 over voltage 에러 처리
                                BaseLib.Helper.LogHelper.Debug($"{0}", $"BIBoxPushCallback(), #4");
                                PublishTaskCompleted(false, data.Code, "PauseCellOverVoltage", dto.ReqTaskSeq);
                            }
                            else if (data.State == TaskStates.PauseCellUnderVoltage)
                            {
                                // 충전시 셀전압 over voltage 에러 처리
                                BaseLib.Helper.LogHelper.Debug($"{0}", $"BIBoxPushCallback(), #4");
                                PublishTaskCompleted(false, data.Code, "PauseCellUnderVoltage", dto.ReqTaskSeq);
                            }
                        }
						break;
                    case PushDataDTO.PushDataTypes.DataBI_V:
                    case PushDataDTO.PushDataTypes.DataBI_T:
                        break;
                    default:
						break;
				} // end switch
			});
		}

        #endregion // callback (bi box)

        #region callback (ds3678)

        /// <summary>
        /// handle barcode scanner push callback
        /// </summary>
        /// <param name="dto"></param>
        private void BarcodeScannerPushCallback(PushDataDTO dto)
		{
			System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
			{
				switch (dto.PushDataType)
				{
					case PushDataDTO.PushDataTypes.Log:
						break;
					case PushDataDTO.PushDataTypes.Open:
						IsBarcodeScannerOpened = Devices.BarcodeScannerInstance.ConnectionState;
						break;
					case PushDataDTO.PushDataTypes.Close:
						IsBarcodeScannerOpened = Devices.BarcodeScannerInstance.ConnectionState;
						break;
					case PushDataDTO.PushDataTypes.Data:
						//SharedPreferences.Instance.BatteryBarcodeMap[ChannelIndex] = $"{dto.Data}";
						break;
					default:
						break;
				} // end switch
			});
		}

		#endregion
	}
}
