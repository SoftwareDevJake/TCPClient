using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strings
{
    // 언어
    public const string KOREAN = "987653"; // 한국어
    public const string ENGLISH = "987650"; // 영어
    public const string CHINESE = "987652"; // 중국어
    public const string JAPANESE = "987651"; // 일본어
    // 콘텐츠 언어별 시작
    public const string KOREAN_START = "356789"; // 한국어 버전 시작
    public const string ENGLISH_START = "056789"; // 영어 버전 시작
    public const string CHINESE_START = "256789"; // 중국어 버전 시작
    public const string JAPANESE_START = "156789"; // 일본어 버전 시작
    // 스테이지 언어별 시작
    public const string KOREAN_STAGE = "k";
    public const string ENGLISH_STAGE = "e";
    public const string CHINESE_STAGE = "c";
    public const string JAPANESE_STAGE = "j";
    // 콘텐츠 재시작
    public const string RESTART_FULL = "356788"; // 콘텐츠 전체 재시작
    public const string POWEROFF_RESTART_TOTAL = "356787"; // 강제종료 되었을 때
    public const string RESTART_STAGE = "356786"; // 스테이지 전체 재시작
    // 시작 재시작
    public const int START = 1; // 시작
    public const int RESTART = 0; // 재시작
    // 스테이지 선택 + Modes.cs
    public const string TRAIN = "T/"; // 스테이지 훈련소
    public const string HANSANDO = "H/"; // 스테이지 한산도 대첩
    public const string JINJU = "J/"; // 스테이지 진주 대첩
    // 스테이지 종류별 고유 숫자
    public const int MODE_TRAIN = 1;
    public const int MODE_HANSANDO = 2;
    public const int MODE_JINJU = 3;
    // ON, OFF
    public const int OFF = 0; // OFF
    public const int ON = 1; // ON
    // PC Power Control Index
    public const int TOTAL_PC = 0;
    public const int THREESIDE_PC = 1;
    public const int BOTTOM_PC = 2;
    // Projector Power Control Index
    public const int PROJECTOR_TOTAL = 0;
    public const int PROJECTOR_1 = 1;
    public const int PROJECTOR_2 = 2;
    public const int PROJECTOR_3 = 3;
    public const int PROJECTOR_4 = 4;
    public const int PROJECTOR_5 = 5;
    public const int PROJECTOR_6 = 6;
    public const int PROJECTOR_7 = 7;
    // 강제종료 명령어
    public const string SHUTDOWN_BOTTOM = "shutdownBottom";
    public const string SHUTDOWN_THREESIDE = "shutdownThreeSide";
    // 각 PC의 MAC ADDRESSES
    public const string BOTTOM_MAC_ADDRESS = "00-E0-53-30-00-8A";
    public const string THREESIDE_MAC_ADDRESS = "FC-34-97-A3-9E-EC";
    // 각 PC의 IP ADDRESSES
    public const string BOTTOM_IP_ADDRESS = "192.168.0.11";
    public const string THREESIDE_IP_ADDRESS = "192.168.0.10";
    // 프로젝터 전원 조절 명령어
    public const string PROJECTOR_POWER_ON = "power = 1 \r";
    public const string PROJECTOR_POWER_OFF = "power = 0 \r";
    // 각 시작, 재시작 버튼들의 고유 숫자
    public const int CONTENTS = 0;
    public const int STAGE = 1;
    // 동영상 스킵 명령
    public const string VIDEO_SKIP = "skip";
    // 벽면PC 프로그램 재시작 명령
    public const string PROGRAM_RESTART = "restart";
}
