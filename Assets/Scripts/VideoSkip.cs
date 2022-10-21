using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VideoSkip : MonoBehaviour
{
    public Sprite[] sprite;
    public Image current;

    ClientSend cs = new ClientSend();

    public void OnButtonClicked()
    {
        cs.SendVideoSkip(Strings.VIDEO_SKIP);
        StartCoroutine(BackAndForth());
    }

    public void mouseDown()
    {
        current.sprite = sprite[1];
        cs.SendVideoSkip(Strings.VIDEO_SKIP);
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
