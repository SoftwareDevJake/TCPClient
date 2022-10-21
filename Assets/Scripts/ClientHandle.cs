using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using UnityEngine.UI;
/// <summary>
/// 서버를 통해 전달 받은 정보 저장
/// </summary>
public class ClientHandle
{
    public GameObject resultPng;

    public string _info;
    public static string[] _splitedTrackerId = new string[4];
    public static int i = 0;

    public static string totalModeSelected;

    // 컨텐츠 쪽에서 재시작이 되었을 때
    public static bool restartOn = false;

    private void Start()
    {
        resultPng = GameObject.Find("rimgResult");
    }
    /// <summary>
    /// 서버가 연결 될 때
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
    /// 서버에 플레이어들의 이름을 전달하고 확인
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
    /// 게임 클라이언트에서 트래커 아이디를 받아와서 저장
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
    /// 서버에 콘텐츠 시작 신호 보낸 후 확인
    /// </summary>
    /// <param name="_packet"></param>
    public void ContentStart(Packet _packet)
    {
        string _contentStart = _packet.ReadString();
        Debug.Log($"Sent Content Info : {_contentStart}");
        if(_contentStart == Strings.POWEROFF_RESTART_TOTAL) // 게임 클라이언트가 강제로 끊어지는 경우 // START : ON, RESTART : OFF
        {
            restartOn = true;
            ContentsPlayerRestart.instance.OnClicked(Strings.RESTART, Strings.CONTENTS);
            ContentsPlayerStart.instance.OnClicked(Strings.RESTART, Strings.CONTENTS);
        }
        else if (_contentStart == Strings.RESTART_FULL || _contentStart == Strings.RESTART_STAGE) // 재시작
        {
            ContentsPlayerRestart.instance.OnClicked(Strings.RESTART, Strings.CONTENTS);
            ContentsPlayerStart.instance.OnClicked(Strings.RESTART, Strings.CONTENTS);
            ContentsPlayerStart.instance.OnClicked(Strings.RESTART, Strings.STAGE);
            ContentsPlayerRestart.instance.OnClicked(Strings.RESTART, Strings.STAGE);
        }
        // 각 언어 별 전체 컨텐츠 시작
        else if (_contentStart == Strings.KOREAN_START
            || _contentStart == Strings.ENGLISH_START
            || _contentStart == Strings.CHINESE_START 
            || _contentStart == Strings.JAPANESE_START)
        {
            ContentsPlayerStart.instance.OnClicked(Strings.START, Strings.CONTENTS);
            ContentsPlayerRestart.instance.OnClicked(Strings.START, Strings.CONTENTS);
        }
        // 각 언어 별 스테이지 시작
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
    /// 서버에 언어 신호보낸 후 확인
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
    /// 서버에서 받은 관리자에서 필요하지 않은 정보들
    /// </summary>
    /// <param name="_packet"></param>
    public void Ignore(Packet _packet)
    {
        string _ignore = _packet.ReadString();
        Debug.Log($"Ignored Data : {_ignore}");
    }
    /// <summary>
    /// 스테이지 정보 전달 후 확인
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
