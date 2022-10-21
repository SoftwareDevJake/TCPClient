using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgramRestart : MonoBehaviour
{
    public Sprite[] sprite;
    public Image current;

    ClientSend cs = new ClientSend();

    public void OnButtonClicked()
    {
        cs.SendRestart(Strings.PROGRAM_RESTART);
        StartCoroutine(BackAndForth());
    }

    public void mouseDown()
    {
        current.sprite = sprite[1];
        cs.SendRestart(Strings.PROGRAM_RESTART);
    }

    public void mouseUp()
    {
        current.sprite = sprite[0];
    }

    IEnumerator BackAndForth()
    {
        current.sprite = sprite[1];
        yield return new WaitForSeconds(1.0f);
        current.sprite = sprite[0];
    }
}
