using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class DynamicIPSetter : MonoBehaviour
{
    public UnityTransport transport;  // Public reference to the UnityTransport component

    void Start()
    {
        if (transport == null)
        {
            Debug.LogError("UnityTransport component not assigned!");
            return;
        }

        transport.ConnectionData.Address = GetLocalIPAddress();
    }

    string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
}
