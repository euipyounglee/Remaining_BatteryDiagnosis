using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Pubsub
{
	public class PushDataDTO : APushDataBase
	{
		public enum PushDataTypes
		{
			Log,
			Open,
			Close,
			Data,
            DataBI_V,
            DataBI_T,
            Hex
		}

		#region property

		public PushDataTypes PushDataType { get; private set; }

		public DateTime LogDt { get; private set; }

		public LogLevels LogLevel { get; private set; }

		public LogDev LogDev { get; private set; }

		public object Data { get; private set; }

        public int ReqTaskSeq { get; private set; }

        #endregion

        #region method

        public PushDataDTO(PushDataTypes pushDataType, LogDev logDev, object data, int reqTaskSeq, LogLevels logLevel = LogLevels.I)
		{
			PushDataType = pushDataType;
			LogDt = DateTime.Now;
			LogLevel = logLevel;
			LogDev = logDev;
			Data = data;

            ReqTaskSeq = reqTaskSeq;
        }

		#endregion
	}
}
