using BaseLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACIRLib.Data.DTO
{
	public class TaskInfoDTO
	{
		public enum TaskModes
		{
			None,
			Single,
			Spectrum
		}

		/// <summary>
		/// task state
		/// </summary>
		public TaskStates State { get; private set; }

		/// <summary>
		/// task mode
		/// </summary>
		public TaskModes Mode { get; private set; }

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="state"></param>
		/// <param name="mode"></param>
		public TaskInfoDTO(TaskStates state, TaskModes mode)
		{
			State = state;
			Mode = mode;
		}

	}
}
