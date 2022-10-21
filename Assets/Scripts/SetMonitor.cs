using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class SetMonitor : MonoBehaviour
{

    #if UNITY_STANDALONE_WIN || UNITY_EDITOR
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(System.String className, System.String windowName);
    /// <summary>
    /// ���� �� �� �� ������ �󿡼��� ��ġ ����
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="resX"></param>
    /// <param name="resY"></param>
    public static void SetPosition(int x, int y, int resX = 0, int resY = 0)
    {
        SetWindowPos(FindWindow(null, System.Diagnostics.Process.GetCurrentProcess().ProcessName), 0, x, y, resX, resY, resX * resY == 0 ? 1 : 0);
    }

    /// <summary>
    /// exe������ â ���� �� ���κа� ȭ���� ���� �� ���κа� ��ġ ��Ŵ
    /// </summary>
    void Awake()
    {
        SetPosition(-10, 0);
    }
#endif
}
