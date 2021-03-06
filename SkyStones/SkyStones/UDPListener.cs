using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkyStones
{
    class UDPListener
    {
        UdpClient udpClient;
        public UDPListener()
        {
            udpClient = new UdpClient();
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 6969));
            udpClient.EnableBroadcast = true;
        }
        public void Start()
        {
            SharedResources.Instance.playersFound.Clear();
            var from = new IPEndPoint(0, 0);
            Task.Run(() =>
            {
                while (true)
                {
                    var recvBuffer = udpClient.Receive(ref from);
                    string msg = Encoding.UTF8.GetString(recvBuffer);
                    if (msg.ElementAt(0) == 'd')
                    {
                        string ip = from.Address.ToString();
                        string plynick = msg.Substring(1);
                        if (plynick != SharedResources.Instance.nickname)
                        {
                            SharedResources.Instance.playersFound.Add(new LocalPlayer(plynick, ip));
                        }
                        var data = Encoding.UTF8.GetBytes("h" + SharedResources.Instance.nickname);
                        udpClient.Send(data, data.Length, ip, 6969);
                    }
                    else if (msg.ElementAt(0) == 'h')
                    {
                        string plynick = msg.Substring(1);
                        string ip = from.Address.ToString();
                        if (plynick != SharedResources.Instance.nickname)
                        {
                            SharedResources.Instance.playersFound.Add(new LocalPlayer(plynick, ip));
                        }
                    }
                }
            });
        }
        public void sendPing()
        {
            var data = Encoding.UTF8.GetBytes("d" + SharedResources.Instance.nickname);
            udpClient.Send(data, data.Length, new IPEndPoint(IPAddress.Broadcast,6969));
        }
    }
}
