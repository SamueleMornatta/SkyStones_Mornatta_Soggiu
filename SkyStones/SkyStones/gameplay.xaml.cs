using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SkyStones
{
    /// <summary>
    /// Logica di interazione per gameplay.xaml
    /// </summary>
    public partial class gameplay : Window
    {
        private Deck deck;
        private SharedResources shared;
        private TcpClient conn;
        private StreamWriter writer;
        private StreamReader reader;
        private Thread t; //L'ho fatto io non ti preoccupare Pozzi

        public gameplay()
        {
            InitializeComponent();
            deck = Deck.Instance;
            shared = SharedResources.Instance;
            conn = shared.gameSocket;
            NetworkStream stream = conn.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            t = new Thread(new ThreadStart(listener));
            t.Start();
            //shared.trec.stop();
        }

        public void send()
        {
            writer.WriteLine(sender.Text);
        }


        public void listener()
        {            
            while (true)
            {
                while (reader.Peek()>=0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => { MessageBox.Show(reader.ReadLine()); })); 
                }
            }
        }

        private void btnsend_Click(object sender, RoutedEventArgs e)
        {
            send();
        }
    }
}
