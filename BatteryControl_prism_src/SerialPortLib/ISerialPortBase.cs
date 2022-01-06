using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortLib
{
	public interface ISerialPortBase
	{
		void OnSerialDataReceivedEvent(List<byte> stream);
	}
}
