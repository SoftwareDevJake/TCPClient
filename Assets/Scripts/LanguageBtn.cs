using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 언어 클래쓰
/// </summary>
public class LanguageBtn : MonoBehaviour
{
    public static LanguageBtn instance;
    public Button[] languages;

    public Sprite[] img_off;
    public Sprite[] img_on;

    public Image[] currentLanImg;
    public int currentBtnIndex;

    // 각 언어의 스위치. 1이면 on, 0이면 off
    public int[] eachLanguageNumbers = new int[4];

    ClientSend cs = new ClientSend();
    public static string languageInfo;

    public static bool languageIsSeleted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        
        for (int i = 0; i < eachLanguageNumbers.Length; i++)
        {
            eachLanguageNumbers[i] = 0;
        }
    }

    public void Choose()
    {
        Debug.Log("currentBtnIndex : " + currentBtnIndex);
        // korean : 987653, english : 987650, chinese : 987652, japanese : 987651
        if (currentBtnIndex == 0)
        {
            // korean
            languageInfo = Strings.KOREAN;
        }
        else if (currentBtnIndex == 1)
        {
            // english
            languageInfo = Strings.ENGLISH;
        }
        else if (currentBtnIndex == 2)
        {
            // chinese
            languageInfo = Strings.CHINESE;
        }
        else if (currentBtnIndex == 3)
        {
            // japanese
            languageInfo = Strings.JAPANESE;
        }
        cs.SendLanguageInfo(languageInfo); // 선택 된 언어 정보를 서버에 전달
    }
    /// <summary>
    /// 언어 버튼이 눌려졌을 때 실행되는 함수
    /// </summary>
    public void onClicked(int index)
    {
        // 눌려진 버튼이 꺼져있을 경우
        if (eachLanguageNumbers[index] == Strings.OFF)
        {
            currentLanImg[index].sprite = img_on[index]; // 눌려진 버튼의 On 이미지 교체
            eachLanguageNumbers[index] = Strings.ON; // 눌려진 버튼 On
            OnlyOneBtnIsAvailable(index);
            languageIsSeleted = true;
        }
        // 언어 버튼이 이미 켜져 있을 때 끔
        else if (eachLanguageNumbers[index] == Strings.ON)
        {
            currentLanImg[index].sprite = img_off[index];
            eachLanguageNumbers[index] = Strings.OFF;
            languageIsSeleted = false;
        }


    }
    /// <summary>
    /// 마지막에 눌려진 버튼 하나만 On, 나머지는 전부 Off
    /// </summary>
    public void OnlyOneBtnIsAvailable(int index)
    {
        for (int i = 0; i < languages.Length; i++)
        {
            if (index != i && eachLanguageNumbers[i] == 1)
            {
                languages[i].GetComponent<Image>().sprite = img_off[i];
                eachLanguageNumbers[i] = 0;

            }
        }
    }
}
