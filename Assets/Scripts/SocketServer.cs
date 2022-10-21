using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class SocketServer : MonoBehaviour
{
    static byte[] Buffer { get; set; }
    static Socket sck;

    public void OpenSocketServer()
    {
        sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        sck.Bind(new IPEndPoint(IPAddress.Any, 1234));
        sck.Listen(100);

        Socket accepted = sck.Accept();

        Buffer = new byte[accepted.SendBufferSize];
        int bytesRead = accepted.Receive(Buffer);
        byte[] formatted = new byte[bytesRead];
        for (int i = 0; i < bytesRead; ++i)
        {
            formatted[i] = Buffer[i];
        }

        string strdata = Encoding.UTF8.GetString(formatted);
        Debug.Log(strdata + "\r\n");
        
        for(int i = 0; i < strdata.Length; i++)
        {
            Debug.Log(strdata[i]);
        }

        accepted.Close();
        sck.Close();
    }
}
