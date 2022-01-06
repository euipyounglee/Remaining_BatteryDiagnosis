using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Defines
{
	public enum ProgressStateTypes
	{
		/// <summary>
		/// 시작
		/// </summary>
		Start,

		/// <summary>
		/// 장비연결확인
		/// </summary>
		CheckConnections,

		/// <summary>
		/// 결과조회
		/// </summary>
		ShowResult,

		/// <summary>
		/// 장비연결상태
		/// </summary>
		DeviceSetting,

		/// <summary>
		/// 배터리선택
		/// </summary>
		SelectBattery,

		/// <summary>
		/// 배터리연결
		/// </summary>
		ConnectBattery,

		/// <summary>
		/// 진단선택
		/// </summary>
		SelectInspection,

		/// <summary>
		/// 간편진단
		/// </summary>
		SimpleInspection,

		/// <summary>
		/// 표준진단
		/// </summary>
		NormalInspection,

		/// <summary>
		/// 정밀진단
		/// </summary>
		CloseInspection,

		/// <summary>
		/// 검사결과
		/// </summary>
		InspectionResult,

		/// <summary>
		/// 배터리분리
		/// </summary>
		DisconnectBatery,

		/// <summary>
		/// 완료
		/// </summary>
		Finish
	}
}
