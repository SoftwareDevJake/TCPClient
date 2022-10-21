using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using SD = System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.NetworkInformation;


public class PCPowerControl : MonoBehaviour
{
    IIILAB_Server server; // Wake On Lan

    public int PcSwitchIndex;

    public Button[] powerBtns; // index 0 : total, 1 : threeSide, 2 : bottom

    public Image[] thisImg;

    public Sprite normalImg;
    public Sprite changeImg;

    public static int[] index = new int[3];

    ClientSend cs = new ClientSend();

    private bool BottomPCON = false;
    private bool ThreeSidePCON = false;

    void Start()
    {
        server = this.gameObject.AddComponent<IIILAB_Server>();
        for(int i = 0; i < index.Length; i++)
        {
            index[i] = Strings.OFF;
        }
        if (pingTest(Strings.BOTTOM_IP_ADDRESS)) // 바닥PC ip 주소로 핑을 보내 확인
        {
            //powerBtns[2].onClick.Invoke();
            BottomPCON = true;
        }

        if(pingTest(Strings.THREESIDE_IP_ADDRESS)) // 삼면PC ip 주소로 핑을 보내 확인
        {
            //powerBtns[1].onClick.Invoke();
            ThreeSidePCON = true;
        }
        // index 0 : total PC, 1 : ThreeSide PC, 2 : Bottom PC
        if(BottomPCON && ThreeSidePCON)
        {
            if (index[Strings.TOTAL_PC] == Strings.OFF)
            {
                for (int i = 0; i < thisImg.Length; i++)
                {
                    thisImg[i].sprite = changeImg;
                    index[i] = Strings.ON;
                }
            }
        }
        else
        {
            if (ThreeSidePCON == true)
            {
                OnOrOff(1, "");
            }

            if (BottomPCON == true)
            {
                OnOrOff(2, "");
                //CheckIfConnected();
            }
        }
    }

    /// <summary>
    /// 어떤 버튼이 눌렸냐에 따라 버튼 ON, OFF 조절
    /// j가 2이면 모두 ON, 그 외에는 전체 버튼은 OFF
    /// </summary>
    public void OnButtonClicked()
    {
        // PC 전체의 전원을 조종 할 때
        if (PcSwitchIndex == Strings.TOTAL_PC)
        {
            ChangeTotalSwitch();
        }
        else
        {
            int j = 0;
            changeEachSwitch();
            for (int i = 1; i < thisImg.Length; i++)
            {

                if (index[i] == Strings.ON)
                {
                    j++;
                }
            }
            if (j == 2)
            {
                thisImg[0].sprite = changeImg;
                index[0] = Strings.ON;
                j = 0;
            }
            else
            {
                thisImg[0].sprite = normalImg;
                index[0] = Strings.OFF;
            }
        }
    }

    /// <summary>
    /// 전체 버튼이 눌릴 때 (TOTALPC = 0)
    /// 모든 전원 버튼 ON or OFF + WOL 명령
    /// 또는
    /// 모든 전원 버튼 ON or OFF + shutdown명령
    /// </summary>
    private void ChangeTotalSwitch()
    {
        // 클릭하여 전원을 켤 때
        if (index[Strings.TOTAL_PC] == Strings.OFF)
        {
            for (int i = 0; i < thisImg.Length; i++)
            {
                if(index[i] != Strings.ON)
                {
                    thisImg[i].sprite = changeImg;
                    index[i] = Strings.ON;
                }
            }
            server.WakeOnLan(Strings.THREESIDE_MAC_ADDRESS); // 3면 PC
            server.WakeOnLan(Strings.BOTTOM_MAC_ADDRESS); // Bottom PC
        }
        // 클릭하여 전원을 끌 때
        else if (index[Strings.TOTAL_PC] == Strings.ON)
        {
            for (int i = 0; i < thisImg.Length; i++)
            {
                if(index[i] != Strings.OFF)
                {
                    thisImg[i].sprite = normalImg;
                    index[i] = Strings.OFF;
                }
            }

            cs.SendShutdown(Strings.SHUTDOWN_BOTTOM);
            cs.SendShutdown(Strings.SHUTDOWN_THREESIDE);
        }
    }

    /// <summary>
    /// 각각의 버튼들이 눌려질 때 (THREESIDEPC = 1, BOTTOMPC = 2)
    /// </summary>
    private void changeEachSwitch()
    {
        if (PcSwitchIndex == Strings.THREESIDE_PC)
        {
            if (index[PcSwitchIndex] == Strings.OFF)
            {
                server.WakeOnLan(Strings.THREESIDE_MAC_ADDRESS); // 3면 PC
            }

            OnOrOff(PcSwitchIndex, Strings.SHUTDOWN_THREESIDE);
            
        }

        else if (PcSwitchIndex == Strings.BOTTOM_PC)
        {
            if (index[PcSwitchIndex] == Strings.OFF)
            {
                server.WakeOnLan(Strings.BOTTOM_MAC_ADDRESS); // Bottom PC
            }

            OnOrOff(PcSwitchIndex, Strings.SHUTDOWN_BOTTOM);
            
            CheckIfConnected();
        }
    }
    /// <summary>
    /// 버튼의 켜고 끔을 실행하는 함수
    /// </summary>
    public void OnOrOff(int PcSwitchIndex, string shutdownCmd)
    {
        if (index[PcSwitchIndex] == Strings.OFF)
        {
            thisImg[PcSwitchIndex].sprite = changeImg;
            index[PcSwitchIndex] = Strings.ON;
        }
        else if (index[PcSwitchIndex] == Strings.ON)
        {
            thisImg[PcSwitchIndex].sprite = normalImg;
            index[PcSwitchIndex] = Strings.OFF;
            cs.SendShutdown(shutdownCmd);
        }
    }
    /// <summary>
    /// 배치 파일을 사용하기 위한 함수
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="arguments"></param>
    public void UseBatchFile(string fileName, string arguments)
    {
        SD.ProcessStartInfo psi = new SD.ProcessStartInfo();
        psi.FileName = fileName;
        psi.Arguments = arguments;

        psi.RedirectStandardOutput = true;
        psi.UseShellExecute = false;

        SD.Process proc = SD.Process.Start(psi);

        while (true)
        {
            string txt = proc.StandardOutput.ReadLine(); // blocking 함수

            if (txt == null) // 프로세스가 종료한 경우 null 반환
            {
                break;
            }

            Console.WriteLine(txt);
        }
    }

    public void CheckIfConnected()
    {
        if (!Client.isConnected)
        {
            UIManager.instance.ReconnectTheServer(); // 서버가 켜진 후 재접속
        }
    }

    public bool pingTest(string ipAddress)
    {
        try
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

            PingOptions options = new PingOptions();

            options.DontFragment = true;

            //전송할 데이터를 입력
            string data = "aaaaa";
            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
            int timeout = 120;

            //IP 주소를 입력
            PingReply reply = ping.Send(IPAddress.Parse(ipAddress), timeout, buffer, options);

            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("Succeess");
                //Debug.Log("Success");
                return true;
            }
            else
            {
                Console.WriteLine("Fail");
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
}
