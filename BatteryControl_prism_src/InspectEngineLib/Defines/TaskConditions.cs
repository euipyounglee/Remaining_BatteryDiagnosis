using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectEngineLib.Defines
{
    public enum TaskDetailName
    {
        [StringValue("ACIR")]
        ACIR,

        [StringValue("Charge")]
        Charge,

        [StringValue("Discharge")]
        Discharge,

        [StringValue("Measure")]
        Measure,

        [StringValue("Rest")]
        Rest,
    }

    public enum TaskConditions
	{
		/// <summary>
		/// PNE CTS
		/// </summary>
		[StringValue("Rest")]
		Rest,

		/// <summary>
		/// PNE CTS
		/// </summary>
		[StringValue("Charge")]
		Charge,

		/// <summary>
		/// PNE CTS
		/// </summary>
		[StringValue("Discharge")]
		Discharge,

		/// <summary>
		/// ACIR
		/// </summary>
		[StringValue("Single")]
		Single,

		/// <summary>
		/// ACIR
		/// </summary>
		[StringValue("Spectrum")]
		Spectrum,

		/// <summary>
		/// BI Box
		/// </summary>
		[StringValue("Voltage")]
		Voltage,

		/// <summary>
		/// BI Box
		/// </summary>
		[StringValue("Temperature")]
		Temperature
	}
}
