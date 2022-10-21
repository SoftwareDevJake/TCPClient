using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ResultToPNG : MonoBehaviour
{
    // png 만드는 부분
    public RectTransform rectT; // Assign the UI element which you wanna capture
    public Image img;
    int width; // width of the object to capture
    int height; // height of the object to capture

    public GameObject Panel1;
    public GameObject Panel2;

    // 정보 바꾸는 부분
    // 현재 공신교서 안의 텍스트들
    public Text txtYear;
    public Text txtMonth;
    public Text txtDay;
    public Text txtPointHansando;
    public Text txtPointJinju;
    public Text txtRank;
    public Text txtName;

    private string userInfo; // 유저 결과 정보
    private string[] split_userInfo; // 위의 결과를 '/' 를 기준으로 나눔

    public Text[] four_texts; // 랭크, 이름, 한산도 점수, 진주 점수의 변수
    private int four_j = 0; // 위 의 인덱스 의 변수
    private int pngNum = 1; // 저장 될 png (player) 변수
    private int beginNum = 0; // 각각의 정보들의 시작 인덱스 변수

    private string moreSplited; // '\n'를 인식하게 한 후 다시 병합할 변수

    ClientHandle ch = new ClientHandle();

    void Start()
    {
        width = System.Convert.ToInt32(rectT.rect.width);
        height = System.Convert.ToInt32(rectT.rect.height);
    }
    /// <summary>
    /// 마지막에 실행할 함수 (결과에 해당하는 입력들을 넣고 png 스크린샷을 찍음
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
    /// 다른 패널들이 있었을 때 Q를 눌러 되돌아가는 기능
    /// </summary>
    private void Update()
    {
        // 돌아가기
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.SetActive(false);
            Panel1.SetActive(true);
            Panel2.SetActive(true);
        }
    }

    /// <summary>
    /// 스크린샷 + 이미지 저장 (결과)
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

        File.WriteAllBytes("C:/inetpub/wwwroot/img/" + pngNum + "P.png", bytes); // 공신교서 결과가 저장되는 위치 (웹 설정 하고 파일의 관리자 권한 설정까지 한 후 가능)

        string imgsrc = System.Convert.ToBase64String(bytes);
        Texture2D scrnShot = new Texture2D(2048, 1024, TextureFormat.ARGB32, false);
        scrnShot.LoadImage(System.Convert.FromBase64String(imgsrc));

        Sprite sprite = Sprite.Create(scrnShot, new Rect(0, 0, scrnShot.width, scrnShot.height), new Vector2(.5f, .5f));
        img.sprite = sprite;
    }
    /// <summary>
    /// 사진을 찍는 함수
    /// </summary>
    /// <param name="pngNum"></param>
    public void Capture(int pngNum)
    {
        //StartCoroutine(takeScreenShot(pngNum)); // screenshot of a particular UI Element.
        StartCoroutine(takeScreenShot(pngNum));
    }
    /// <summary>
    /// 전달 받은 정보를 토대로 텍스쳐 바꾸기 (한국어, 중국어, 일본어는 가로로 표시되어 있음)
    /// </summary>
    /// <param name="beginNum"></param>
    public void resultBody(int beginNum)
    {
        userInfo = ch._info;
        split_userInfo = userInfo.Split('/');
        
        for (int i = beginNum; i < beginNum + 4; i++)
        {
            // 랭크를 제외한 나머지는 엔터를 넣어서 세로로 표현해줌
            if (four_j >= 1)
            {
                // 단어 하나하나를 분리하고 스페이스를 넣은 뒤 다시 합침
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
            // 마지막 정보까지 다 넣었으면 캡쳐
            if ((i % 4) == 3)
            {
                four_j = 0;
            }
        }
    }
    /// <summary>
    /// 현재 날짜도 엔터와 함께 넣어줌
    /// </summary>
    public void ResultDate()
    {
        GetYear();
        GetMonth();
        GetDay();
    }
    /// <summary>
    /// 현재 년도
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
    /// 현재 달
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
    /// 현재 일
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
