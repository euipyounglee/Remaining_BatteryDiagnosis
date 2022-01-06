using PneCtsLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Data.DTO
{
	public class CTS_MD_SYSTEM_DATA_DTO
	{
		#region property

		// 모듈 ID (Module에서는 GroupID, Host에서는 ModuleID)
		public uint ModuleID { get; private set; }

		public uint SystemType { get; private set; }

		// 충방전 통신 프로토콜 버전 (Protocol Version)
		public uint ProtocolVersion { get; private set; }

		// Module Model Name
		public sbyte[] ModelName { get; private set; }

		// System Version
		public uint OSVersion { get; private set; }

		public ushort VoltageRange { get; private set; }

		public ushort CurrentRange { get; private set; }

		public uint[] VoltageSpec { get; private set; }

		public uint[] CurrentSpec { get; private set; }

		public byte CanCommType { get; private set; }

		public byte[] TypeData { get; private set; }

		public ushort InstalledBoard { get; private set; }

		public ushort ChannelPerBoard { get; private set; }

		public uint InstalledChCount { get; private set; }

		public uint TotalJigNo { get; private set; }

		public uint[] BdInJig { get; private set; }

		public uint[] Reserved { get; private set; }

		#endregion

		#region method
		
		public CTS_MD_SYSTEM_DATA_DTO(CTS_MD_SYSTEM_DATA data)
		{
			ModuleID = data.nModuleID;
			SystemType = data.nSystemType;
			ProtocolVersion = data.nProtocolVersion;

			ModelName = new sbyte[data.szModelName.Length];
			Array.Copy(data.szModelName, 0, ModelName, 0, data.szModelName.Length);

			OSVersion = data.nOSVersion;
			VoltageRange = data.wVoltageRange;
			CurrentRange = data.wCurrentRange;

			VoltageSpec = new uint[data.nVoltageSpec.Length];
			Array.Copy(data.nVoltageSpec, 0, VoltageSpec, 0, data.nVoltageSpec.Length);

			CurrentSpec = new uint[data.nCurrentSpec.Length];
			Array.Copy(data.nCurrentSpec, 0, CurrentSpec, 0, data.nCurrentSpec.Length);

			CanCommType = data.byCanCommType;

			TypeData = new byte[data.byTypeData.Length];
			Array.Copy(data.byTypeData, 0, TypeData, 0, data.byTypeData.Length);

			InstalledBoard = data.wInstalledBoard;
			ChannelPerBoard = data.wChannelPerBoard;
			InstalledChCount = data.nInstalledChCount;
			TotalJigNo = data.nTotalJigNo;

			BdInJig = new uint[data.awBdInJig.Length];
			Array.Copy(data.awBdInJig, 0, BdInJig, 0, data.awBdInJig.Length);

			Reserved = new uint[data.reserved.Length];
			Array.Copy(data.reserved, 0, Reserved, 0, data.reserved.Length);
		}

		#endregion
	}
}
