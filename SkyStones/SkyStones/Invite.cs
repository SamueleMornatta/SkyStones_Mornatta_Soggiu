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
        NetworkStream stream;
        bool sender;
        public string othernick { get; set; }
        public Invite(TcpClient connection)
        {
            this.connection = connection;
            stream = connection.GetStream();
            sender = false;
            t = new Thread(new ThreadStart(Listen));
        }
        public Invite(string ip)
        {
            this.connection = new TcpClient(ip, 6969);
            stream = connection.GetStream();
            SendInvite();
            sender = true;
            t = new Thread(new ThreadStart(Listen));
        }
        public void Listen()
        {
            bool active = true;
            while (active)
            {
                byte[] data = new byte[256];
                string responseData = null;
                int i;
                while ((i = stream.Read(data, 0, data.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, i);
                    Console.WriteLine("Received: {0}", responseData);
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(responseData);
                }
                //int bytes = stream.Read(data, 0, data.Length);
                //responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
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
            byte[] data = System.Text.Encoding.ASCII.GetBytes("0" + SharedResources.Instance.nickname);
            stream.Write(data, 0, data.Length);
        }
        public void acceptInvite()
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes("0y" + SharedResources.Instance.nickname);
            stream.Write(data, 0, data.Length);
        }
        public void denyInvite()
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes("0n");
            stream.Write(data, 0, data.Length);
        }
        public void setNick(string tmp)
        {
            othernick = tmp;
        }
    }
}
