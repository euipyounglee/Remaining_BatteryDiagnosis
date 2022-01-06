using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Defines
{
	public class Constants
	{
		public const int CTS_MAX_MAPPING_AUX = 512;     /*!< Channel 당 최대 AUX 갯수 (온도(256) + 전압(256)) \n (Maximum number of AUX per channel) */
		public const int CTS_MAX_MAPPING_CAN = 512;     /*!< Channel 당 최대 CAN 갯수 (Master(256) + Slave(256)) \n (Maximum number of CAN per channel)*/

		//PC Part state
		public const byte PS_STATE_LINE_OFF = 0x20;             /*!< 통신 두절 (Line off) */
		public const byte PS_STATE_LINE_ON = 0x21;              /*!< 통신 연결 (Line on) */
		public const byte PS_STATE_END = 0x26;                  /*!< 테스트 종료 (Test end) */
		public const byte PS_STATE_READY = 0x29;                /*!< 준비 (Test ready) */
		public const byte PS_STATE_ERROR = 0xFF;                /*!< Error */

		//Channel State
		public const byte PS_STATE_IDLE = 0x0000;               /*!< 초기상태 (Idle)*/
		public const byte PS_STATE_STANDBY = 0x0001;            /*!< 대기 (Standby) */
		public const byte PS_STATE_RUN = 0x0002;                /*!< 가동중 (Run) */
		public const byte PS_STATE_PAUSE = 0x0003;              /*!< 일시정지 (Pause) */
		public const byte PS_STATE_MAINTENANCE = 0x0004;        /*!< 유지보수 (Maintenance) */

		//Channel step
		public const byte PS_STEP_NONE = 0x00;
		public const byte PS_STEP_CHARGE = 0x01;
		public const byte PS_STEP_DISCHARGE = 0x02;
		public const byte PS_STEP_REST = 0x03;
		public const byte PS_STEP_OCV = 0x04;
		public const byte PS_STEP_IMPEDANCE = 0x05;
		public const byte PS_STEP_END = 0x06;
		public const byte PS_STEP_ADV_CYCLE = 0x07;
		public const byte PS_STEP_LOOP = 0x08;
		public const byte PS_STEP_PATTERN = 0x09;

		#region ResponseCode SendCommand Response Code
		//response of command code
		public const uint CTS_NACK = 0x00000000; /*!< 오류 (Error) */
		public const uint CTS_ACK = 0x00000001;	/*!< 정상 (Normal) */
		public const uint CTS_TIMEOUT = 0x00000002;	/*!< 시간초과 (Time over) */
		public const uint CTS_SIZE_MISMATCH = 0x00000003;	/*!< Body Size 불일치 (Size mismatch) */
		public const uint CTS_RX_BUFF_OVER_FLOW = 0x00000004;	/*!< 수신 버퍼 오버플로우 (Receive buffer overflow) */
		public const uint CTS_TX_BUFF_OVER_FLOW = 0x00000005;	/*!< 송신 버퍼 오버플로우 (Transmit buffer overflow) */
		public const uint CTS_CONNECTED = 0x00000006;	/*!< 접속 성공 (Connection success) */
		public const uint CTS_NOT_CONNECTED = 0x00000007;	/*!< 접속 되어 있지 않음 (Not connected) */
		public const uint CTS_FILE_NOT_EXIT = 0x00000008;	/*!< 해당 경로에 파일이 없음 (Not exist file) */
		public const uint CTS_FILE_OPENED = 0x00000009;	/*!< 파일이 Open되어 있음 (File open fail) */
		public const uint CTS_CAN_LIN_TYPE_INVALID = 0x0000000A;	/*!< CAN Rx/Tx, LIN File Type이 맞지 않음 (Type mismatch) */
		public const uint CTS_CHANNEL_RUN = 0x0000000B;	/*!< 현재 동작중인 채널임 (Channel is running) */
		public const uint CTS_FAIL = 0xFFFFFFFF;	/*!< 실패 (Failure) */
		#endregion SendCommand ResponseCode 정의 끝
	}
}
