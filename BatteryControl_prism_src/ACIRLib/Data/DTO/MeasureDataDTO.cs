using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACIRLib.Data.DTO
{
	public class MeasureDataDTO : ABaseStreamDTO
	{
		#region property

		/// <summary>
		/// data length
		/// </summary>
		public const int DATA_LENGTH = 10;

		/// <summary>
		/// 1 ~ 16: 셀
		/// 0x00: 전체
		/// </summary>
		public byte BatteryType
		{
			get
			{
				return Source[DATA_OFFSET];
			}
			set
			{
				Source[DATA_OFFSET] = value;
			}
		}

		/// <summary>
		/// 0x00:single, 0x01:spectrum
		/// </summary>
		public byte Mode
		{
			get
			{
				return Source[DATA_OFFSET + 1];
			}
			set
			{
				Source[DATA_OFFSET + 1] = value;
			}
		}

		/// <summary>
		/// frequency
		/// </summary>
		public float Frequency
		{
			get
			{
				var f = BitConverter.ToInt32(Source, DATA_OFFSET + 2);
				return float.IsNaN(f) ? 0f : f;
			}
			set
			{
				byte[] values = BitConverter.GetBytes(value);
				Array.Copy(values, 0, Source, DATA_OFFSET + 2, values.Length);
			}
		}

		/// <summary>
		/// voltage (unavailable)
		/// </summary>
		public float Voltage
		{
			get
			{
				var f = BitConverter.ToInt32(Source, DATA_OFFSET + 6);
				return float.IsNaN(f) ? 0f : f;
			}
			set
			{
				byte[] values = BitConverter.GetBytes(value);
				Array.Copy(values, 0, Source, DATA_OFFSET + 6, values.Length);
			}
		}

		#endregion

		#region method

		public MeasureDataDTO(byte batteryType, byte mode, float frequency) : base(FIXED_LENGTH + DATA_LENGTH)
		{
			STX = 0x99;
			CMD = 0x82;
			CurrentNumber = 0x01;
			TotalNumber = 0x01;

			BatteryType = batteryType;
			Mode = mode;
			Frequency = frequency;

			ETX = Core.Core.ETX;
		}

		public byte[] ToStream()
		{
			CheckSum = Core.CRCHelper.CalCheckSum(Source);
			return Source;
		}

		#endregion
	}
}
