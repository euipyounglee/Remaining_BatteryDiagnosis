using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACIRLib.Data.DTO
{
	public class StatusDataDTO : ABaseStreamDTO
	{
		#region property

		/// <summary>
		/// data length
		/// </summary>
		public const int DATA_LENGTH = 10;

		/// <summary>
		/// 0x01: 정상
		/// 0x00: 오류 (에러코드 참조)
		/// </summary>
		public byte Value1
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
		public byte Value2
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
		/// 전압
		/// </summary>
		public float Voltage
		{
			get
			{
				var f = BitConverter.ToSingle(Source, DATA_OFFSET + 2);
				return float.IsNaN(f) ? 0f : f;
			}
			set
			{
				byte[] values = BitConverter.GetBytes(value);
				Array.Copy(values, 0, Source, DATA_OFFSET + 2, values.Length);
			}
		}

		/// <summary>
		/// 온도
		/// </summary>
		public float Temperature
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

		#endregion

		#region method

		public StatusDataDTO(byte[] data) : base(data)
		{

		}

		#endregion
	}
}
