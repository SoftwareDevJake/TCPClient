using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ��� Ŭ����
/// </summary>
public class LanguageBtn : MonoBehaviour
{
    public static LanguageBtn instance;
    public Button[] languages;

    public Sprite[] img_off;
    public Sprite[] img_on;

    public Image[] currentLanImg;
    public int currentBtnIndex;

    // �� ����� ����ġ. 1�̸� on, 0�̸� off
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
        cs.SendLanguageInfo(languageInfo); // ���� �� ��� ������ ������ ����
    }
    /// <summary>
    /// ��� ��ư�� �������� �� ����Ǵ� �Լ�
    /// </summary>
    public void onClicked(int index)
    {
        // ������ ��ư�� �������� ���
        if (eachLanguageNumbers[index] == Strings.OFF)
        {
            currentLanImg[index].sprite = img_on[index]; // ������ ��ư�� On �̹��� ��ü
            eachLanguageNumbers[index] = Strings.ON; // ������ ��ư On
            OnlyOneBtnIsAvailable(index);
            languageIsSeleted = true;
        }
        // ��� ��ư�� �̹� ���� ���� �� ��
        else if (eachLanguageNumbers[index] == Strings.ON)
        {
            currentLanImg[index].sprite = img_off[index];
            eachLanguageNumbers[index] = Strings.OFF;
            languageIsSeleted = false;
        }


    }
    /// <summary>
    /// �������� ������ ��ư �ϳ��� On, �������� ���� Off
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
