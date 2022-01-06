using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Data.DTO
{
	public class CurrentTaskInfoDTO
	{
		#region property

		public string RunId { get; private set; }

		public int TaskId { get; private set; }

		public int TaskSeq { get; private set; }

        public int TaskStepNo { get; private set; }


        public bool IsEmpty
		{
			get
			{
				return "".Equals(RunId) || TaskId == 0 || TaskSeq == 0;
			}
		}

		#endregion

		#region constructor

		public CurrentTaskInfoDTO(string runId, int taskId, int taskSeq, int taskStepNo)
        {
			Set(runId, taskId, taskSeq, taskStepNo);
		}

		#endregion

		#region method

		public void Clear()
		{
			RunId = "";
			TaskId = 0;
			TaskSeq = 0;
            TaskStepNo = 0;
		}

		public void Set(string runId, int taskId, int taskSeq, int taskStepNo)
		{
			RunId = runId;
			TaskId = taskId;
			TaskSeq = taskSeq;
            TaskStepNo = taskStepNo;
        }

		#endregion


	}
}
