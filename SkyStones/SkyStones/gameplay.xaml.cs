using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Logica di interazione per gameplay.xaml
    /// </summary>
    public partial class gameplay : Window
    {
        private Deck deck;
        private SharedResources shared;
        private TcpClient conn;
        public gameplay()
        {
            InitializeComponent();
            deck = Deck.Instance;
            shared = SharedResources.Instance;
            conn = shared.gameSocket;

        }


    }
}
