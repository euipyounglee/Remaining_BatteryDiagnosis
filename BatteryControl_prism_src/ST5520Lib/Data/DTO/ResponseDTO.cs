using ST5520Lib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST5520Lib.Data.DTO
{
	public class ResponseDTO
	{
		public QueryCommand Query { get; private set; }

		public string Data { get; private set; }

		public ResponseDTO(QueryCommand cmd, string data)
		{
			Query = cmd;
			Data = data;
		}
	}
}
