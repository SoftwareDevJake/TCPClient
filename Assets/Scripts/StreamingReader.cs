using UnityEngine;

public class StreamingReader : MonoBehaviour
{
    //�� ���氡���ϰ�

    int m_iLineNum = 0;
    string m_sFileName = "Settings";

    private void Awake()
    {
        ReadString();
    }
    /// <summary>
    /// ���� �� �ٲ�� �ϴ� �κ��� �ؽ�Ʈ ���Ͽ��� �����Ͽ� ���� �� �� �ֵ��� ��.
    /// StreamingAssets ���� �ȿ� ����.
    /// </summary>
    void ReadString()
    {
        System.IO.StreamReader _reader = new System.IO.StreamReader(Application.dataPath + "/StreamingAssets" + "/" + m_sFileName + ".txt");

        while (!_reader.EndOfStream)
        {
            if (m_iLineNum == 1) // 0�� �ؽ�Ʈ ������ ���� ���κ� (0�� ������ ¦���� ����, Ȧ���� ������ ��
            {
                UIManager.seconds = float.Parse(_reader.ReadLine()); // ������ �ð�
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