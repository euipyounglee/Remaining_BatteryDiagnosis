using BaseLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBoxLib.Data.DTO
{
	public class TaskInfoDTO
	{
		/// <summary>
		/// task state
		/// </summary>
		public TaskStates State { get; private set; }
        public int Code { get; private set; }

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="state"></param>
		public TaskInfoDTO(TaskStates state)
		{
			State = state;
            Code = 0;
        }


        public TaskInfoDTO(TaskStates state, int code)
        {
            State = state;
            Code = code;
        }
    }
}
