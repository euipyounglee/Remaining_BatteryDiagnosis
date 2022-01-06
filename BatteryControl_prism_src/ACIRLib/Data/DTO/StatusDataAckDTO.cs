using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACIRLib.Data.DTO
{
	public class StatusDataAckDTO : ABaseStreamDTO
	{
		#region property

		/// <summary>
		/// data length
		/// </summary>
		public const int DATA_LENGTH = 2;

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

		#endregion

		#region method

		public StatusDataAckDTO(byte cellCount) : base(new byte[FIXED_LENGTH + DATA_LENGTH])
		{
			STX = 0x99;
			CMD = 0x81;
			CurrentNumber = 0x01;
			TotalNumber = 0x01;

			Value1 = cellCount;
			Value2 = 0x01;

			CheckSum = Core.CRCHelper.CalCheckSum(Source);
		}

		public byte[] ToStream()
		{
			CheckSum = Core.CRCHelper.CalCheckSum(Source);
			return Source;
		}

		#endregion
	}
}
