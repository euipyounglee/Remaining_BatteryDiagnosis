using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectEngineLib.Defines
{
	public enum TaskGroupTags
	{
		[StringValue("Before")]
		Before,

		[StringValue("After")]
		After,

        [StringValue("Check")]
        Check
    }
}
