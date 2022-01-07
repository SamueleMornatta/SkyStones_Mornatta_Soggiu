using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkyStones
{
    class TCPReceive
    {

        private Thread T;
        private MainWindow M;

        public TCPReceive(/*MainWindow M*/)
        {
            T = new Thread(Receiver);
            //this.M = M;
        }

        public void start()
        {
            T.Start();
        }

        public void stop()
        {
            T.Abort();
        }


        public void Receiver()
        {
            TcpListener server = null;
            try
            {

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(6969);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data

                // Enter the listening loop.
                while (true)
                {
                    //M.Statorich.Content = ("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    //M.Statorich.Content = ("Connected!");
                    Invite inv = new Invite(client);
                    SharedResources.Instance.addInvite(inv);
                }
            }
            catch (SocketException e)
            {
                
            }
        }
    }
}
