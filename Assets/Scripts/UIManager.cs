using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public InputField[] info; // �̸�

    public int index;
    
    public Image[] playerImgs;

    public static float seconds = 60.0f; // ������ ���� �� ������ �ð��� �����Ͽ� �־�� ��.

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //else if (instance != this)
        //{
        //    Debug.Log("Instance already exists, destroying object!");
        //    Destroy(this);
        //}
    }

    IEnumerator reconnect()
    {
        yield return new WaitForSeconds(seconds); // �޸��忡�� �ٲ� �� ����.
        Client.instance.ConnectToServer();
    }

    public void ReconnectTheServer()
    {
        StartCoroutine(reconnect());
    }
}
