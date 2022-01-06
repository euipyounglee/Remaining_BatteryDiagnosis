using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Defines
{
	public enum LogDev
	{
		[StringValue("Unknown")]
		Unknown,

		/// <summary>
		/// 기본
		/// </summary>
		[StringValue("gbtp2020")]
		APP,

		/// <summary>
		/// 충방전기
		/// </summary>
		[StringValue("충방전기")]
        Cycler,                     // changed, PNE_CTS --> Cycler

        /// <summary>
        /// USB to CAN converter
        /// </summary>
        [StringValue("USB to CAN Converter")]
		I_7565_H1,

		/// <summary>
		/// 바코드 스캐너
		/// </summary>
		[StringValue("Zebra Barcode Scanner")]
		DS3678,

		/// <summary>
		/// 절연저항
		/// </summary>
		[StringValue("절연저항시험기")]
		ST5520,

		/// <summary>
		/// 릴레이 박스 컨트롤러
		/// </summary>
		[StringValue("Relay Box Controller")]
		RELAY_BOX,

		/// <summary>
		/// bi box
		/// </summary>
		[StringValue("BI Box Controller")]
		BI_BOX,

		/// <summary>
		/// multimeter
		/// </summary>
		[StringValue("Digit Multimeter 34461A")]
		MULTIMETER,

		/// <summary>
		/// acir
		/// </summary>
		[StringValue("Impedance Meter")]
		ACIR,

        /// <summary>
        /// acir
        /// </summary>
        [StringValue("LOCAL")]
        LOCAL
    }
}
