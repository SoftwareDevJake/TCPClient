using UnityEngine;

public class StreamingReader : MonoBehaviour
{
    //초 변경가능하게

    int m_iLineNum = 0;
    string m_sFileName = "Settings";

    private void Awake()
    {
        ReadString();
    }
    /// <summary>
    /// 빌드 후 바꿔야 하는 부분을 텍스트 파일에서 변경하여 적용 할 수 있도록 함.
    /// StreamingAssets 파일 안에 있음.
    /// </summary>
    void ReadString()
    {
        System.IO.StreamReader _reader = new System.IO.StreamReader(Application.dataPath + "/StreamingAssets" + "/" + m_sFileName + ".txt");

        while (!_reader.EndOfStream)
        {
            if (m_iLineNum == 1) // 0은 텍스트 파일의 제일 윗부분 (0을 포함한 짝수는 설명, 홀수는 변수의 값
            {
                UIManager.seconds = float.Parse(_reader.ReadLine()); // 재접속 시간
            }
            
            else
            {
                Debug.LogWarning("UNDIFINED DATA   :   " + _reader.ReadLine());
            }
            m_iLineNum++;
        }
        _reader.Close();

    }
}