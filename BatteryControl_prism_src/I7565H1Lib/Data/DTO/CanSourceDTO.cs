using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I7565H1Lib.Data.DTO
{
	public class CanSourceDTO
	{
		public DateTime LogDt { get; set; }

		public byte Mode { get; set; }

		public byte RTR { get; set; }

		public byte DLC { get; set; }

		public string CanId { get; set; }

		public string MessageTime { get; set; }

		public string HexPrint { get; set; }
	}
}
