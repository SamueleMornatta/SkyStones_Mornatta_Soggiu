using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkyStones
{
    /*
    * PALETTE:
    * #2D2424 marrone 1
    * #5C3D2E marrone 2
    * #E0C097 marrone 3
    * #14DD75 verde
    * #BB0104 rosso
     */
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SharedResources shared;
        private Deck deck;
        public MainWindow()
        {
            InitializeComponent();
            shared = SharedResources.Instance;
            deck = Deck.Instance;
        }

        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGioca_Click(object sender, RoutedEventArgs e)
        {
            WindowPlay W = new WindowPlay();
            W.Show();
            this.Close();
        }

        private void btnColl_Click(object sender, RoutedEventArgs e)
        {
            WindowCollection wc = new WindowCollection();
            wc.Show();
            this.Close();
        }
    }
}
