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
        WindowPlay play;
        bool sender;
        public string othernick { get; set; }
        public Invite(TcpClient connection)
        {
            this.connection = connection;
            stream = connection.GetStream();
            sender = false;
            t = new Thread(new ThreadStart(Listen));
            Start();
        }
        public Invite(string ip, WindowPlay play)
        {
            this.connection = new TcpClient(ip, 6969);
            stream = connection.GetStream();
            SendInvite();
            sender = true;
            t = new Thread(new ThreadStart(Listen));
            Start();
            this.play = play;
        }
        public void Listen()
        {
            try
            {
                bool active = true;
                while (active)
                {
                    byte[] data = new byte[256];
                    string responseData = null;
                    int i;
                    i = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, i);
                    Console.WriteLine(responseData);
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
                                Application.Current.Dispatcher.Invoke(new Action(() => {
                                    play.caninvite = true;
                                    play.checkifcaninvite();
                                    Application.Current.Windows[0].Close();
                                    gameplay g = new gameplay();
                                    g.Show();
                                    MessageBox.Show("Invite accepted by " + othernick);
                                }));
                            }
                            else if (responseData.ElementAt(1) == 'n')
                            {
                                active = false;
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    play.caninvite = true;
                                    play.checkifcaninvite();
                                    MessageBox.Show("Invite Denied!");
                                }));
                            }
                        }
                        else
                        {
                            if (responseData.ElementAt(1) == 'y')
                            {
                                active = false;
                                SharedResources.Instance.gameSocket = connection;
                                Application.Current.Dispatcher.Invoke(new Action(() => { Application.Current.Windows[0].Close(); }));
                                gameplay g = new gameplay();
                                g.Show();
                            }
                            else if (responseData.ElementAt(1) == 'n')
                            {
                                active = false;
                            }
                            else
                            {
                                othernick = responseData.Substring(1);
                                Console.WriteLine(othernick);
                            }
                        }
                    }
                }
                Thread.Sleep(0);
            }
            catch (Exception err)
            {
                t.Abort();
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
            try
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes("0y" + SharedResources.Instance.nickname);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
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
