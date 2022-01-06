using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Defines
{
	public class PSServerAPI
	{
		#region api

		private const string DLLNAME = "PSServerAPI.dll";

		// 디버깅용 Test API 정의 ////////////////////////////////////////////////////////////////////////////////
		[DllImport(DLLNAME)] //PSServerAPI.DLL 링크
		public static extern IntPtr ctsApiDebugPtrStruct(ref DEBUG_TEST debug1);

		[DllImport(DLLNAME)] //PSServerAPI.DLL 링크
		public static extern DEBUG_TEST ctsApiDebugStruct(DEBUG_TEST stEmg); //API 함수 정의

		[DllImport(DLLNAME)] //PSServerAPI.DLL 링크
		public static extern int ctsApiDebugInteger(int nA); //API 함수 정의

		[DllImport(DLLNAME)] //PSServerAPI.DLL 링크
		public static extern StringBuilder ctsApiDebugString(char[] szString);

		[DllImport(DLLNAME)] //PSServerAPI.DLL 링크
		public static extern IntPtr ctsApiDebugPtrStruct2(ref CTS_EMG_DATA debug1);

		// ctsAPI 정의 ////////////////////////////////////////////////////////////////////////////////
		[DllImport(DLLNAME)] //PSServerAPI.DLL 링크
		public static extern int ctsServerCreate(int nInstalledModuleNo, IntPtr hWnd);

		[DllImport(DLLNAME)] //PSServerAPI.DLL 링크
		public static extern int ctsServerStart();

		[DllImport(DLLNAME)] //PSServerAPI.DLL 링크
		public static extern int ctsServerClose();

		[DllImport(DLLNAME)]
		public static extern void ctsSetLogPath(char[] szLogPath);

		[DllImport(DLLNAME)]
		public static extern int ctsGetStackedDataSize(int nModuleID, int nChIndex);

		[DllImport(DLLNAME)]
		public static extern int ctsPopSaveData(int nModuleID, int nChIndex, ref CTS_VARIABLE_CH_DATA pSaveChData);

		[DllImport(DLLNAME)]
		public static extern int ctsSendSchedule(uint nModuleID, int nBitLCh, int nBitHCh = 0, char[] szSchPath = null, int nCANCheckOption = 0, int nChamberOperation = 0);

		[DllImport(DLLNAME)]
		public static extern int ctsGetInstalledModuleNum();

		[DllImport(DLLNAME)]
		public static extern StringBuilder ctsGetIPAddress(int nModuleID);

		[DllImport(DLLNAME)]
		public static extern int ctsSendPause(uint nModuleID, int nBitLCh, int nBitHCh = 0, int nCycleNo = 0, int nStepNo = 0);

		[DllImport(DLLNAME)]
		public static extern int ctsSendPauseCancel(uint nModuleID, int nBitLCh, int nBitHCh = 0);

		[DllImport(DLLNAME)]
		public static extern int ctsSendStop(uint nModuleID, int nBitLCh, int nBitHCh = 0, int nCycleNo = 0, int nStepNo = 0);

		[DllImport(DLLNAME)]
		public static extern int ctsSendStopCancel(uint nModuleID, int nBitLCh, int nBitHCh = 0);

		[DllImport(DLLNAME)]
		public static extern int ctsSendNextStep(uint nModuleID, int nBitLCh, int nBitHCh = 0);

		[DllImport(DLLNAME)]
		public static extern int ctsSendContinue(uint nModuleID, int nBitLCh, int nBitHCh = 0, int nContinueOption = 0);

		[DllImport(DLLNAME)]
		public static extern int ctsSendIso(uint nModuleID, int nChannelIndex, byte bISO, byte bDivCh = 1);


		#endregion

		#region callback

		public delegate void dCallbackConnected(int nModuleID, ref CTS_MD_SYSTEM_DATA sysInfo);

		[DllImport(DLLNAME)]
		public static extern void CallbackConnected(dCallbackConnected handler);

		public delegate void dCallbackClosed(int nModuleID);

		[DllImport(DLLNAME)]
		public static extern void CallbackClosed(dCallbackClosed handler);

		public delegate void dCallbackChData(UInt32 nModIDandChIdex, ref CTS_VARIABLE_CH_DATA ChData);
		
		[DllImport(DLLNAME)]
		public static extern void CallbackChData(dCallbackChData handler);

		public delegate void dCallbackChData_D(UInt32 nModIDandChIdex, ref CTS_VARIABLE_CH_DATA_D ChData);

		[DllImport(DLLNAME)]
		public static extern void CallbackChData_D(dCallbackChData_D handler);

		public delegate void dCallbackChData_F(UInt32 nModIDandChIdex, ref CTS_VARIABLE_CH_DATA_F ChData);

		[DllImport(DLLNAME)]
		public static extern void CallbackChData_F(dCallbackChData_F handler);

		public delegate void dCallbackTestComplete(int nModuleID, int nChIndex);

		[DllImport(DLLNAME)]
		public static extern void CallbackTestComplete(dCallbackTestComplete handler);

		public delegate void dCallbackStackNotify(int nModuleID, int nChIndex);

		[DllImport(DLLNAME)]
		public static extern void CallbackStackNotify(dCallbackStackNotify handler);

		public delegate void dCallbackStepEndDataReceive(int nModIDandChIdex, int nStepNum);

		[DllImport(DLLNAME)]
		public static extern void CallbackStepEndDataReceive(dCallbackStepEndDataReceive handler);

		public delegate void dCallbackEmergency(int nModuleID, ref CTS_EMG_DATA emgData);

		[DllImport(DLLNAME)]
		public static extern void CallbackEmergency(dCallbackEmergency handler);

		public delegate void dCallbackPause(int nModuleID, int nChIndex);

		[DllImport(DLLNAME)]
		public static extern void CallbackPause(dCallbackPause handler);

		#endregion
		
	}
}
