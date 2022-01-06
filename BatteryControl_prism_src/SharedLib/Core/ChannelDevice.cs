using I7565H1Lib.Core;
using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Core
{
	public class ChannelDevice
	{
		#region property

		public int ChannelIndex { get; set; }

		public MultimeterLib.Core.M34461A MultimeterInstance { get; set; }

		public ST5520Lib.Core.Core ST5520Instance { get; set; }

		public ACIRLib.Core.Core ACIRInstance { get; set; }

		public PneCtsLib.Core.Core PNECTSInstance { get; set; }

		public RelayBoxLib.Core.Core RelayBoxInstance { get; set; }

		public BIBoxLib.Core.Core BIBoxInstance { get; set; }

		public ZebraScannerLib.Core.Core BarcodeScannerInstance { get; set; }

		#endregion

		#region constructor

		public ChannelDevice(Type sdk, int channelNo)
		{
			ChannelIndex = channelNo;

			MultimeterInstance = new MultimeterLib.Core.M34461A(channelNo);
			ST5520Instance = new ST5520Lib.Core.Core(LogDev.ST5520, channelNo);
			CreateACIRInstance();
			PNECTSInstance = PneCtsLib.Core.Core.Instance;
			RelayBoxInstance = RelayBoxLib.Core.Core.Instance;
			BIBoxInstance = new BIBoxLib.Core.Core(sdk, channelNo, "PACK".Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) ? 18 : 2);
			BarcodeScannerInstance = new ZebraScannerLib.Core.Core(channelNo);
		}

		#endregion

		#region method

		public void CreateACIRInstance()
		{
			ACIRInstance = new ACIRLib.Core.Core(ChannelIndex);
		}

		#endregion
	}
}
