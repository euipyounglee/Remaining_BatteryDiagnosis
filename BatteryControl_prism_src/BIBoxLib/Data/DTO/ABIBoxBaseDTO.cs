using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBoxLib.Data.DTO
{
	public abstract class ABIBoxBaseDTO
	{
		#region property

		public byte MBMSId { get; private set; }

		#endregion

		#region method

		public ABIBoxBaseDTO(uint canId)
		{
			MBMSId = (byte)(canId - 0x110);
		}

		/// <summary>
		/// to short
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public short ToShort(byte a, byte b)
		{
			return (short)(((0x0000 | a) << 8) + b);
		}

		#endregion
	}
}
