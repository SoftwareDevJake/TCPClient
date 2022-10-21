using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentsPlayerStart : MonoBehaviour
{
    public static ContentsPlayerStart instance;
    // 시작 이미지 관련
    public Sprite start_off;
    public Sprite start_on;
    public Image[] currentStartImg;
    public int myself;
    // 시작 버튼 --> 스테이지와 연결 시키기 위해 배열로 만들고 풀 버전 스테이지 버전 인덱스 별로 구별
    public Button[] startBtn;
    // 시작 버튼의 on / off --> 스테이지와 연결 시키기 위해 배열로 만들고 풀 버전 스테이지 버전 인덱스 별로 구별
    public bool contentsStarted = false;
    // 재시작의 on / off
    ContentsPlayerRestart cpr;
    public GameObject restart;
    public bool[] stageRestart;

    public string FinalStageSelected;

    ClientSend cs = new ClientSend();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        currentStartImg[myself] = startBtn[myself].GetComponent<Image>();
        cpr = restart.GetComponent<ContentsPlayerRestart>();
    }

    /// <summary>
    /// 콘텐츠 시작 버튼이 눌렸을 때 실행되는 함수
    /// 전체 콘텐츠인지 스테이지 콘텐츠인지 구별하여 버튼 조절
    /// </summary>
    public void OnClicked(int OnOff, int index)
    {
        // 콘텐츠 멈춤
        if (OnOff == Strings.OFF)
        {
            currentStartImg[index].sprite = start_off;
            OnOff = Strings.ON;
        }
        // 콘텐츠 전체 시작
        else if (OnOff == Strings.ON && LanguageBtn.languageIsSeleted == true && !stageRestart[myself])
        {
            currentStartImg[index].sprite = start_on;
            OnOff = Strings.OFF;
            startBtn[index].interactable = false;
        }
        // 스테이지의 시작
        else if (OnOff == Strings.ON && stageRestart[myself] && LanguageBtn.languageIsSeleted == true)
        {
            currentStartImg[index].sprite = start_on;
            OnOff = Strings.OFF;
            startBtn[Strings.STAGE].interactable = false;
            // T : 훈련장, H : 한산도, J : 진주 (ex: T/H = 훈련장 + 한산도)
            Modes.mode = string.Empty;
        }
    }
    /// <summary>
    /// 언어 선택
    /// 언어 클래쓰에서 지정된 언어를 가져오고 서버로 전달
    /// </summary>
    public void ChooseCorrectLanVer()
    {
        if(LanguageBtn.languageIsSeleted) // 언어가 지정 되어야만 클릭 가능
        {
            if (LanguageBtn.languageInfo == Strings.KOREAN) // 한국어
            {
                cs.SendContentStart(Strings.KOREAN_START);
            }
            else if (LanguageBtn.languageInfo == Strings.ENGLISH) // 영어
            {
                cs.SendContentStart(Strings.ENGLISH_START);
            }
            else if (LanguageBtn.languageInfo == Strings.CHINESE) // 중국어
            {
                cs.SendContentStart(Strings.CHINESE_START);
            }
            else if (LanguageBtn.languageInfo == Strings.JAPANESE) // 일본어
            {
                cs.SendContentStart(Strings.JAPANESE_START);
            }
        }
    }
    /// <summary>
    /// 스테이지 시작 버튼이 눌렸을 때 실행되는 함수
    /// 버튼이 ON인 모드를 찾아 서버에 해당 정보를 전달
    /// </summary>
    public void ChooseCorrectContVer()
    {
        string SelectedStages;
        for(int i = 0; i < Modes.instance.arrImg.Length; i++)
        {
            if(Modes.instance.index[i] == Strings.ON)
            {
                if(i == 0)
                {
                    SelectedStages = Strings.TRAIN;
                }
                else if (i == 1)
                {
                    SelectedStages = Strings.HANSANDO;
                }
                else
                {
                    SelectedStages = Strings.JINJU;
                }
                FinalStageSelected += SelectedStages;
            }
        }

        if (LanguageBtn.languageInfo == Strings.KOREAN) // 한국어
        {
            cs.SendContentStart(FinalStageSelected + Strings.KOREAN_STAGE);
        }
        else if (LanguageBtn.languageInfo == Strings.ENGLISH) // 영어
        {
            cs.SendContentStart(FinalStageSelected + Strings.ENGLISH_STAGE);
        }
        else if (LanguageBtn.languageInfo == Strings.CHINESE) // 중국어
        {
            cs.SendContentStart(FinalStageSelected + Strings.CHINESE_STAGE);
        }
        else if (LanguageBtn.languageInfo == Strings.JAPANESE) // 일본어
        {
            cs.SendContentStart(FinalStageSelected + Strings.JAPANESE_STAGE);
        }
        FinalStageSelected = string.Empty;
    }
}
