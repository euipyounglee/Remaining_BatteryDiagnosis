using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Defines
{
	public enum LogLevels
	{
		[StringValue("Debug")]
		D,

		[StringValue("Error")]
		E,

		[StringValue("Information")]
		I
	}
}
