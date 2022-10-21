using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
using System.IO;

public class CreateQRcode : MonoBehaviour
{
    // 코드 생성 부분
    public GameObject button;
    private string textInQR;
    // 플레이어 수
    private int playerNumbers;

    // QR코드 보여주는 부분
    private int imgSize = 5;
    public GameObject[] ImageOnPanels;
    private RawImage[] imgs;

    void Start()
    {
        imgs = new RawImage[imgSize];
        playerNumbers = 4;
    }

    /// <summary>
    /// QR 코드 내용 가져올 변수
    /// </summary>
    /// <param name="textForEncoding"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns>Color32</returns>
    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        // QRcode Make (인코딩작업)
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        
        return writer.Write(textForEncoding);
    }// QR code 안에 저장할 텍스트와 함께 생성
    /// <summary>
    /// 만들어진 QR코드를 파일 위치에 생성
    /// </summary>
    /// <param name="text"></param>
    /// <param name="playerNumbers"></param>
    /// <returns>Texture2D</returns>
    public static Texture2D generateQR(string text, int playerNumbers)
    {
        //  인코딩 작업을 위한 Encode 함수 호출
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();

        // 인코드 완료후 PNG 파일로 만들기 위한 File 시스템
        byte[] bytes = encoded.EncodeToPNG();
        //Application.dataPath // 빌드 후에도 아래 File. 함수가 제대로 작동하는지 확인해야함
        //Application.persistentDataPath // 빌드 후 위치 찾아보기
        //string test = "D:\\UnityProject\\JinjuAdministrator\\Assets\\Resources\\Textures\\" + text + ".png";
        //Debug.Log(test);
 
        File.WriteAllBytes("D:/UnityProject/JinjuTotal/" + playerNumbers + "P.png", bytes); // 파일 생성 위치
        
        return encoded;
    }
    /// <summary>
    /// 새 QR코드 모양 생성
    /// </summary>
    public void CreateNewQRcode()
    {
        Texture2D[] tex = new Texture2D[imgSize];

        for(int i = 1; i <= playerNumbers; i++)
        {
            textInQR = "http://210.178.71.189:26950/img/" + i + ".png"; // QR코드 스캔에 나오는 string 값
            generateQR(textInQR, i);
        }
    }
}
