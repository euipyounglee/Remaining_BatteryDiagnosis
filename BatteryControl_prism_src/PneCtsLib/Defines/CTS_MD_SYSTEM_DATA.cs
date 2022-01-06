using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Defines
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CTS_MD_SYSTEM_DATA
	{
		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nModuleID;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nSystemType;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nProtocolVersion;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public SByte[] szModelName;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nOSVersion;

		[MarshalAs(UnmanagedType.U2, SizeConst = 1)]
		public UInt16 wVoltageRange;

		[MarshalAs(UnmanagedType.U2, SizeConst = 1)]
		public UInt16 wCurrentRange;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
		public UInt32[] nVoltageSpec;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
		public UInt32[] nCurrentSpec;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte byCanCommType;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
		public Byte[] byTypeData;

		[MarshalAs(UnmanagedType.U2, SizeConst = 1)]
		public UInt16 wInstalledBoard;

		[MarshalAs(UnmanagedType.U2, SizeConst = 1)]
		public UInt16 wChannelPerBoard;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nInstalledChCount;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nTotalJigNo;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public UInt32[] awBdInJig;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public UInt32[] reserved;
	}
}
