using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiLib.Defines
{
	public enum RestApiLogTypes
	{
		[StringValue("Common")]
		Common,

		[StringValue("Channel1")]
		Channel1,

		[StringValue("Channel2")]
		Channel2
	}
}
