using SQLManager.Data.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class InspectionResultDTO
	{
		#region property

		/// <summary>
		/// battery info
		/// </summary>
		public pg_btry_info_DTO BatteryInfo { get; private set; }

		/// <summary>
		/// task info
		/// </summary>
		public tbl_task_run_DTO TaskInfo { get; private set; }

		/// <summary>
		/// pne cts measure data
		/// </summary>
		public tbl_log_cts_measuredata_DTO PneCtsMeasureData { get; private set; }

        public tbl_log_bi_data_DTO BIBoxData { get; private set; }

        /// <summary>
        /// task step data
        /// </summary>
        public List<InspectionStepResultDTO> TaskStepDataList { get; private set; }

		#endregion

		#region constructor

		private InspectionResultDTO()
		{
			TaskStepDataList = new List<InspectionStepResultDTO>();
		}

		#endregion

		#region method

		public static InspectionResultDTO Create(string runId)
		{
			var taskInfo = new tbl_task_run().Get(runId);
			if (taskInfo == null) return null;

			var dto = new InspectionResultDTO();
			dto.TaskInfo = taskInfo;

			var taskDetails = new tbl_task_run_detail().Get(runId);
			foreach (var taskDetail in taskDetails)
			{
				dto.TaskStepDataList.Add(new InspectionStepResultDTO(taskDetail));
			} // end foreach
			if (dto.TaskStepDataList.Count == 0) return null;

			dto.PneCtsMeasureData = new tbl_log_cts_measuredata().Get(runId);
			if (dto.PneCtsMeasureData == null) return null;

            dto.BatteryInfo = new pg_btry_info().Get(dto.TaskInfo.BTRY_CODE);
			if (dto.BatteryInfo == null) return null;

			return dto;
		}

		#endregion

	}
}
