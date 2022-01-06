using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data
{
	public class StringValue : System.Attribute
	{
		public string Value { get; private set; }

		public StringValue(string value)
		{
			Value = value;
		}
	}
}
