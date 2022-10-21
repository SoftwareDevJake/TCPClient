using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public InputField[] info; // 이름

    public int index;
    
    public Image[] playerImgs;

    public static float seconds = 60.0f; // 서버가 켜질 때 까지의 시간을 측정하여 넣어야 함.

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
        yield return new WaitForSeconds(seconds); // 메모장에서 바꿀 수 있음.
        Client.instance.ConnectToServer();
    }

    public void ReconnectTheServer()
    {
        StartCoroutine(reconnect());
    }
}
