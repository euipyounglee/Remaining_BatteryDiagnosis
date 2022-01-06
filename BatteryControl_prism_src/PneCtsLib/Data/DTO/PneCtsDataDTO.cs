using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Data.DTO
{
	public class PneCtsDataDTO
	{
		#region constructor

		public PneCtsDataDTO()
		{
			CtsChannelDataMap = new Dictionary<byte, CTS_CH_DATA_DTO>();
		}

		#endregion

		#region property

		private Dictionary<byte, CTS_CH_DATA_DTO> CtsChannelDataMap { get; set; }

		#endregion

		#region method

		public void UpdateData(CTS_CH_DATA_DTO dto)
		{
			CTS_CH_DATA_DTO data = Get(dto.ChNo);
			if (data == null)
			{
				CtsChannelDataMap.Add(dto.ChNo, dto);
			}
			else
			{
				CtsChannelDataMap[dto.ChNo] = dto;
			}
		}

		public CTS_CH_DATA_DTO Get(byte channelNo)
		{
            CTS_CH_DATA_DTO chData;
			if (!CtsChannelDataMap.TryGetValue(channelNo, out chData))
			{
                return null;
			}
			else
			{
                return chData;
			}
		}

		/// <summary>
		/// get voltage
		/// </summary>
		/// <param name="channelNo"></param>
		/// <returns></returns>
		public int GetVoltage(byte channelNo)  
        {
            CTS_CH_DATA_DTO dto = Get(channelNo);   // 1, 2

            //return dto == null ? 0 : dto.Voltage;
            if (dto == null)
            {
                return 0;
            }
            else
            {
                return dto.Voltage;
            }

        }

		/// <summary>
		/// get current
		/// </summary>
		/// <param name="channelNo"></param>
		/// <returns></returns>
		public int GetCurrent(byte channelIndex)  // 0, 1 ???  // channelNo ---> channelIndex
        {
			CTS_CH_DATA_DTO dto = Get(channelIndex);
			return dto == null ? 0 : dto.Current;
		}

        public int GetCapacity(byte channelIndex)  // 0, 1 ???  // channelNo ---> channelIndex
        {
            CTS_CH_DATA_DTO dto = Get(channelIndex);
            return dto == null ? 0 : ( dto.Current > 0 ? dto.ChargeAh : dto.DisChargeAh);
        }

        /// <summary>
        /// clear data
        /// </summary>
        public void Clear()
		{
			CtsChannelDataMap.Clear();
		}

		#endregion
	}
}
