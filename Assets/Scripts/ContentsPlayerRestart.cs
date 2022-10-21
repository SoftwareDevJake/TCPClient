using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentsPlayerRestart : MonoBehaviour
{
    public static ContentsPlayerRestart instance;
    // ����� �̹��� ���� ����
    public Sprite reStart_off;
    public Sprite reStart_on;
    public Image[] currentReStartImg;
    // ��Ʈ�ѷ� �̹��� ���� ����
    public Sprite controller_normal;
    public Sprite controller_touched;
    public Image[] signalImg;
    
    public Button[] restartBtn; // ����� ��ư

    // start�� on / off
    ContentsPlayerStart cps;
    public GameObject[] start;

    // ����� �÷��̾ ü���ϰ� �ִ���
    public Button[] playersAreOn;

    // � ���������� ���� ���̾����� + �������� ����� ����
    private Button[] EachStage;

    ClientSend cs = new ClientSend();

    public int myselfNum; // 0 : ������ ����, 1 : �������� ����

    // Modes ��ũ��Ʈ �Լ��� ����ϱ� ���� ���� ����
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
    /// ���ڱ� ���� Ŭ���̾�Ʈ�� �������� ������ ���� �� ��
    /// ����� ��ư�� ���� �ʱ�ȭ ��Ű�� �Լ�
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
    /// ������ ����� ��ư ������ �� �θ��� �Լ�
    /// � ��������� �����Ͽ� ����
    /// </summary>
    public void OnClicked(int OnOff, int index)
    {
        // ����� off
        if (OnOff == 1)
        {
            currentReStartImg[index].sprite = reStart_off;
        }
        // ����� on
        else if (OnOff == 0)
        {
            currentReStartImg[index].sprite = reStart_on;
            
            // ���������� �����
            if (index == Strings.STAGE)
            {
                cps.startBtn[Strings.STAGE].interactable = true; // ��Ȱ��ȭ ��Ų ���� ��ư Ȱ��ȭ ��Ű��
                RestartTheContents();
            }
            // ��ü ������ �����
            else
            {
                cps.startBtn[cps.myself].interactable = true; // ��Ȱ��ȭ ��Ų ���� ��ư Ȱ��ȭ ��Ű��
                RestartTheContents();
            }
        }
    }
    /// <summary>
    /// ��ü ������ ��ư Ŭ���� ����Ǵ� �Լ�
    /// ��ü ������ ����� ��ɾ ������ ����
    /// </summary>
    public void SendInfoToServer()
    {
        cs.SendContentStart(Strings.RESTART_FULL);
    }
    /// <summary>
    /// �������� ������ ��ư Ŭ���� ����Ǵ� �Լ�
    /// �������� ����� ��ɾ ������ ����
    /// </summary>
    public void SendInfoToServerForStage()
    {
        cs.SendContentStart(Strings.RESTART_STAGE);
    }
    /// <summary>
    /// ������ ����� �Լ�
    /// </summary>
    public void RestartTheContents()
    {
        ClientHandle.i = 0; // ��Ʈ�ѷ��� ã�� �� ����� �ε��� ���� �ʱ�ȭ
        // ��Ʈ�ѷ� ���̵� ���� �ʱ�ȭ
        for (int i = 0; i < ClientHandle._splitedTrackerId.Length; i++)
        {
            ClientHandle._splitedTrackerId[i] = null;
            ButtonHandler.instance.trackerIdExist[i].sprite = ButtonHandler.instance.trackerIdExist_OFF;
        }
        InitializeTheContents(); // ������ �ʱ�ȭ
    }

    /// <summary>
    /// ����� ��ư���� ������ �� ó�� �ٸ� ���� �ʱ�ȭ
    /// for������ ��� ����� �ʱ�ȭ
    /// </summary>
    public void InitializeTheContents()
    {
        // ��Ʈ�ѷ� �̹��� ��Ȱ��ȭ
        for (int i = 0; i < signalImg.Length; i++)
        {
            //signalImg[i].sprite = controller_normal;
            ButtonHandler.instance.thisImg[i].sprite = controller_normal;
            ButtonHandler.instance.ButtonNum[i] = Strings.OFF;
        }
        // �÷��̾�� ���� �ʱ�ȭ
        for (int i = 0; i < playersAreOn.Length; i++)
        {
            UIManager.instance.info[i].text = null;
            playersAreOn[i].GetComponent<ButtonHandler>().InitializePlayerIsOn(i);
        }
        // �������� ���û��׵� ��� �ʱ�ȭ
        for (int i = 0; i < modes.Length; i++)
        {
            Modes.instance.arrImg[i].sprite = Modes.instance.normal[i];
            Modes.instance.index[i] = Strings.OFF;
        }
        // ��� �ʱ�ȭ
        for (int i = 0; i < Modes.instance.AmongOne.Length; i++)
        {
            Modes.instance.AmongOne[i].interactable = true;
        }
    }
}
