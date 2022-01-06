using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectEngineLib.Defines
{
	public enum InspectionTypes
	{
		[StringValue("E")]
		None,

		[StringValue("S")]
		Simple,

		[StringValue("N")]
		Normal,

		[StringValue("C")]
		Close
	}

	public class InspectionTypeConverter
	{
		public static InspectionTypes Convert(string value)
		{
			if (value.Equals(StringEnum.GetStringValue(InspectionTypes.Simple)))
			{
				return InspectionTypes.Simple;
			}
			else if (value.Equals(StringEnum.GetStringValue(InspectionTypes.Normal)))
			{
				return InspectionTypes.Normal;
			}
			else if (value.Equals(StringEnum.GetStringValue(InspectionTypes.Close)))
			{
				return InspectionTypes.Close;
			}
			else
			{
				return InspectionTypes.None;
			}
		}
	}
}
