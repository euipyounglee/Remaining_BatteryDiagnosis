using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACIRLib.Data.DTO
{
	public abstract class ABaseStreamDTO
	{
		#region property

		public const int FIXED_LENGTH = 4 + 2;

		public const int DATA_OFFSET = 4;

		/// <summary>
		/// original source
		/// </summary>
		public byte[] Source { get; private set; }

		/// <summary>
		/// stx
		/// </summary>
		public byte STX
		{
			get
			{
				return Source[0];
			}
			set
			{
				Source[0] = value;
			}
		}

		/// <summary>
		/// cmd
		/// </summary>
		public byte CMD
		{
			get
			{
				return Source[1];
			}
			set
			{
				Source[1] = value;
			}
		}

		public byte CurrentNumber
		{
			get
			{
				return Source[2];
			}
			set
			{
				Source[2] = value;
			}
		}

		public byte TotalNumber
		{
			get
			{
				return Source[3];
			}
			set
			{
				Source[3] = value;
			}
		}

		public byte CheckSum
		{
			get
			{
				return Source[Source.Length - 2];
			}
			set
			{
				Source[Source.Length - 2] = value;
			}
		}

		public byte ETX
		{
			get
			{
				return Source[Source.Length - 1];
			}
			set
			{
				Source[Source.Length - 1] = value;
			}
		}

		#endregion

		#region constructor

		public ABaseStreamDTO(byte[] source)
		{
			Source = source;
		}

		public ABaseStreamDTO(int len)
		{
			Source = new byte[len];
		}

		#endregion
	}
}
