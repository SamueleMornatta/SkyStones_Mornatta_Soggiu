using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        public WindowPlay()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UdpClient udpClient = new UdpClient(6969);
            var data = Encoding.UTF8.GetBytes("d");
            udpClient.Send(data, data.Length, "255.255.255.255", 6969);
            var from = new IPEndPoint(0, 0);
            Task.Run(() =>
            {
                while (true)
                {
                    var recvBuffer = udpClient.Receive(ref from);
                    string msg = Encoding.UTF8.GetString(recvBuffer);
                    if (msg.ElementAt(0) == 'h')
                    {
                        string plynick = msg.Substring(1);
                        string ip = from.Address.ToString();
                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            viewPlayers.Items.Add(new LocalPlayer(plynick, ip));
                        }));
                    }
                }
            });
            viewInvites.ItemsSource = null;
            viewInvites.ItemsSource = SharedResources.Instance.invites;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            viewInvites.ItemsSource = null;
            viewInvites.ItemsSource = SharedResources.Instance.invites;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Invite inv = button.DataContext as Invite;
            inv.acceptInvite();
            SharedResources.Instance.invites.Remove(inv);
            for (int i = 0; i < SharedResources.Instance.invites.Count; i++)
            {
                Invite tmp = (Invite)viewInvites.Items.GetItemAt(i);
                tmp.denyInvite();
                SharedResources.Instance.invites.Remove(tmp);
            }
            viewInvites.ItemsSource = null;
            viewInvites.ItemsSource = SharedResources.Instance.invites;
        }
        private void Deny_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Invite inv = button.DataContext as Invite;
            inv.denyInvite();
            SharedResources.Instance.invites.Remove(inv);
            viewInvites.ItemsSource = null;
            viewInvites.ItemsSource = SharedResources.Instance.invites;
        }
    }
}
