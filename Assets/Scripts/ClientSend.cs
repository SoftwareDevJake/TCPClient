using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend
{
    
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("this client has joined the server!");

            SendTCPData(_packet);
        }
    }
    
    public void SendNames(string _name)
    {
        using (Packet _packet = new Packet((int)ClientPackets.sendName))
        {
            _packet.Write(_name);

            SendTCPData(_packet);
        }
    }
    #endregion
}
