using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultBody : MonoBehaviour
{
    // ���� ���ű��� ���� �ؽ�Ʈ��
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
        string test = "1/��ȫ��/210/220/2/�質��/100/200/3/��������/201/203/4/����ȿ/200/80";
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
