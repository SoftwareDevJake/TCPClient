using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentsPlayerRestart : MonoBehaviour
{
    public static ContentsPlayerRestart instance;
    // 재시작 이미지 관련 변수
    public Sprite reStart_off;
    public Sprite reStart_on;
    public Image[] currentReStartImg;
    // 콘트롤러 이미지 관련 변수
    public Sprite controller_normal;
    public Sprite controller_touched;
    public Image[] signalImg;
    
    public Button[] restartBtn; // 재시작 버튼

    // start의 on / off
    ContentsPlayerStart cps;
    public GameObject[] start;

    // 몇명의 플레이어가 체험하고 있는지
    public Button[] playersAreOn;

    // 어떤 스테이지가 진행 중이었는지 + 스테이지 재시작 인지
    private Button[] EachStage;

    ClientSend cs = new ClientSend();

    public int myselfNum; // 0 : 콘텐츠 시작, 1 : 스테이지 시작

    // Modes 스크립트 함수를 사용하기 위해 만든 변수
    public GameObject[] modes;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        currentReStartImg[myselfNum] = restartBtn[myselfNum].GetComponent<Image>();
        cps = start[myselfNum].GetComponent<ContentsPlayerStart>();
    }

    /// <summary>
    /// 갑자기 게임 클라이언트가 서버에서 연결이 해제 될 때
    /// 재시작 버튼을 눌러 초기화 시키는 함수
    /// </summary>
    private void FixedUpdate()
    {
        if(ClientHandle.restartOn == true)
        {
            OnClicked(0,0);
            ClientHandle.restartOn = false;
        }
    }

    /// <summary>
    /// 콘텐츠 재시작 버튼 눌려질 때 부르는 함수
    /// 어떤 재시작인지 구별하여 실행
    /// </summary>
    public void OnClicked(int OnOff, int index)
    {
        // 재시작 off
        if (OnOff == 1)
        {
            currentReStartImg[index].sprite = reStart_off;
        }
        // 재시작 on
        else if (OnOff == 0)
        {
            currentReStartImg[index].sprite = reStart_on;
            
            // 스테이지의 재시작
            if (index == Strings.STAGE)
            {
                cps.startBtn[Strings.STAGE].interactable = true; // 비활성화 시킨 시작 버튼 활성화 시키기
                RestartTheContents();
            }
            // 전체 콘텐츠 재시작
            else
            {
                cps.startBtn[cps.myself].interactable = true; // 비활성화 시킨 시작 버튼 활성화 시키기
                RestartTheContents();
            }
        }
    }
    /// <summary>
    /// 전체 콘텐츠 버튼 클릭시 실행되는 함수
    /// 전체 콘텐츠 재시작 명령어를 서버로 전달
    /// </summary>
    public void SendInfoToServer()
    {
        cs.SendContentStart(Strings.RESTART_FULL);
    }
    /// <summary>
    /// 스테이지 콘텐츠 버튼 클릭시 실행되는 함수
    /// 스테이지 재시작 명령어를 서버로 전달
    /// </summary>
    public void SendInfoToServerForStage()
    {
        cs.SendContentStart(Strings.RESTART_STAGE);
    }
    /// <summary>
    /// 콘텐츠 재시작 함수
    /// </summary>
    public void RestartTheContents()
    {
        ClientHandle.i = 0; // 콘트롤러를 찾을 때 사용한 인덱스 변수 초기화
        // 콘트롤러 아이디 전부 초기화
        for (int i = 0; i < ClientHandle._splitedTrackerId.Length; i++)
        {
            ClientHandle._splitedTrackerId[i] = null;
            ButtonHandler.instance.trackerIdExist[i].sprite = ButtonHandler.instance.trackerIdExist_OFF;
        }
        InitializeTheContents(); // 콘텐츠 초기화
    }

    /// <summary>
    /// 재시작 버튼으로 시작할 때 처럼 다른 값들 초기화
    /// for문으로 모든 내용들 초기화
    /// </summary>
    public void InitializeTheContents()
    {
        // 컨트롤러 이미지 비활성화
        for (int i = 0; i < signalImg.Length; i++)
        {
            //signalImg[i].sprite = controller_normal;
            ButtonHandler.instance.thisImg[i].sprite = controller_normal;
            ButtonHandler.instance.ButtonNum[i] = Strings.OFF;
        }
        // 플레이어들 정보 초기화
        for (int i = 0; i < playersAreOn.Length; i++)
        {
            UIManager.instance.info[i].text = null;
            playersAreOn[i].GetComponent<ButtonHandler>().InitializePlayerIsOn(i);
        }
        // 스테이지 선택사항들 모두 초기화
        for (int i = 0; i < modes.Length; i++)
        {
            Modes.instance.arrImg[i].sprite = Modes.instance.normal[i];
            Modes.instance.index[i] = Strings.OFF;
        }
        // 모드 초기화
        for (int i = 0; i < Modes.instance.AmongOne.Length; i++)
        {
            Modes.instance.AmongOne[i].interactable = true;
        }
    }
}
