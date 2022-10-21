using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���������� ������ ������ ���� ��
/// </summary>
public class ClientSend
{
    /// <summary>
    /// ������ ���� �����ϱ� �� ��Ŷ�� ���̸� ����
    /// </summary>
    /// <param name="_packet"></param>
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    #region Packets
    /// <summary>
    /// ������ ���� ���� �� ȯ�� �޼��� ����
    /// </summary>
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("JinjuAdministrator has joined the server!");

            SendTCPData(_packet);
        }
    }
    /// <summary>
    /// �÷��̾� �̸����� �Է��� �� ������ ���� ����
    /// </summary>
    /// <param name="_name"></param>
    public void SendNames(string _name)
    {
        using (Packet _packet = new Packet((int)ClientPackets.sendName))
        {
            _packet.Write(_name);

            SendTCPData(_packet);
        }
    }
    /// <summary>
    /// ������ ������ ���� ���� �� ���� ����
    /// </summary>
    /// <param name="_contentStart"></param>
    public void SendContentStart(string _contentStart)
    {
        using (Packet _packet = new Packet((int)ClientPackets.ContentStart))
        {
            _packet.Write(_contentStart);

            SendTCPData(_packet);
        }
    }
    /// <summary>
    /// �� ���� ���� �� ���� ����
    /// </summary>
    /// <param name="_language"></param>
    public void SendLanguageInfo(string _language)
    {
        using (Packet _packet = new Packet((int)ClientPackets.Language))
        {
            _packet.Write(_language);
            SendTCPData(_packet);
        }
    }

    public void SendMode(string _modes)
    {
        using (Packet _packet = new Packet((int)ClientPackets.Mode))
        {
            _packet.Write(_modes);
            SendTCPData(_packet);
        }
    }

    public void SendShutdown(string _shutdown)
    {
        using(Packet _packet = new Packet((int)ClientPackets.Shutdown))
        {
            _packet.Write(_shutdown);
            SendTCPData(_packet);
        }
    }

    public void SendVideoSkip(string _skip)
    {
        using (Packet _packet = new Packet((int)ClientPackets.videoSkip))
        {
            _packet.Write(_skip);
            SendTCPData(_packet);
        }
    }

    public void SendRestart(string _restart)
    {
        using (Packet _packet = new Packet((int)ClientPackets.restart))
        {
            _packet.Write(_restart);
            SendTCPData(_packet);
        }
    }
    #endregion
}
