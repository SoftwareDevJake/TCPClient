using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strings
{
    // ���
    public const string KOREAN = "987653"; // �ѱ���
    public const string ENGLISH = "987650"; // ����
    public const string CHINESE = "987652"; // �߱���
    public const string JAPANESE = "987651"; // �Ϻ���
    // ������ �� ����
    public const string KOREAN_START = "356789"; // �ѱ��� ���� ����
    public const string ENGLISH_START = "056789"; // ���� ���� ����
    public const string CHINESE_START = "256789"; // �߱��� ���� ����
    public const string JAPANESE_START = "156789"; // �Ϻ��� ���� ����
    // �������� �� ����
    public const string KOREAN_STAGE = "k";
    public const string ENGLISH_STAGE = "e";
    public const string CHINESE_STAGE = "c";
    public const string JAPANESE_STAGE = "j";
    // ������ �����
    public const string RESTART_FULL = "356788"; // ������ ��ü �����
    public const string POWEROFF_RESTART_TOTAL = "356787"; // �������� �Ǿ��� ��
    public const string RESTART_STAGE = "356786"; // �������� ��ü �����
    // ���� �����
    public const int START = 1; // ����
    public const int RESTART = 0; // �����
    // �������� ���� + Modes.cs
    public const string TRAIN = "T/"; // �������� �Ʒü�
    public const string HANSANDO = "H/"; // �������� �ѻ굵 ��ø
    public const string JINJU = "J/"; // �������� ���� ��ø
    // �������� ������ ���� ����
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
    // �������� ��ɾ�
    public const string SHUTDOWN_BOTTOM = "shutdownBottom";
    public const string SHUTDOWN_THREESIDE = "shutdownThreeSide";
    // �� PC�� MAC ADDRESSES
    public const string BOTTOM_MAC_ADDRESS = "00-E0-53-30-00-8A";
    public const string THREESIDE_MAC_ADDRESS = "FC-34-97-A3-9E-EC";
    // �� PC�� IP ADDRESSES
    public const string BOTTOM_IP_ADDRESS = "192.168.0.11";
    public const string THREESIDE_IP_ADDRESS = "192.168.0.10";
    // �������� ���� ���� ��ɾ�
    public const string PROJECTOR_POWER_ON = "power = 1 \r";
    public const string PROJECTOR_POWER_OFF = "power = 0 \r";
    // �� ����, ����� ��ư���� ���� ����
    public const int CONTENTS = 0;
    public const int STAGE = 1;
    // ������ ��ŵ ���
    public const string VIDEO_SKIP = "skip";
    // ����PC ���α׷� ����� ���
    public const string PROGRAM_RESTART = "restart";
}
