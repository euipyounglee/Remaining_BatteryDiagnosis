using BaseLib.Defines;
using InspectEngineLib.Defines;
using PneCtsLib.Data.DTO;
using Prism.Mvvm;
using SQLManager.Data.DTO;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class tbl_task_VM : BindableBase
	{
		#region property

		/// <summary>
		/// dto
		/// </summary>
		private tbl_task_DTO m_Dto;
		public tbl_task_DTO Dto
		{
			get
			{
				return m_Dto;
			}
			set
			{
				m_Dto = value;
				RaisePropertyChanged("Dto");
			}
		}

		public int task_id
		{
			get
			{
				return Dto.task_id;
			}
		}

		public string task_name
		{
			get
			{
				return Dto.task_name;
			}
		}

		public InspectionTypes task_type
		{
			get
			{
				switch (Dto.task_type)
				{
					case "S": return InspectionTypes.Simple;
					case "N": return InspectionTypes.Normal;
					case "C": return InspectionTypes.Close;
					default: return InspectionTypes.None;
				}
			}
		}

		public int task_seq
		{
			get
			{
				return Dto.task_seq;
			}
		}

// add, task step_no {
        public int step_no
        {
            get
            {
                return Dto.step_no;
            }
        }

        public int sub_step_no
        {
            get
            {
                return Dto.sub_step_no;
            }
        }
        // add, task step_no }

        public string task_detail_name
		{
			get
			{
				return Dto.task_detail_name;
			}
		}

		public LogDev device_cd
		{
			get
			{
				if ($"{LogDev.ACIR}".Equals(Dto.device_cd)) return LogDev.ACIR;
				else if ($"{LogDev.Cycler}".Equals(Dto.device_cd)) return LogDev.Cycler;
				else if ($"{LogDev.BI_BOX}".Equals(Dto.device_cd)) return LogDev.BI_BOX;
				else return LogDev.Unknown;
			}
		}

		public string device_name
		{
			get
			{
				return Dto.device_name;
			}
		}

		public string task_condition
		{
			get
			{
				return Dto.task_condition;
			}
		}

		public int task_group
		{
			get
			{
				return Dto.task_group;
			}
		}

		public string task_tag
		{
			get
			{
				return Dto.task_tag;
			}
		}

		public int time_expect
		{
			get
			{
				return Dto.time_expect;
			}
		}

		public string file_path
		{
			get
			{
				return Dto.file_path;
			}
		}

		public bool disabled
		{
			get
			{
				return "Y".Equals(Dto.disabled);
			}
		}

		public bool visibility
		{
			get
			{
				return "Y".Equals(Dto.visibility);
			}
		}

		public string BTRY_CODE
		{
			get
			{
				return Dto.BTRY_CODE;
			}
		}

		#endregion

		#region property (extra)

		public DateTime BeginTime { get; set; }

		/// <summary>
		/// elapsed time
		/// </summary>
		private TimeSpan m_ElapsedTime;
		public TimeSpan ElapsedTime
		{
			get
			{
				return m_ElapsedTime;
			}
			set
			{
				m_ElapsedTime = value;
				RaisePropertyChanged("ElapsedTime");
			}
		}

		/// <summary>
		/// task state
		/// </summary>
		private TaskStates m_TaskState;
		public TaskStates TaskState
		{
			get
			{
				return m_TaskState;
			}
			set
			{
				m_TaskState = value;
				RaisePropertyChanged("TaskState");
			}
		}

		#endregion

		#region method

		public tbl_task_VM(tbl_task_DTO dto)
		{
			Dto = dto;

			TaskState = disabled ? TaskStates.Disabled : TaskStates.Ready;
		}

		#endregion
	}
}
