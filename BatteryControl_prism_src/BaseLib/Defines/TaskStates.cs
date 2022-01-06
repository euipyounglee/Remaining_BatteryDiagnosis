using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Defines
{
	public enum TaskStates
	{
		Disabled,
		Ready,
		Running,
		Pause,
        PauseCellUnderVoltage,
        PauseCellOverVoltage,
        Completed,
        CompletedBIVolt,
        CompletedBITemp,
    }
}
