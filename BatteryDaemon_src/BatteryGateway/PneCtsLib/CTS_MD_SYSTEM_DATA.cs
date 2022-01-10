using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BatteryGateway.PneCtsLib
{
	//public class CTS_MD_SYSTEM_DATA
 //   {
	//	[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
	//	public UInt32 nModuleID;

	//	[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
	//	public UInt32 nSystemType;

	//	[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
	//	public UInt32 nProtocolVersion;

	//	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
	//	public SByte[] szModelName;

	//	[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
	//	public UInt32 nOSVersion;

	//	[MarshalAs(UnmanagedType.U2, SizeConst = 1)]
	//	public UInt16 wVoltageRange;

	//	[MarshalAs(UnmanagedType.U2, SizeConst = 1)]
	//	public UInt16 wCurrentRange;

	//	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	//	public UInt32[] nVoltageSpec;

	//	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	//	public UInt32[] nCurrentSpec;

	//	[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
	//	public Byte byCanCommType;

	//	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
	//	public Byte[] byTypeData;

	//	[MarshalAs(UnmanagedType.U2, SizeConst = 1)]
	//	public UInt16 wInstalledBoard;

	//	[MarshalAs(UnmanagedType.U2, SizeConst = 1)]
	//	public UInt16 wChannelPerBoard;

	//	[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
	//	public UInt32 nInstalledChCount;

	//	[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
	//	public UInt32 nTotalJigNo;

	//	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	//	public UInt32[] awBdInJig;

	//	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	//	public UInt32[] reserved;
	//}






    //ksj 20200728 : CTS_MD_SYSTEM_DATA 구조체 선언
    [StructLayout(LayoutKind.Sequential)]
    public struct CTS_MD_SYSTEM_DATA
    {
        [MarshalAs(UnmanagedType.U4, SizeConst = 1)]//길이 4
        public UInt32 nModuleID;	   	     /**< 모듈 ID */

        [MarshalAs(UnmanagedType.U4, SizeConst = 1)]//길이 4
        public UInt32 nSystemType;

        [MarshalAs(UnmanagedType.U4, SizeConst = 1)]//길이 4
        public UInt32 nProtocolVersion;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]//길이128
        public byte[] szModelName;

        [MarshalAs(UnmanagedType.U4, SizeConst = 1)]//길이 4
        public UInt32 nOSVersion;

        [MarshalAs(UnmanagedType.U2, SizeConst = 1)]//길이 2
        public UInt16 wVoltageRange;

        [MarshalAs(UnmanagedType.U2, SizeConst = 1)]//길이 2
        public UInt16 wCurrentRange;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]//길이 4 * 5 = 20
        public UInt32[] nVoltageSpec;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]//길이 4 * 5 = 20
        public UInt32[] nCurrentSpec;

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]//길이 1
        public byte byCanCommType;

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]//길이 1
        public byte byTypeData;

        [MarshalAs(UnmanagedType.U2, SizeConst = 1)]//길이 2
        public UInt16 wInstalledBoard;

        [MarshalAs(UnmanagedType.U2, SizeConst = 1)]//길이 2
        public UInt16 wChannelPerBoard;

        [MarshalAs(UnmanagedType.U4, SizeConst = 1)]//길이 4
        public UInt32 nInstalledChCount;

        [MarshalAs(UnmanagedType.U4, SizeConst = 1)]//길이 4
        public UInt32 nTotalJigNo;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]//길이 4 * 16 = 64
        public UInt32[] awBdinJig;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]//길이 4 * 4 = 16
        public UInt32[] reserved;

    }


}
