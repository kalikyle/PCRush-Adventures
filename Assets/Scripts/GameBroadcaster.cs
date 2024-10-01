using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;

public class GameBroadcaster : MonoBehaviour
{
    private const int BroadcastPort = 47777; // Port for broadcasting

    public void BroadcastGameCreation(string ipAddress, string gameName, string GameMode)
    {
        UdpClient udpClient = new UdpClient();
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, BroadcastPort);

        string message = $"GAME:{ipAddress} GAMENAME:{gameName} GAMEMODE:{GameMode}";
        byte[] data = Encoding.UTF8.GetBytes(message);

        udpClient.Send(data, data.Length, endPoint);
        udpClient.Close();
    }

    public void BroadcastGameCancellation(string ipAddress)
    {
        UdpClient udpClient = new UdpClient();
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, BroadcastPort);

        string message = $"CANCEL:{ipAddress}";
        byte[] data = Encoding.UTF8.GetBytes(message);

        udpClient.Send(data, data.Length, endPoint);
        udpClient.Close();
    }
}
