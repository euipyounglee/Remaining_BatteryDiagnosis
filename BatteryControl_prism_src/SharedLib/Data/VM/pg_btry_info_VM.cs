using Prism.Mvvm;
using SQLManager.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class pg_btry_info_VM : BindableBase
	{
		#region property

		/// <summary>
		/// source
		/// </summary>
		private pg_btry_info_DTO m_Dto;
		public pg_btry_info_DTO Dto
		{
			get
			{
				return m_Dto;
			}
			set
			{
				m_Dto = value;
				RaisePropertyChanged("Dto");
			}
		}

		public uint NO
		{
			get
			{
				return Dto.NO;
			}
		}

		public string BTRY_CODE
		{
			get
			{
				return Dto.BTRY_CODE;
			}
		}

		public string BTRY_TY
		{
			get
			{
				return Dto.BTRY_TY;
			}
		}

		public string MARK_CODE
		{
			get
			{
				return Dto.MARK_CODE;
			}
		}

		public string MAKR_DESC
		{
			get
			{
				return Dto.MAKR_DESC;
			}
		}

		public string MODEL_CODE
		{
			get
			{
				return Dto.MODEL_CODE;
			}
		}

		public string MODL_DESC
		{
			get
			{
				return Dto.MODL_DESC;
			}
		}

		public string CONFIG
		{
			get
			{
				return Dto.CONFIG;
			}
		}

		public int TYPE_P
		{
			get
			{
				return Dto.TYPE_P;
			}
		}

		public int TYPE_S
		{
			get
			{
				return Dto.TYPE_S;
			}
		}

        public float CELL_MIN_VLTGE
        {
            get
            {
                return Dto.CELL_MIN_VLTGE;
            }
        }

        public float CELL_MAX_VLTGE
        {
            get
            {
                return Dto.CELL_MAX_VLTGE;
            }
        }

        /// <summary>
        /// 전압 V
        /// </summary>
        public float VLTGE
		{
			get
			{
				return Dto.VLTGE;
			}
		}

		/// <summary>
		/// 배터리 용량 Ah
		/// </summary>
		public float CPCTY
		{
			get
			{
				return Dto.CPCTY;
			}
		}

		/// <summary>
		/// 최소전압 V
		/// </summary>
		public float MUMM_VLTGE
		{
			get
			{
				return Dto.MUMM_VLTGE;
			}
		}

		/// <summary>
		/// 최대전압 V
		/// </summary>
		public float MXMM_VLTGE
		{
			get
			{
				return Dto.MXMM_VLTGE;
			}
		}

		/// <summary>
		/// 최소전압 경계 V
		/// </summary>
		public float MUMM_VLTGE_LIMIT
		{
			get
			{
				return Dto.MUMM_VLTGE_LIMIT;
			}
		}

		/// <summary>
		/// 배터리 에너지 kWh
		/// </summary>
		public float BTRY_ENERGY
		{
			get
			{
				return Dto.BTRY_ENERGY;
			}
		}

		#endregion

		#region property (extra)

		/// <summary>
		/// display battery summary
		/// </summary>
		public string Summary
		{
			get
			{
				return $"{MAKR_DESC}자동차 {MODL_DESC} {CONFIG}";
			}
		}

		#endregion

		#region method

		public pg_btry_info_VM(pg_btry_info_DTO dto)
		{
			Dto = dto;
		}

		#endregion
	}
}
