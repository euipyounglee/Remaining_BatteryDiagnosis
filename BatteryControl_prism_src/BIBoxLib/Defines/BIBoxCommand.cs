using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBoxLib.Defines
{
	public enum BIBoxCommand
	{
		[StringValue("Get Cell Voltage")]
		GetCellVoltage = 0xA1,

		[StringValue("Get Cell Temperature")]
		GetCellTemperature = 0xA2,

		[StringValue("Get Module Voltage")]
		GetModuleVoltage = 0xA3,

        [StringValue("Check Cell Voltage")]
        CheckCellVoltage = 0xB1,

        [StringValue("Get Status")]
		GetStatus = 0xF0
	}
}
