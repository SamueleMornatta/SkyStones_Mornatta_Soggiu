using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SkyStones
{
    class Invite
    {
        Thread t;
        TcpClient connection;
        bool sender;
        public string othernick { get; set; }
        public Invite(TcpClient connection)
        {
            this.connection = connection;
            sender = false;
            t = new Thread(new ThreadStart(Listen));
        }
        public Invite(string ip)
        {
            this.connection = new TcpClient(ip, 6969);
            SendInvite();
            sender = true;
            t = new Thread(new ThreadStart(Listen));
        }
        public void Listen()
        {
            NetworkStream stream = connection.GetStream();
            bool active = true;
            while (active)
            {
                byte[] data = new byte[256];
                string responseData = String.Empty;
                int bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                if (responseData.ElementAt(0) == '0')
                {
                    if (sender)
                    {
                        if (responseData.ElementAt(1) == 'y')
                        {
                            othernick = responseData.Substring(2);
                            data = System.Text.Encoding.ASCII.GetBytes("0y");
                            stream.Write(data, 0, data.Length);
                            active = false;
                            SharedResources.Instance.gameSocket = connection;
                            var w = Application.Current.Windows[0];
                            w.Close();
                            // PASSARE ALLA SCHERMATA DI GAMEPLAY
                        }
                        else if (responseData.ElementAt(1) == 'n')
                        {
                            active = false;
                        }
                    }
                    else
                    {
                        if (responseData.ElementAt(1) == 'y')
                        {
                            active = false;
                            SharedResources.Instance.gameSocket = connection;
                            var w = Application.Current.Windows[0];
                            w.Close();
                            // PASSARE ALLA SCHERMATA DI GAMEPLAY
                        }
                        else if (responseData.ElementAt(1) == 'n')
                        {
                            active = false;
                        }
                    }
                }
            }
        }
        public void Start()
        {
            t.Start();
        }
        public void SendInvite()
        {
            NetworkStream stream = connection.GetStream();
            byte[] data = System.Text.Encoding.ASCII.GetBytes("0" + SharedResources.Instance.nickname);
            stream.Write(data, 0, data.Length);
        }
        public void acceptInvite()
        {
            NetworkStream stream = connection.GetStream();
            byte[] data = System.Text.Encoding.ASCII.GetBytes("0y" + SharedResources.Instance.nickname);
            stream.Write(data, 0, data.Length);
        }
        public void denyInvite()
        {
            NetworkStream stream = connection.GetStream();
            byte[] data = System.Text.Encoding.ASCII.GetBytes("0n");
            stream.Write(data, 0, data.Length);
        }
        public void setNick(string tmp)
        {
            othernick = tmp;
        }
    }
}
