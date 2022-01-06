using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACIRLib.Data.DTO
{
	public class MeasureDataAckDTO : ABaseStreamDTO
	{
		#region property

		/// <summary>
		/// data length
		/// </summary>
		public const int DATA_LENGTH = 34;

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
		/// ng:0, ok:1
		/// </summary>
		public byte Value
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
		/// step no
		/// </summary>
		public byte StepNo
		{
			get
			{
				return Source[DATA_OFFSET + 2];
			}
			set
			{
				Source[DATA_OFFSET + 2] = value;
			}
		}

		/// <summary>
		/// current no
		/// </summary>
		public byte CurrentNo
		{
			get
			{
				return Source[DATA_OFFSET + 3];
			}
			set
			{
				Source[DATA_OFFSET + 3] = value;
			}
		}

		/// <summary>
		/// total no
		/// single: 2
		/// spectrum(6 point) : 19
		/// </summary>
		public byte TotalNo
		{
			get
			{
				return Source[DATA_OFFSET + 4];
			}
			set
			{
				Source[DATA_OFFSET + 4] = value;
			}
		}

		/// <summary>
		/// mode
		/// 0x00: single
		/// 0x01: spectrum
		/// </summary>
		public byte Mode
		{
			get
			{
				return Source[DATA_OFFSET + 5];
			}
			set
			{
				Source[DATA_OFFSET + 5] = value;
			}
		}

		/// <summary>
		/// 전압
		/// </summary>
		public float Voltage
		{
			get
			{
				var f = BitConverter.ToSingle(Source, DATA_OFFSET + 6);
				return float.IsNaN(f) ? 0f : f;
			}
			set
			{
				byte[] values = BitConverter.GetBytes(value);
				Array.Copy(values, 0, Source, DATA_OFFSET + 6, values.Length);
			}
		}

		/// <summary>
		/// 온도
		/// </summary>
		public float Temp
		{
			get
			{
				var f = BitConverter.ToSingle(Source, DATA_OFFSET + 10);
				return float.IsNaN(f) ? 0f : f;
			}
			set
			{
				byte[] values = BitConverter.GetBytes(value);
				Array.Copy(values, 0, Source, DATA_OFFSET + 10, values.Length);
			}
		}

		/// <summary>
		/// hz
		/// </summary>
		public float Hz
		{
			get
			{
				var f = BitConverter.ToSingle(Source, DATA_OFFSET + 14);
				return float.IsNaN(f) ? 0f : f;
			}
			set
			{
				byte[] values = BitConverter.GetBytes(value);
				Array.Copy(values, 0, Source, DATA_OFFSET + 14, values.Length);
			}
		}

		/// <summary>
		/// re
		/// </summary>
		public float Re
		{
			get
			{
				var f = BitConverter.ToSingle(Source, DATA_OFFSET + 18);
				return float.IsNaN(f) ? 0f : f;
			}
			set
			{
				byte[] values = BitConverter.GetBytes(value);
				Array.Copy(values, 0, Source, DATA_OFFSET + 18, values.Length);
			}
		}

		/// <summary>
		/// im
		/// </summary>
		public float Im
		{
			get
			{
				var f = BitConverter.ToSingle(Source, DATA_OFFSET + 22);
				return float.IsNaN(f) ? 0f : f;
			}
			set
			{
				byte[] values = BitConverter.GetBytes(value);
				Array.Copy(values, 0, Source, DATA_OFFSET + 22, values.Length);
			}
		}

		/// <summary>
		/// reserved 1
		/// </summary>
		public uint Reserved
		{
			get
			{
				return BitConverter.ToUInt32(Source, DATA_OFFSET + 26);
			}
			set
			{
				byte[] values = BitConverter.GetBytes(value);
				Array.Copy(values, 0, Source, DATA_OFFSET + 26, values.Length);
			}
		}

		/// <summary>
		/// reserved 2
		/// </summary>
		public uint Reserved2
		{
			get
			{
				return BitConverter.ToUInt32(Source, DATA_OFFSET + 30);
			}
			set
			{
				byte[] values = BitConverter.GetBytes(value);
				Array.Copy(values, 0, Source, DATA_OFFSET + 30, values.Length);
			}
		}

		#endregion

		#region method

		public MeasureDataAckDTO(byte[] data) : base(data)
		{

		}

		#endregion
	}
}
