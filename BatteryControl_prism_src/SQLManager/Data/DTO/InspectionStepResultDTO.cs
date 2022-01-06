using SQLManager.Data.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class InspectionStepResultDTO
	{
		#region property

		/// <summary>
		/// task step information
		/// </summary>
		public tbl_task_run_detail_DTO TaskStepInfo { get; private set; }

		/// <summary>
		/// pne cts data
		/// </summary>
		public List<tbl_log_cts_chdata_DTO> PneCtsDataList { get; private set; }

		/// <summary>
		/// bi box data
		/// </summary>
		public List<tbl_log_bi_data_DTO> BIBoxDataList { get; private set; }

		/// <summary>
		/// acir measure data
		/// </summary>
		public List<tbl_log_acir_measuredata_DTO> ACIRMeasureDataList { get; private set; }

		/// <summary>
		/// acir measure result (spectrum only)
		/// </summary>
		public List<tbl_log_acir_measureresult_DTO> ACIRMeasureResultList { get; private set; }

		#endregion

		#region constructor

		public InspectionStepResultDTO(tbl_task_run_detail_DTO dto)
		{
			TaskStepInfo = dto;
			PneCtsDataList = new tbl_log_cts_chdata().Get(TaskStepInfo.run_id, TaskStepInfo.task_seq);
			BIBoxDataList = new tbl_log_bi_data().Get(TaskStepInfo.run_id, TaskStepInfo.task_seq);
			ACIRMeasureDataList = new tbl_log_acir_measuredata().Get(TaskStepInfo.run_id, TaskStepInfo.task_seq);
			ACIRMeasureResultList = new tbl_log_acir_measureresult().Get(TaskStepInfo.run_id, TaskStepInfo.task_seq);
		}

		#endregion
	}
}
