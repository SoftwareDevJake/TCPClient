using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
/// <summary>
/// �������� �����ϴ� Ŭ����
/// </summary>
public class Modes : MonoBehaviour
{
    public static Modes instance;
    public Sprite[] touched;
    public Sprite[] normal;

    public Image[] arrImg;
    public Button[] AmongOne;
    public int[] index = new int[3];

    public int modeSelection;

    public static string mode;

    ClientSend cs = new ClientSend();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }   
    }

    public void SendModesToServer()
    {
        if(modeSelection == Strings.MODE_TRAIN)
        {
            cs.SendMode(Strings.TRAIN);
        }
        else if(modeSelection == Strings.MODE_HANSANDO)
        {
            cs.SendMode(Strings.HANSANDO);
        }
        else if(modeSelection == Strings.MODE_JINJU)
        {
            cs.SendMode(Strings.JINJU);
        }
        
    }
    /// <summary>
    /// �������� ��ư���� ������ �� �̹��� ��ȯ
    /// </summary>
    /// <param name="mode"></param>
    public void SelectMode(int modeIndex)
    {
        if (index[modeIndex] == Strings.OFF)
        {
            arrImg[modeIndex].sprite = touched[modeIndex];
            //cs.ModeSelection(mode);
            index[modeIndex] = Strings.ON;
        }
        else if (index[modeIndex] == Strings.ON)
        {
            arrImg[modeIndex].sprite = normal[modeIndex];
            index[modeIndex] = Strings.OFF;
        }
        // 2�� ������ ���� �� �� �ֵ��� �ϴ� �Լ�
        int limit = 0;
        int notSelected = 0;
        for (int i = 0; i < index.Length; i++)
        {
            if (index[i] == Strings.ON)
            {
                limit++;
            }
            else
            {
                notSelected = i;
            }
        }
        if (limit == 2)
        {
            AmongOne[notSelected].interactable = false;
        }
        else
        {
            for(int i = 0; i < AmongOne.Length; i++)
            {
                AmongOne[i].interactable = true;
            }
        }
    }
}
