using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanelController : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.SetActive(false);
            Panel1.SetActive(true);
            Panel2.SetActive(true);
        }
    }
}
