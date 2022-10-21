using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public static ButtonHandler instance;

    public Sprite touched;
    public Sprite normal;
    public Image[] thisImg;

    public int[] ButtonNum = new int[4];

    public int players = 0; // ��� ° �÷��̾����� Ȯ���ϱ� ���� ������ �ѹ���

    ClientSend cs = new ClientSend();

    public Image[] trackerIdExist;

    public Sprite trackerIdExist_ON;
    public Sprite trackerIdExist_OFF;
    
    /// <summary>
    /// �÷��̾� ��Ʈ�ѷ� ��ư ����
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }
    /// <summary>
    /// �ε��� �ʱ�ȭ
    /// </summary>
    private void Start()
    {
        
        for (int i = 0; i < ButtonNum.Length; i++)
        {
            ButtonNum[i] = 0;
        }
    }

    public void OnButtonClicked(int userIndex)
    {
        // � ��ư�� �������� ����
        UIManager.instance.index = userIndex;
        // ��ư ��
        if (ButtonNum[userIndex] == Strings.ON && ButtonNum[userIndex] == 0)
        {
            Debug.Log("OFF");
            thisImg[userIndex].sprite = normal;
            ButtonNum[userIndex] = Strings.OFF;
        }
        // ��ư ��
        else if(ButtonNum[userIndex] == Strings.OFF)
        {
            Debug.Log("ON");
            thisImg[userIndex].sprite = touched;
            ButtonNum[userIndex] = Strings.ON;
            // �� �÷��̾ ���� ��ų Tracker�� ID���� �ִ��� Ȯ��
            if (userIndex == 0)
            {
                CheckIfIdExists(userIndex);
            }
            else if (userIndex == 1)
            {
                CheckIfIdExists(userIndex);
            }
            else if (userIndex == 2)
            {
                CheckIfIdExists(userIndex);
            }
            else if (userIndex == 3)
            {
                CheckIfIdExists(userIndex);
            }
        }
        
    }
    /// <summary>
    /// ���� ��ų Ʈ��Ŀ�� ���̵� �����ϴ��� Ȯ���ϰ� �������� ������ ���� �Ұ�, �����ϸ� ���� ����
    /// </summary>
    public void CheckIfIdExists(int userIndex)
    {
        if (ClientHandle._splitedTrackerId[userIndex] == null || ClientHandle._splitedTrackerId[userIndex] == "")
        {
            Debug.Log("Tracker Id is empty");
            thisImg[userIndex].sprite = normal;
            ButtonNum[userIndex] = 0;
        }
        else
        {
            ButtonNum[userIndex] = 1;
        }
    }
    /// <summary>
    /// �÷��̾ ���� ��
    /// </summary>
    public void InitializePlayerIsOn(int i)
    {
        ButtonNum[i] = 0;
    }

    public void showItself()
    {
        cs.SendNames(players + "/" + ClientHandle._splitedTrackerId[players - 1] + "/" + UIManager.instance.info[players - 1].text + "          ");
    }
}
