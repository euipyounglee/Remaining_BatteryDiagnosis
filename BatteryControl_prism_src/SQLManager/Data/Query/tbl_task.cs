using SQLManager.Data.DTO;
using SQLManager.MySQL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.Query
{
	public class tbl_task : QueryManager
	{
		/// <summary>
		/// get tasks
		/// </summary>
		/// <returns></returns>
		public List<tbl_task_DTO> GetTasks(string taskType, string BTRY_CODE)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("select a.task_id, a.task_name, a.task_type, ");
            sb.Append("b.task_seq, b.step_no, b.sub_step_no, b.task_detail_name, b.device_cd, b.device_name, b.task_condition, ");
            sb.Append("b.task_group, b.task_tag, b.time_expect, b.file_path, b.disabled, ");
			sb.Append("b.visibility, b.BTRY_CODE ");
			sb.Append("from tbl_task a, tbl_task_detail b ");
			sb.Append($"where a.task_type = '{taskType}' ");
			sb.Append("and a.task_id = b.task_id ");
			sb.Append($"and (length(b.BTRY_CODE) = 0 || b.BTRY_CODE = '{BTRY_CODE}') ");
			sb.Append("order by a.task_id, b.task_seq ");

			return Get<tbl_task_DTO>(sb.ToString());
		}

	}
}
