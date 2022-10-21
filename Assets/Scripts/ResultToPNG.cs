using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ResultToPNG : MonoBehaviour
{
    // png ����� �κ�
    public RectTransform rectT; // Assign the UI element which you wanna capture
    public Image img;
    int width; // width of the object to capture
    int height; // height of the object to capture

    public GameObject Panel1;
    public GameObject Panel2;

    // ���� �ٲٴ� �κ�
    // ���� ���ű��� ���� �ؽ�Ʈ��
    public Text txtYear;
    public Text txtMonth;
    public Text txtDay;
    public Text txtPointHansando;
    public Text txtPointJinju;
    public Text txtRank;
    public Text txtName;

    private string userInfo; // ���� ��� ����
    private string[] split_userInfo; // ���� ����� '/' �� �������� ����

    public Text[] four_texts; // ��ũ, �̸�, �ѻ굵 ����, ���� ������ ����
    private int four_j = 0; // �� �� �ε��� �� ����
    private int pngNum = 1; // ���� �� png (player) ����
    private int beginNum = 0; // ������ �������� ���� �ε��� ����

    private string moreSplited; // '\n'�� �ν��ϰ� �� �� �ٽ� ������ ����

    ClientHandle ch = new ClientHandle();

    void Start()
    {
        width = System.Convert.ToInt32(rectT.rect.width);
        height = System.Convert.ToInt32(rectT.rect.height);
    }
    /// <summary>
    /// �������� ������ �Լ� (����� �ش��ϴ� �Էµ��� �ְ� png ��ũ������ ����
    /// </summary>
    public void LastWork()
    {
        for (int i = 0; i < 16; i++)
        {
            ResultDate();
            resultBody(beginNum);
            i += 3;
            beginNum = i;
            
            Capture(pngNum);
            pngNum++;
        }
    }
    /// <summary>
    /// �ٸ� �гε��� �־��� �� Q�� ���� �ǵ��ư��� ���
    /// </summary>
    private void Update()
    {
        // ���ư���
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.SetActive(false);
            Panel1.SetActive(true);
            Panel2.SetActive(true);
        }
    }

    /// <summary>
    /// ��ũ���� + �̹��� ���� (���)
    /// </summary>
    /// <param name="pngNum"></param>
    /// <returns></returns>
    public IEnumerator takeScreenShot(int pngNum)
    {
        yield return new WaitForEndOfFrame();
        Vector2 temp = rectT.transform.position;
        var startX = temp.x - width / 2;
        var startY = temp.y - height / 2;

        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(startX, startY, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        var bytes = tex.EncodeToPNG();
        Destroy(tex);

        File.WriteAllBytes("C:/inetpub/wwwroot/img/" + pngNum + "P.png", bytes); // ���ű��� ����� ����Ǵ� ��ġ (�� ���� �ϰ� ������ ������ ���� �������� �� �� ����)

        string imgsrc = System.Convert.ToBase64String(bytes);
        Texture2D scrnShot = new Texture2D(2048, 1024, TextureFormat.ARGB32, false);
        scrnShot.LoadImage(System.Convert.FromBase64String(imgsrc));

        Sprite sprite = Sprite.Create(scrnShot, new Rect(0, 0, scrnShot.width, scrnShot.height), new Vector2(.5f, .5f));
        img.sprite = sprite;
    }
    /// <summary>
    /// ������ ��� �Լ�
    /// </summary>
    /// <param name="pngNum"></param>
    public void Capture(int pngNum)
    {
        //StartCoroutine(takeScreenShot(pngNum)); // screenshot of a particular UI Element.
        StartCoroutine(takeScreenShot(pngNum));
    }
    /// <summary>
    /// ���� ���� ������ ���� �ؽ��� �ٲٱ� (�ѱ���, �߱���, �Ϻ���� ���η� ǥ�õǾ� ����)
    /// </summary>
    /// <param name="beginNum"></param>
    public void resultBody(int beginNum)
    {
        userInfo = ch._info;
        split_userInfo = userInfo.Split('/');
        
        for (int i = beginNum; i < beginNum + 4; i++)
        {
            // ��ũ�� ������ �������� ���͸� �־ ���η� ǥ������
            if (four_j >= 1)
            {
                // �ܾ� �ϳ��ϳ��� �и��ϰ� �����̽��� ���� �� �ٽ� ��ħ
                foreach (char c in split_userInfo[i])
                {
                    moreSplited += c + "\n";
                }

                four_texts[four_j].text = moreSplited;
                four_j++;
                moreSplited = "";
            }
            else
            {
                four_texts[four_j].text = split_userInfo[i];
                four_j++;
            }
            // ������ �������� �� �־����� ĸ��
            if ((i % 4) == 3)
            {
                four_j = 0;
            }
        }
    }
    /// <summary>
    /// ���� ��¥�� ���Ϳ� �Բ� �־���
    /// </summary>
    public void ResultDate()
    {
        GetYear();
        GetMonth();
        GetDay();
    }
    /// <summary>
    /// ���� �⵵
    /// </summary>
    public void GetYear()
    {
        string combineYear = "";
        string year = DateTime.Now.ToString("yyyy");
        foreach(char c in year)
        {
            combineYear += c + "\n";
        }
        txtYear.text = combineYear;
    }
    /// <summary>
    /// ���� ��
    /// </summary>
    public void GetMonth()
    {
        string combineMonth = "";
        string month = DateTime.Now.ToString("MM");
        foreach (char c in month)
        {
            combineMonth += c + "\n";
        }
        txtMonth.text = combineMonth;
    }
    /// <summary>
    /// ���� ��
    /// </summary>
    public void GetDay()
    {
        string combineDay = "";
        string day = DateTime.Now.ToString("dd");
        foreach (char c in day)
        {
            combineDay += c + "\n";
        }
        txtDay.text = combineDay;
    }
}
