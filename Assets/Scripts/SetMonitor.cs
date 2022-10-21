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
    /// 빌드 후 열 때 윈도우 상에서의 위치 조절
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
    /// exe파일의 창 왼쪽 위 끝부분과 화면의 왼쪽 위 끝부분과 일치 시킴
    /// </summary>
    void Awake()
    {
        SetPosition(-10, 0);
    }
#endif
}
