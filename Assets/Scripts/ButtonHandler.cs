using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public static ButtonHandler instance;

    public Sprite touched;
    public Sprite normal;
    public Image[] thisImg;

    public int[] ButtonNum = new int[4];

    public int players = 0; // 몇번 째 플레이어인지 확인하기 위한 각각의 넘버들

    ClientSend cs = new ClientSend();

    public Image[] trackerIdExist;

    public Sprite trackerIdExist_ON;
    public Sprite trackerIdExist_OFF;
    
    /// <summary>
    /// 플레이어 컨트롤러 버튼 조절
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }
    /// <summary>
    /// 인덱스 초기화
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
        // 어떤 버튼을 조절할지 정함
        UIManager.instance.index = userIndex;
        // 버튼 끔
        if (ButtonNum[userIndex] == Strings.ON && ButtonNum[userIndex] == 0)
        {
            Debug.Log("OFF");
            thisImg[userIndex].sprite = normal;
            ButtonNum[userIndex] = Strings.OFF;
        }
        // 버튼 켬
        else if(ButtonNum[userIndex] == Strings.OFF)
        {
            Debug.Log("ON");
            thisImg[userIndex].sprite = touched;
            ButtonNum[userIndex] = Strings.ON;
            // 각 플레이어를 연결 시킬 Tracker의 ID값이 있는지 확인
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
    /// 연결 시킬 트래커의 아이디가 존재하는지 확인하고 존재하지 않으면 실행 불가, 존재하면 정보 전달
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
    /// 플레이어가 꺼질 때
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
