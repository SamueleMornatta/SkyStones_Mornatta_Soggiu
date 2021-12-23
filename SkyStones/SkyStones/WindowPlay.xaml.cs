using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
    /// Interaction logic for WindowPlay.xaml
    /// </summary>
    public partial class WindowPlay : Window
    {
        public WindowPlay()
        {
            InitializeComponent();
        }
        void OnUdpData(IAsyncResult result)
        {
            UdpClient socket = result.AsyncState as UdpClient;
            IPEndPoint source = new IPEndPoint(0, 0);
            byte[] message = socket.EndReceive(result, ref source);
            string msg = BitConverter.ToString(message);
            if (msg.ElementAt(0) == 'h')
            {
                string plynick = msg.Substring(1);
                string ip = source.Address.ToString();
                viewPlayers.Items.Add(new LocalPlayer(plynick, ip));
            }

            socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UdpClient udpClient = new UdpClient(6969);
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 6969));
            var data = Encoding.UTF8.GetBytes("d");
            udpClient.Send(data, data.Length, "255.255.255.255", 6969);
            udpClient.BeginReceive(new AsyncCallback(OnUdpData), udpClient);
        }
    }
}
