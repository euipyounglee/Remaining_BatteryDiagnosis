using BaseLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Data.DTO
{
	public class TaskInfoDTO
	{
		/// <summary>
		/// task state
		/// </summary>
		public TaskStates State { get; private set; }

        public int Code { get; private set; }

		/// <summary>
		/// module
		/// </summary>
		public int Module { get; private set; }

		/// <summary>
		/// channel
		/// </summary>
		public int Channel { get; private set; }

        public int ReqTaskSeq { get; private set; }

        public TaskInfoDTO(TaskStates state, int module, int channel_num)
		{
			State = state;
			Module = module;
			Channel = channel_num;
            Code = 0;
		}

        public TaskInfoDTO(TaskStates state, int module, int channel_num, int code)
        {
            State = state;
            Module = module;
            Channel = channel_num;
            Code = code;
        }

        public TaskInfoDTO(TaskStates state, int module, int channel_num, int code, int seq)
        {
            State = state;
            Module = module;
            Channel = channel_num;
            Code = code;
            ReqTaskSeq = seq;
        }
    }
}
