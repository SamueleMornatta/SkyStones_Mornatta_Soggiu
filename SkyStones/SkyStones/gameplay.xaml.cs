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
        private Card[,] matr = new Card[5, 5];
        private Card[] mano = new Card[5];
        private Label l = new Label();
        private Image Imm = new Image();
        public Boolean ric { get; set; }
        public gameplay()
        {
            InitializeComponent();
            deck = Deck.Instance;
            shared = SharedResources.Instance;
            conn = shared.gameSocket;
            NetworkStream stream = conn.GetStream();
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            reader = new StreamReader(stream);
            t = new Thread(new ThreadStart(listener));
            ric = false;
            t.SetApartmentState(ApartmentState.STA);


            for (int i = 0; i < 30; i++)
            {
                int[] atttm = new int[4];
                for (int j = 0; j < 4; j++)
                {
                    atttm[j]= new Random().Next(4);
                    Thread.Sleep(10);
                }
                deck.addCard(new Card(i, atttm, new BitmapImage(), ""));
            }            


            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matr[i, j] = new Card(0, new int[4] { 0, 0, 0, 0 }, new BitmapImage(), "");
                    matr[i, j].setgameplay(this);
                    matr[i, j].vuota = true;
                    //matr[i, j].Can = tm.Can;
                    matr[i, j].Can.Background = Brushes.Transparent;
                }
            }

            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 5; j++)
            //    {
            //Card C = new Card();
            //C.setgameplay(this);
            //if (ric)
            //{
            //    C.vuota = false;
            //    //matr[0, 0] = C;
            //}
            //else
            //{
            //    C.vuota = false;
            //    //matr[4, 4] = C;
            //}
            //C.setPosseduta(true);
            //    }

            //}
            //double x = 25;
            //double y = 4;

            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 5; j++)
            //    {
            //if (ric)
            //{
            //    tavola.Children.Add(matr[0, 0].Can);
            //    Canvas.SetLeft(matr[0, 0].Can, x);
            //    x += 114.4;
            //    Canvas.SetTop(matr[0, 0].Can, y);
            //}
            //else
            //{
            //    matr[4, 4].vuota = false;
            //    matr[4, 4].setPosseduta(true);
            //    tavola.Children.Add(matr[4, 4].Can);
            //    x += 114.4 * 4 + 25;
            //    Canvas.SetLeft(matr[4, 4].Can, x);
            //    y = 100.4 * 4 + 4;
            //    Canvas.SetTop(matr[4, 4].Can, y);
            //}
            //    }
            //    y += 100.4;
            //    x = 25;
            //}            
            //shared.trec.stop();

            t.Start();
        }

        public void addCard(Card C, Point P)
        {
            matr[Convert.ToInt32(P.Y), Convert.ToInt32(P.X)] = C;
            disegnaTavolo();
        }

        public void send() 
        {
            writer.WriteLine(sender.Text);
            //writer.Flush();            
        }

        public void send(String n)
        {
            writer.WriteLine(n);
            //writer.Flush();            
        }


        public void listener()
        {
            string line;
            while (true)
            {
                while ((line = reader.ReadLine()) != null)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {                                                                                               
                        if (line.ElementAt(0) == '1')
                        {
                            int id = (Convert.ToInt32(line.ElementAt(3)) - 48) * 10 + Convert.ToInt32(line.ElementAt(4)) - 48;
                            matr[Convert.ToInt32(line.ElementAt(2)) - 48, Convert.ToInt32(line.ElementAt(1)) - 48] = getCardbyID(id);
                            matr[Convert.ToInt32(line.ElementAt(2)) - 48, Convert.ToInt32(line.ElementAt(1)) - 48].setPosseduta(false);
                            matr[Convert.ToInt32(line.ElementAt(2)) - 48, Convert.ToInt32(line.ElementAt(1)) - 48].setgameplay(this);
                            matr[Convert.ToInt32(line.ElementAt(2)) - 48, Convert.ToInt32(line.ElementAt(1)) - 48].vuota = false;
                            //tavola.Children.Add(matr[Convert.ToInt32(line.ElementAt(3)) - 48, Convert.ToInt32(line.ElementAt(2)) - 48].Can);
                            disegnaTavolo();
                        }
                    }));
                }
            }
        }

        private Card getCardbyID(int ID)
        {
            for (int i = 0; i < deck.getAmmountCards(); i++)
            {
                if (deck.getCardAt(i).ID==ID)
                {
                    return deck.getCardAt(i);
                }
            }
            return new Card();
        }

        private void btnsend_Click(object sender, RoutedEventArgs e)
        {
            send();
        }


        public int checkWin()
        {
            int chm = 0, cho = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!matr[i,j].vuota)
                    {
                        if (matr[i,j].isPosseduta())
                        {
                            chm++;
                        }
                        else if (!matr[i, j].isPosseduta())
                        {
                            cho++;
                        }
                    }
                }                                
            }
            if (chm + cho != 25)
            {
                return 0; //Partita ancora in corso
            }
            else if (chm > cho)
            {
                return 1; //Ho vinto io
            }
            else if (cho > chm)
            {
                return 2; //Ha vinto l'avversario
            }
            return -1;
        }


        public void disegnaTavolo()
        {
            bool CN = true, CS = true, CO = true, CE = true;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            CS = true;
                            CE = true;
                            CN = false;
                            CO = false;
                        }
                        else if (j == 1 || j == 2 || j == 3)
                        {
                            CS = true;
                            CE = true;
                            CN = false;
                            CO = true;
                        }
                        else if (j == 4)
                        {
                            CS = true;
                            CE = false;
                            CN = false;
                            CO = true;
                        }
                    }
                    else if (i == 1 || i == 2 || i == 3)
                    {
                        if (j == 0)
                        {
                            CS = true;
                            CE = true;
                            CN = true;
                            CO = false;
                        }
                        else if (j == 1 || j == 2 || j == 3)
                        {
                            CS = true;
                            CE = true;
                            CN = true;
                            CO = true;
                        }
                        else if (j == 4)
                        {
                            CS = true;
                            CE = false;
                            CN = true;
                            CO = true;
                        }
                    }
                    else if (i == 4)
                    {
                        if (j == 0)
                        {
                            CS = false;
                            CE = true;
                            CN = true;
                            CO = false;
                        }
                        else if (j == 1 || j == 2 || j == 3)
                        {
                            CS = false;
                            CE = true;
                            CN = true;
                            CO = true;
                        }
                        else if (j == 4)
                        {
                            CS = false;
                            CE = false;
                            CN = true;
                            CO = true;
                        }
                    }


                    //CONTROLLO

                    if (!matr[i, j].vuota)
                    {

                        if (CN)
                        {
                            if (!matr[i - 1, j].vuota && matr[i - 1, j].isPosseduta() != matr[i, j].isPosseduta())
                            {
                                if (matr[i - 1, j].isPosseduta())
                                {
                                    if (matr[i - 1, j].att[1] > matr[i, j].att[0])
                                        matr[i, j].setPosseduta(true);
                                    else if (matr[i - 1, j].att[1] < matr[i, j].att[0])
                                        matr[i - 1, j].setPosseduta(false);
                                }
                                else if (!matr[i - 1, j].isPosseduta())
                                {
                                    if (matr[i - 1, j].att[1] > matr[i, j].att[0])
                                        matr[i, j].setPosseduta(false);
                                    else if (matr[i - 1, j].att[1] < matr[i, j].att[0])
                                        matr[i - 1, j].setPosseduta(true);
                                }
                            }
                        }
                        if (CS)
                        {
                            if (!matr[i + 1, j].vuota && matr[i + 1, j].isPosseduta() != matr[i, j].isPosseduta())
                            {
                                if (matr[i + 1, j].isPosseduta())
                                {
                                    if (matr[i + 1, j].att[0] > matr[i, j].att[1])
                                        matr[i, j].setPosseduta(false);
                                    else if (matr[i + 1, j].att[0] < matr[i, j].att[1])
                                        matr[i + 1, j].setPosseduta(true);
                                }
                                else if (!matr[i + 1, j].isPosseduta())
                                {
                                    if (matr[i + 1, j].att[0] > matr[i, j].att[1])
                                        matr[i, j].setPosseduta(false);
                                    else if (matr[i + 1, j].att[0] < matr[i, j].att[1])
                                        matr[i + 1, j].setPosseduta(true);
                                }
                            }
                        }
                        if (CO)
                        {
                            if (!matr[i, j - 1].vuota && matr[i, j - 1].isPosseduta() != matr[i, j].isPosseduta())
                            {
                                if (matr[i, j - 1].isPosseduta())
                                {
                                    if (matr[i, j - 1].att[3] > matr[i, j].att[2])
                                        matr[i, j].setPosseduta(true);
                                    else if (matr[i, j - 1].att[3] < matr[i, j].att[2])
                                        matr[i, j - 1].setPosseduta(false);
                                }
                                else if (!matr[i, j - 1].isPosseduta())
                                {
                                    if (matr[i, j - 1].att[3] > matr[i, j].att[2])
                                        matr[i, j].setPosseduta(false);
                                    else if (matr[i, j - 1].att[3] < matr[i, j].att[2])
                                        matr[i, j - 1].setPosseduta(true);
                                }
                            }
                        }
                        if (CE)
                        {
                            if (!matr[i, j + 1].vuota && matr[i, j + 1].isPosseduta() != matr[i, j].isPosseduta())
                            {
                                if (matr[i, j + 1].isPosseduta())
                                {
                                    if (matr[i, j + 1].att[2] > matr[i, j].att[3])
                                        matr[i, j].setPosseduta(true);
                                    else if (matr[i, j + 1].att[2] < matr[i, j].att[3])
                                        matr[i, j + 1].setPosseduta(false);
                                }
                                else if (!matr[i, j + 1].isPosseduta())
                                {
                                    if (matr[i, j + 1].att[2] > matr[i, j].att[3])
                                        matr[i, j].setPosseduta(false);
                                    else if (matr[i, j + 1].att[2] < matr[i, j].att[3])
                                        matr[i, j + 1].setPosseduta(true);
                                }
                            }
                        }
                    }

                }
            }


            double left = 25, top = 4;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!matr[i, j].vuota)
                    {
                        try
                        {
                            tavola.Children.Add(matr[i, j].Can);
                            Canvas.SetLeft(matr[i, j].Can, left + 114.4 * j);
                            Canvas.SetTop(matr[i, j].Can, top + 100.4 * i);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }

            //tavola = new Canvas();
            //    }
            //}
        }

        private void resa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mazzobutt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Card tm = deck.drawRandomCard();
            tm.setPosseduta(true);
            tm.canMove = true;
            tm.setgameplay(this);
            tavola.Children.Add(tm.Can);
            Canvas.SetLeft(tm.Can, 25 + 114.4 * 0);
            Canvas.SetTop(tm.Can, 4 + 100.4 * 5);
        }
    }
}
