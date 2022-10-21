using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentsPlayerStart : MonoBehaviour
{
    public static ContentsPlayerStart instance;
    // ���� �̹��� ����
    public Sprite start_off;
    public Sprite start_on;
    public Image[] currentStartImg;
    public int myself;
    // ���� ��ư --> ���������� ���� ��Ű�� ���� �迭�� ����� Ǯ ���� �������� ���� �ε��� ���� ����
    public Button[] startBtn;
    // ���� ��ư�� on / off --> ���������� ���� ��Ű�� ���� �迭�� ����� Ǯ ���� �������� ���� �ε��� ���� ����
    public bool contentsStarted = false;
    // ������� on / off
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
    /// ������ ���� ��ư�� ������ �� ����Ǵ� �Լ�
    /// ��ü ���������� �������� ���������� �����Ͽ� ��ư ����
    /// </summary>
    public void OnClicked(int OnOff, int index)
    {
        // ������ ����
        if (OnOff == Strings.OFF)
        {
            currentStartImg[index].sprite = start_off;
            OnOff = Strings.ON;
        }
        // ������ ��ü ����
        else if (OnOff == Strings.ON && LanguageBtn.languageIsSeleted == true && !stageRestart[myself])
        {
            currentStartImg[index].sprite = start_on;
            OnOff = Strings.OFF;
            startBtn[index].interactable = false;
        }
        // ���������� ����
        else if (OnOff == Strings.ON && stageRestart[myself] && LanguageBtn.languageIsSeleted == true)
        {
            currentStartImg[index].sprite = start_on;
            OnOff = Strings.OFF;
            startBtn[Strings.STAGE].interactable = false;
            // T : �Ʒ���, H : �ѻ굵, J : ���� (ex: T/H = �Ʒ��� + �ѻ굵)
            Modes.mode = string.Empty;
        }
    }
    /// <summary>
    /// ��� ����
    /// ��� Ŭ�������� ������ �� �������� ������ ����
    /// </summary>
    public void ChooseCorrectLanVer()
    {
        if(LanguageBtn.languageIsSeleted) // �� ���� �Ǿ�߸� Ŭ�� ����
        {
            if (LanguageBtn.languageInfo == Strings.KOREAN) // �ѱ���
            {
                cs.SendContentStart(Strings.KOREAN_START);
            }
            else if (LanguageBtn.languageInfo == Strings.ENGLISH) // ����
            {
                cs.SendContentStart(Strings.ENGLISH_START);
            }
            else if (LanguageBtn.languageInfo == Strings.CHINESE) // �߱���
            {
                cs.SendContentStart(Strings.CHINESE_START);
            }
            else if (LanguageBtn.languageInfo == Strings.JAPANESE) // �Ϻ���
            {
                cs.SendContentStart(Strings.JAPANESE_START);
            }
        }
    }
    /// <summary>
    /// �������� ���� ��ư�� ������ �� ����Ǵ� �Լ�
    /// ��ư�� ON�� ��带 ã�� ������ �ش� ������ ����
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

        if (LanguageBtn.languageInfo == Strings.KOREAN) // �ѱ���
        {
            cs.SendContentStart(FinalStageSelected + Strings.KOREAN_STAGE);
        }
        else if (LanguageBtn.languageInfo == Strings.ENGLISH) // ����
        {
            cs.SendContentStart(FinalStageSelected + Strings.ENGLISH_STAGE);
        }
        else if (LanguageBtn.languageInfo == Strings.CHINESE) // �߱���
        {
            cs.SendContentStart(FinalStageSelected + Strings.CHINESE_STAGE);
        }
        else if (LanguageBtn.languageInfo == Strings.JAPANESE) // �Ϻ���
        {
            cs.SendContentStart(FinalStageSelected + Strings.JAPANESE_STAGE);
        }
        FinalStageSelected = string.Empty;
    }
}
