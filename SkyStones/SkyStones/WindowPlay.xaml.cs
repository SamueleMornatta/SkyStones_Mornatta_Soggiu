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
            UDPListener listen = new UDPListener();
            listen.sendPing();
            listen.Start();
            viewInvites.ItemsSource = null;
            viewInvites.ItemsSource = SharedResources.Instance.invites;
            viewPlayers.ItemsSource = null;
            viewPlayers.ItemsSource = SharedResources.Instance.playersFound;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            viewPlayers.ItemsSource = null;
            viewPlayers.ItemsSource = SharedResources.Instance.playersFound;
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
        private void Invite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnmaninv_Click(object sender, RoutedEventArgs e)
        {
            Invite inv = new Invite(ipman.Text);
        }
    }
}
