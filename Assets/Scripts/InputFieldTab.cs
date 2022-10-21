using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Tap, Shift+Tab �� ���� ���� �� �Ű��� inputfield ���� �����ϰ� �迭�� �߰�
/// </summary>
public class InputFieldTab : MonoBehaviour
{
    InputFieldTabManager inputFieldTabMrg = new InputFieldTabManager();

    public InputField inputField1;
    public InputField inputField2;
    public InputField inputField3;
    public InputField inputField4;

    private void Start()
    {
        inputFieldTabMrg.Add(inputField1);
        inputFieldTabMrg.Add(inputField2);
        inputFieldTabMrg.Add(inputField3);
        inputFieldTabMrg.Add(inputField4);
        inputFieldTabMrg.SetFocus();
    }

    private void Update()
    {
        inputFieldTabMrg.CheckFocus();
    }
}
