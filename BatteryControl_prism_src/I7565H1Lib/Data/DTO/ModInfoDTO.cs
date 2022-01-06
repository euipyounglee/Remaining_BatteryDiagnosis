using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I7565H1Lib.Data.DTO
{
	public class ModInfoDTO
	{
		public string ModID { get; private set; }
		public string FWVer { get; private set; }
		public string HWSN { get; private set; }

		public ModInfoDTO()
		{
			ModID = "";
			FWVer = "";
			HWSN = "";
		}

		public ModInfoDTO(string modId, string fwVer, string hwsn)
		{
			ModID = modId;
			FWVer = fwVer;
			HWSN = hwsn;
		}
	}
}
