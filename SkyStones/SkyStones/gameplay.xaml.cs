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
        public gameplay()
        {
            InitializeComponent();
            deck = Deck.Instance;
            shared = SharedResources.Instance;
            Card mornatta = new Card(1000, new int[] { 4, 4, 4, 4 }, new BitmapImage(), "avatar");
            Card soggiu = new Card(1001, new int[] { 4, 4, 4, 4 }, new BitmapImage(), "avatar");
            Card roncoroni = new Card(1002, new int[] { 4, 4, 4, 4 }, new BitmapImage(), "avatar");
            Card marelli = new Card(1003, new int[] { 4, 4, 4, 4 }, new BitmapImage(), "avatar");
            //tav.Items.Add(mornatta);
            //tav.Items.Add(soggiu);
            //tav.Items.Add(roncoroni);
            //tav.Items.Add(marelli);            
        }
    }
}
