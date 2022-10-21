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
        if (pingTest(Strings.BOTTOM_IP_ADDRESS)) // �ٴ�PC ip �ּҷ� ���� ���� Ȯ��
        {
            //powerBtns[2].onClick.Invoke();
            BottomPCON = true;
        }

        if(pingTest(Strings.THREESIDE_IP_ADDRESS)) // ���PC ip �ּҷ� ���� ���� Ȯ��
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
    /// � ��ư�� ���ȳĿ� ���� ��ư ON, OFF ����
    /// j�� 2�̸� ��� ON, �� �ܿ��� ��ü ��ư�� OFF
    /// </summary>
    public void OnButtonClicked()
    {
        // PC ��ü�� ������ ���� �� ��
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
    /// ��ü ��ư�� ���� �� (TOTALPC = 0)
    /// ��� ���� ��ư ON or OFF + WOL ���
    /// �Ǵ�
    /// ��� ���� ��ư ON or OFF + shutdown���
    /// </summary>
    private void ChangeTotalSwitch()
    {
        // Ŭ���Ͽ� ������ �� ��
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
            server.WakeOnLan(Strings.THREESIDE_MAC_ADDRESS); // 3�� PC
            server.WakeOnLan(Strings.BOTTOM_MAC_ADDRESS); // Bottom PC
        }
        // Ŭ���Ͽ� ������ �� ��
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
    /// ������ ��ư���� ������ �� (THREESIDEPC = 1, BOTTOMPC = 2)
    /// </summary>
    private void changeEachSwitch()
    {
        if (PcSwitchIndex == Strings.THREESIDE_PC)
        {
            if (index[PcSwitchIndex] == Strings.OFF)
            {
                server.WakeOnLan(Strings.THREESIDE_MAC_ADDRESS); // 3�� PC
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
    /// ��ư�� �Ѱ� ���� �����ϴ� �Լ�
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
    /// ��ġ ������ ����ϱ� ���� �Լ�
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
            string txt = proc.StandardOutput.ReadLine(); // blocking �Լ�

            if (txt == null) // ���μ����� ������ ��� null ��ȯ
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
            UIManager.instance.ReconnectTheServer(); // ������ ���� �� ������
        }
    }

    public bool pingTest(string ipAddress)
    {
        try
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

            PingOptions options = new PingOptions();

            options.DontFragment = true;

            //������ �����͸� �Է�
            string data = "aaaaa";
            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
            int timeout = 120;

            //IP �ּҸ� �Է�
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
