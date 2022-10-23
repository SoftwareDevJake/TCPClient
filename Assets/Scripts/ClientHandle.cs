using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using UnityEngine.UI;

public class ClientHandle
{
    public void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");

        Client.instance.myId = _myId;
        // use ClientSend to send Data back to Server
        ClientSend.WelcomeReceived();
        Client.isConnected = true;
    }
    // You can add like below. You receive data through packet
    public void ReceivedUserInfo(Packet _packet)
    {
        string _name = _packet.ReadString();
        string[] name = _name.Split('/');
        int userIndex = int.Parse(name[0]) - 1;
        Debug.Log("index : " + userIndex);
        
        Debug.Log($"Sent user info: {_name}");
    }
}
