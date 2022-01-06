using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiLib.Defines
{
    public enum EVAL_STEP
    {
        LOGIN = 0,                     // 사용자 Login

        SYSTEM = 101,                  // 연결 시스템 선택.  inputValue ex) 192.168.1.57,23;192.168.1.100,3001;
        BATTERY = 102,                 // 배터리 선택.
        BARCODE_SEL = 103,             // 배터리 바코드 입력.
        MSD_DISCON = 104,              // MSD 분리 확인.
        VOLTAGE_01 = 105,              // 종단부 전압 측정.
        //BIBMS_SEL = 106,               // (unused) BI 또는 BMS 선택.
        BIBMS_CONN = 7,                //  BI 또는 BMS 연결.

        //COVER_REMOVE = 110,            // (unused) 배터리 팩 상부 커버 제거.
        //SHNS_CONN = 111,               // (unused) +/- 직결 하네스 연결.
        //PRA_HV_CONN = 112,             // (unused) PRA에 LV 하네스 연결(BMS 연결).
        //PRA_LV_CONN = 113,             // (unused) PRA에 HV 하네스 연결.
        //BI_CONN = 116,                 // (unused) BI 하네스 연결(BI 연결).
        //GROUND_CONN = 119,             // (unused) PRA 접지 단자에 접지선 연결.

        MSD_CONN = 114,                // MSD 연결.
        VOLTAGE_02 = 115,              // 종단부 전압 입력.
        //LV_CONN = 117,                 // (unused) LV Junction Box 연결.
        HV_CONN = 118,                 // HV Junction Box 연결.
        CHECK_CONN = 120,              // 연결점검.  (배터리 연결확인, 절연저항 측정)
        CHECK_READY = 121,             // 준비 점검. (ACIR 측정, 충방전기 측정, BI 측정)
        SCENARIO_SEL = 130,            // 검사방법 선택.
        SCENARIO_END = 131,            // 검사 종료.
        GRADE = 132,                   // 진단 등급 출력.
        RESULT = 133,                  // 시험 결과.
        //BARCODE_PRINT = 134,           // (unused) 바코드 출력.
        //CHECK_END = 140,               // (unused) 정리점검.
        CHECK_DISCON = 141,            // 분리점검.
        MSD_DISCON_2 = 145,            // MSD 분리
        VOLTAGE_03 = 146,              // 종단부 전압 측정.
        //LV_DISCON = 143,               // (unused) LV Junction Box 분리.
        HV_DISCON = 142,               // HV Junction Box 분리.

        //BI_DISCON = 144,               // (unused) 하네스 분리(BI 연결 분리).
        //SHNS_DISCON = 147,             // (unused) +/- 직결 하네스 분리.
        //COVER_RECOVER = 148,           // (unused) 배터리 팩 상부 커버 조립.
        //PRA_LV_DISCON = 149,           // (unused) PRA에서 HV 하네스 분리. 
        //PRA_HV_DISCON = 150,           // (unused) PRA에서 LV 하네스 분리(BMS 분리).

        //BIBMS_DISCON = 27,             // (unused) BI 또는 BMS 분리.
        MSD_DISCON_3 = 28,             // MSD 분리.
        VOLTAGE_04 = 29,               // 종단부 전압 측정.
        //REPORT = 135,                  // (unused) 시험 성적서 출력.

        EXIT = 999,                    // 프로그램 종료.
    }

}
