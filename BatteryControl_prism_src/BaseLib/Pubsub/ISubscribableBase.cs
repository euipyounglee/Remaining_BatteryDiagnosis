using BaseLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Pubsub
{
	public interface ISubscribableBase<T>
	{
		bool Connect(T data);

		void Disconnect();

		ConnectionStates ConnectionState { get; set; }
	}
}
