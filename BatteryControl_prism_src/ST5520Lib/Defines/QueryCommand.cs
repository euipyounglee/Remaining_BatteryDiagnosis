using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST5520Lib.Defines
{
	public enum QueryCommand
	{
		[StringValue("*IDN?")]
		IDN,

		[StringValue(":MEASure?")]
		Measure,

		[StringValue(":VOLTage?")]
		Voltage
	}
}
