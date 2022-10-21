using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using UnityEngine.UI;
/// <summary>
/// ������ ���� ���� ���� ���� ����
/// </summary>
public class ClientHandle
{
    public GameObject resultPng;

    public string _info;
    public static string[] _splitedTrackerId = new string[4];
    public static int i = 0;

    public static string totalModeSelected;

    // ������ �ʿ��� ������� �Ǿ��� ��
    public static bool restartOn = false;

    private void Start()
    {
        resultPng = GameObject.Find("rimgResult");
    }
    /// <summary>
    /// ������ ���� �� ��
    /// </summary>
    /// <param name="_packet"></param>
    public void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");

        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();
        Client.isConnected = true;
    }
    /// <summary>
    /// ������ �÷��̾���� �̸��� �����ϰ� Ȯ��
    /// </summary>
    /// <param name="_packet"></param>
    public void ReceivedUserInfo(Packet _packet)
    {
        string _name = _packet.ReadString();
        string[] name = _name.Split('/');
        int userIndex = int.Parse(name[0]) - 1;
        Debug.Log("index : " + userIndex);
        if(name[1] == "" || name[1] == null)
        {
            Debug.Log("Empty");
        }
        else
        {
            ButtonHandler.instance.OnButtonClicked(userIndex);
            UIManager.instance.info[userIndex].text = name[name.Length - 1];
            Debug.Log("btn : " + ButtonHandler.instance.players);
        }
        Debug.Log($"Sent user info: {_name}");
    }
    /// <summary>
    /// ���� Ŭ���̾�Ʈ���� Ʈ��Ŀ ���̵� �޾ƿͼ� ����
    /// </summary>
    /// <param name="_packet"></param>
    public void ReceivedTrackerId(Packet _packet)
    {
        string _trackerId = _packet.ReadString();
        _splitedTrackerId[i] = _trackerId;
        Debug.Log($"Received Tracker ID : {_splitedTrackerId[i]}");
        ButtonHandler.instance.trackerIdExist[i].sprite = ButtonHandler.instance.trackerIdExist_ON;
        i++;
    }
    /// <summary>
    /// ������ ������ ���� ��ȣ ���� �� Ȯ��
    /// </summary>
    /// <param name="_packet"></param>
    public void ContentStart(Packet _packet)
    {
        string _contentStart = _packet.ReadString();
        Debug.Log($"Sent Content Info : {_contentStart}");
        if(_contentStart == Strings.POWEROFF_RESTART_TOTAL) // ���� Ŭ���̾�Ʈ�� ������ �������� ��� // START : ON, RESTART : OFF
        {
            restartOn = true;
            ContentsPlayerRestart.instance.OnClicked(Strings.RESTART, Strings.CONTENTS);
            ContentsPlayerStart.instance.OnClicked(Strings.RESTART, Strings.CONTENTS);
        }
        else if (_contentStart == Strings.RESTART_FULL || _contentStart == Strings.RESTART_STAGE) // �����
        {
            ContentsPlayerRestart.instance.OnClicked(Strings.RESTART, Strings.CONTENTS);
            ContentsPlayerStart.instance.OnClicked(Strings.RESTART, Strings.CONTENTS);
            ContentsPlayerStart.instance.OnClicked(Strings.RESTART, Strings.STAGE);
            ContentsPlayerRestart.instance.OnClicked(Strings.RESTART, Strings.STAGE);
        }
        // �� ��� �� ��ü ������ ����
        else if (_contentStart == Strings.KOREAN_START
            || _contentStart == Strings.ENGLISH_START
            || _contentStart == Strings.CHINESE_START 
            || _contentStart == Strings.JAPANESE_START)
        {
            ContentsPlayerStart.instance.OnClicked(Strings.START, Strings.CONTENTS);
            ContentsPlayerRestart.instance.OnClicked(Strings.START, Strings.CONTENTS);
        }
        // �� ��� �� �������� ����
        else if (_contentStart.Contains(Strings.KOREAN_STAGE)
            || _contentStart.Contains(Strings.ENGLISH_STAGE) 
            || _contentStart.Contains(Strings.CHINESE_STAGE) 
            || _contentStart.Contains(Strings.JAPANESE_STAGE))
        {
            ContentsPlayerStart.instance.OnClicked(Strings.START, Strings.STAGE);
            ContentsPlayerRestart.instance.OnClicked(Strings.START, Strings.STAGE);
        }
    }
    /// <summary>
    /// ������ ��� ��ȣ���� �� Ȯ��
    /// </summary>
    /// <param name="_packet"></param>
    public void LanguageInfo(Packet _packet)
    {
        string _language = _packet.ReadString();
        int index = 0;
        Debug.Log($"Sent Language Info : {_language}");
        if (_language == Strings.KOREAN)
        {
            // korean
            index = 0;
        }
        else if (_language == Strings.ENGLISH)
        {
            // english
            index = 1;
        }
        else if (_language == Strings.CHINESE)
        {
            // chinese
            index = 2;
        }
        else if (_language == Strings.JAPANESE)
        {
            // japanese
            index = 3;
        }
        LanguageBtn.instance.onClicked(index);
    }
    /// <summary>
    /// �������� ���� �����ڿ��� �ʿ����� ���� ������
    /// </summary>
    /// <param name="_packet"></param>
    public void Ignore(Packet _packet)
    {
        string _ignore = _packet.ReadString();
        Debug.Log($"Ignored Data : {_ignore}");
    }
    /// <summary>
    /// �������� ���� ���� �� Ȯ��
    /// </summary>
    /// <param name="_packet"></param>
    public void Mode(Packet _packet)
    {
        string _mode = _packet.ReadString();
        int index = 0;

        if(_mode == Strings.TRAIN)
        {
            index = 0;
        }
        else if(_mode == Strings.HANSANDO)
        {
            index = 1;
        }
        else if(_mode == Strings.JINJU)
        {
            index = 2;
        }
        Modes.instance.SelectMode(index);
    }

    public void Shutdown(Packet _packet)
    {
        string _shutdown = _packet.ReadString();
        Debug.Log($"shutdown : {_shutdown}");
    }

    public void VideoSkip(Packet _packet)
    {
        string _skip = _packet.ReadString();
        Debug.Log($"skip : {_skip}");
    }

    public void Restart(Packet _packet)
    {
        string _restart = _packet.ReadString();
        Debug.Log($"restart : { _restart}");
    }
}
