using Prism.Mvvm;
using SQLManager.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class tbl_task_result_VM : BindableBase
	{
		#region property

		/// <summary>
		/// dto
		/// </summary>
		private tbl_task_result_DTO m_Dto;
		public tbl_task_result_DTO Dto
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

		public string run_id
		{
			get
			{
				return Dto.run_id;
			}
		}

		public DateTime evaluation_dt
		{
			get
			{
				return Dto.evaluation_dt;
			}
		}

		public string evaluation_type
		{
			get
			{
				return Dto.evaluation_type;
			}
		}

		public TimeSpan time_required
		{
			get
			{
				return Dto.time_required;
			}
		}

		public string grade
		{
			get
			{
				return Dto.grade;
			}
		}

		public float soc
		{
			get
			{
				return Dto.soc;
			}
		}

		public float soh
		{
			get
			{
				return Dto.soh;
			}
		}

		public float sob
		{
			get
			{
				return Dto.sob;
			}
		}

		public float sop
		{
			get
			{
				return Dto.sop;
			}
		}

		public string BTRY_CODE
		{
			get
			{
				return Dto.BTRY_CODE;
			}
		}

		public string BTRY_TY
		{
			get
			{
				return Dto.BTRY_TY;
			}
		}

		public string MARK_CODE
		{
			get
			{
				return Dto.MARK_CODE;
			}
		}

		public string MAKR_DESC
		{
			get
			{
				return Dto.MAKR_DESC;
			}
		}

		public string MODEL_CODE
		{
			get
			{
				return Dto.MODEL_CODE;
			}
		}

		public string MODL_DESC
		{
			get
			{
				return Dto.MODL_DESC;
			}
		}

		public string CONFIG
		{
			get
			{
				return Dto.CONFIG;
			}
		}

		public DateTime create_dt
		{
			get
			{
				return Dto.create_dt;
			}
		}

		public DateTime update_dt
		{
			get
			{
				return Dto.update_dt;
			}
		}

		#endregion

		#region constructor

		public tbl_task_result_VM(tbl_task_result_DTO dto)
		{
			Dto = dto;
		}

		#endregion
	}
}
