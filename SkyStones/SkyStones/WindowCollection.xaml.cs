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
    /// Interaction logic for WindowCollection.xaml
    /// </summary>
    public partial class WindowCollection : Window
    {
        private Deck deck;
        private SharedResources shared;
        private Key[] secretcode;
        private int countcode;
        public WindowCollection()
        {
            InitializeComponent();
            deck = Deck.Instance;
            shared = SharedResources.Instance;
            tabavatar.Visibility = Visibility.Hidden;
            secretcode = new Key[]{ Key.A, Key.V , Key.A , Key.T , Key.A , Key.R};
            Card mornatta = new Card(1000, new int[] { 4, 4, 4, 4 }, new BitmapImage(), "avatar");
            mornatta.nome = "Samuele Mornatta";
            Card soggiu = new Card(1001, new int[] { 4, 4, 4, 4 }, new BitmapImage(), "avatar");
            soggiu.nome = "Marco Soggiu";
            Card roncoroni = new Card(1002, new int[] { 4, 4, 4, 4 }, new BitmapImage(), "avatar");
            roncoroni.nome = "Daniele Roncoroni";
            Card marelli = new Card(1003, new int[] { 4, 4, 4, 4 }, new BitmapImage(), "avatar");
            marelli.nome = "Giacomo Marelli";
            viewAvatar.Items.Add(mornatta);
            viewAvatar.Items.Add(soggiu);
            viewAvatar.Items.Add(roncoroni);
            viewAvatar.Items.Add(marelli);
            countcode = 0;
            RefreshUI();
        }
        private void RefreshUI()
        {
            viewDeck.Items.Clear();
            viewAir.Items.Clear();
            viewEarth.Items.Clear();
            viewFire.Items.Clear();
            viewWater.Items.Clear();
            txtNum.Content = deck.getAmmountCards();
            for (int i = 0; i < deck.getAmmountCards(); i++)
            {
                viewDeck.Items.Add(deck.getCardAt(i));
            }
            List<Card> air = shared.getCategory("air");
            List<Card> earth = shared.getCategory("earth");
            List<Card> fire = shared.getCategory("fire");
            List<Card> water = shared.getCategory("water");
            foreach (Card c in air)
            {
                if (!deck.getDeck().Contains(c))
                {
                    viewAir.Items.Add(c);
                }
            }
            foreach (Card c in earth)
            {
                if (!deck.getDeck().Contains(c))
                {
                    viewEarth.Items.Add(c);
                }
            }
            foreach (Card c in fire)
            {
                if (!deck.getDeck().Contains(c))
                {
                    viewFire.Items.Add(c);
                }
            }
            foreach (Card c in water)
            {
                if (!deck.getDeck().Contains(c))
                {
                    viewWater.Items.Add(c);
                }
            }
            deck.deckToJSON();
        }

        private void btnexit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == secretcode[countcode])
            {
                countcode++;
                if (countcode == secretcode.Length)
                {
                    this.KeyDown -= Window_KeyDown;
                    MessageBox.Show("Benvenuto VIP");
                    tabavatar.Visibility = Visibility.Visible;
                }
            }
            else
            {
                countcode = 0;
            }
        }

        private void btnremove_Click(object sender, RoutedEventArgs e)
        {
            Card torem = (Card)viewDeck.Items[viewDeck.SelectedIndex];
            deck.removeCard(torem);
            RefreshUI();
        }

        private void viewDeck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnremove.IsEnabled = true;
        }

        private void btnAddAir_Click(object sender, RoutedEventArgs e)
        {
            Card toadd = (Card)viewAir.Items[viewAir.SelectedIndex];
            btnAddAir.IsEnabled = false;
            deck.addCard(toadd);
            RefreshUI();
        }

        private void btnAddEarth_Click(object sender, RoutedEventArgs e)
        {
            Card toadd = (Card)viewEarth.Items[viewEarth.SelectedIndex];
            btnAddEarth.IsEnabled = false;
            deck.addCard(toadd);
            RefreshUI();
        }

        private void btnAddWater_Click(object sender, RoutedEventArgs e)
        {
            Card toadd = (Card)viewWater.Items[viewWater.SelectedIndex];
            btnAddWater.IsEnabled = false;
            deck.addCard(toadd);
            RefreshUI();
        }

        private void btnAddFire_Click(object sender, RoutedEventArgs e)
        {
            Card toadd = (Card)viewFire.Items[viewFire.SelectedIndex];
            btnAddFire.IsEnabled = false;
            deck.addCard(toadd);
            RefreshUI();
        }

        private void btnAddAvatar_Click(object sender, RoutedEventArgs e)
        {
            Card toadd = (Card)viewAvatar.Items[viewAvatar.SelectedIndex];
            btnAddAvatar.IsEnabled = false;
            deck.addCard(toadd);
            RefreshUI();
        }

        private void viewAir_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddAir.IsEnabled = true;
        }

        private void viewEarth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddEarth.IsEnabled = true;
        }

        private void viewWater_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddWater.IsEnabled = true;
        }

        private void viewFire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddFire.IsEnabled = true;
        }

        private void viewAvatar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddAvatar.IsEnabled = true;
        }
    }
}
