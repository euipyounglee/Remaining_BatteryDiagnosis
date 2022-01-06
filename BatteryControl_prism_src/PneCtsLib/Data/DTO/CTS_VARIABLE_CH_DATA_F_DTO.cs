using PneCtsLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Data.DTO
{
	public class CTS_VARIABLE_CH_DATA_F_DTO : ICTS_VARIABLE_CH_DATA_DTO
	{
		#region property

		/// <summary>
		/// 기본 채널 데이터
		/// </summary>
		public CTS_CH_DATA_DTO ChData { get; set; }

		/// <summary>
		/// Aux Data
		/// </summary>
		public List<CTS_AUX_DATA_DTO> AuxData { get; set; }

		/// <summary>
		/// Can Data
		/// </summary>
		public List<CTS_CAN_DATA_DTO> CanData { get; set; }

		#endregion

		#region method

		public CTS_VARIABLE_CH_DATA_F_DTO(CTS_VARIABLE_CH_DATA_F src)
		{
			ChData = new CTS_CH_DATA_DTO(src.chData);

			AuxData = new List<CTS_AUX_DATA_DTO>();
			foreach (var data in src.auxData)
			{
				AuxData.Add(new CTS_AUX_DATA_DTO(data));
			}

			CanData = new List<CTS_CAN_DATA_DTO>();
			foreach (var data in src.canData)
			{
				CanData.Add(new CTS_CAN_DATA_DTO(data));
			}
		}

		#endregion
	}
}
