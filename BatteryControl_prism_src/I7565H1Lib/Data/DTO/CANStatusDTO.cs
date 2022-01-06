using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I7565H1Lib.Data.DTO
{
	public class CANStatusDTO
	{
		public byte CANNo { get; set; }
		public uint CurCANBaud { get; set; }
		public byte CANReg { get; set; }
		public byte CANTxErrCnt { get; set; }
		public byte CANRxErrCnt { get; set; }
		public byte ModState { get; set; }
	}
}
