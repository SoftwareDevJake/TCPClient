using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultBody : MonoBehaviour
{
    // 현재 공신교서 안의 텍스트들
    public Text txtYear;
    public Text txtMonth;
    public Text txtDay;
    public Text txtPointHansando;
    public Text txtPointJinju;
    public Text txtRank;
    public Text txtName;

    // Start is called before the first frame update
    void Start()
    {
        string test = "1/김홍수/210/220/2/김나리/100/200/3/누구누구/201/203/4/이이효/200/80";
        string[] split_test;
        Text[] four_texts = {txtRank, txtName, txtPointHansando, txtPointJinju};
        int j = 0;

        split_test = test.Split('/');

        for(int i = 0; i < split_test.Length; i++)
        {
            //Debug.Log(split_test[i]);
            four_texts[j].text = split_test[i];
            Debug.Log(four_texts[j].text);
            j++;

            if ((i % 4) == 3)
            {
                //Debug.Log("devided");
                j = 0;
            }
        }
        
    }
}
