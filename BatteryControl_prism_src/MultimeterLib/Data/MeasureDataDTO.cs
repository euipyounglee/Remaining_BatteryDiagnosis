using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultimeterLib.Data.DTO
{
    public class MeasureDataDTO
    {
		public string Query { get; private set; }

		public string Data { get; private set; }

		public MeasureDataDTO(string cmd, string data)
		{
			Query = cmd;
			Data = data;
		}
	}
}
