using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

public class IIILAB_Server : MonoBehaviour
{
    //private string mac_address = ""; // PC 1
    private byte[] m_MacAddress; // 6�ڸ� ������ �ּ�(�ƾ�巹��)�� ������ �˴ϴ�.

    public void WakeOnLan(string mac_address)
    {
        m_MacAddress = PhysicalAddress.Parse(mac_address).GetAddressBytes();

        UdpClient client = new UdpClient();
        client.Connect(IPAddress.Broadcast, 40000);

        byte[] packet = new byte[17 * 6];

        for (int i = 0; i < 6; i++)
        {
            packet[i] = 0xFF;
        }

        for (int i = 1; i <= 16; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                packet[i * 6 + j] = m_MacAddress[j];
            }
        }
        client.Send(packet, packet.Length);
    }
}
