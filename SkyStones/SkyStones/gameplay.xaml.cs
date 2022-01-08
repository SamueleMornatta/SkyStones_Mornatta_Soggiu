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
        private bool _isRectDragInProg;
        private Canvas[,] matr = new Canvas[5, 5];
        private Label l = new Label();
        private Image Imm= new Image();
        public gameplay()
        {
            InitializeComponent();
            deck = Deck.Instance;
            shared = SharedResources.Instance;
            //conn = shared.gameSocket;
            //NetworkStream stream = conn.GetStream();
            //writer = new StreamWriter(stream);
            //writer.AutoFlush = true;
            //reader = new StreamReader(stream);
            t = new Thread(new ThreadStart(listener));
            //t.Start();
            int c = 0;
            
            
            SolidColorBrush br = new SolidColorBrush();
            br.Color = Colors.Gray;
            
            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matr[i, j] = new Canvas();
                    matr[i, j].Height = 90;
                    matr[i, j].Width = 70;
                    matr[i, j].Background = br;

                    matr[i, j].MouseLeftButtonDown += rect_MouseLeftButtonDown;
                    matr[i, j].MouseLeftButtonUp += rect_MouseLeftButtonUp;
                    matr[i,j].MouseMove += rect_MouseMove;

                    //matr[i, j].Children.Add(l);

                    Imm.Height = 20;
                    Imm.Width = 50;

                    Imm.Source = new BitmapImage(new Uri(@"D:\Utenti\MAALFING\Documents\GitHub\SkyStones_Mornatta_Soggiu\images\attN4.png"));
                    var immagineN = Imm;
                    Canvas.SetTop(immagineN, 0);
                    Canvas.SetLeft(immagineN, 10);
                    matr[i, j].Children.Add(immagineN);
                    Imm = new Image();

                    Imm.Height = 20;
                    Imm.Width = 50;

                    Imm.Source = new BitmapImage(new Uri(@"D:\Utenti\MAALFING\Documents\GitHub\SkyStones_Mornatta_Soggiu\images\attS4.png"));
                    var immagineS = Imm;
                    Canvas.SetBottom(immagineS, 0);
                    Canvas.SetLeft(immagineS, 10);
                    matr[i, j].Children.Add(immagineS);
                    Imm = new Image();


                    Imm.Height = 50;
                    Imm.Width = 20;

                    Imm.Source = new BitmapImage(new Uri(@"D:\Utenti\MAALFING\Documents\GitHub\SkyStones_Mornatta_Soggiu\images\attO4.png"));
                    var immagineO = Imm;
                    Canvas.SetTop(immagineO, 20);
                    Canvas.SetLeft(immagineO, 0);
                    matr[i, j].Children.Add(immagineO);
                    Imm = new Image();

                    Imm.Height = 50;
                    Imm.Width = 20;

                    Imm.Source = new BitmapImage(new Uri(@"D:\Utenti\MAALFING\Documents\GitHub\SkyStones_Mornatta_Soggiu\images\attE4.png"));
                    var immagineE = Imm;
                    Canvas.SetTop(immagineE, 20);
                    Canvas.SetRight(immagineE, 0);
                    matr[i, j].Children.Add(immagineE);
                    Imm = new Image();

                    l.Content = c;
                    l = new Label();
                    c++;
                }
                
            }
            double x = 25;
            double y = 4;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    tavola.Children.Add(matr[i, j]);
                    Canvas.SetLeft(matr[i, j], x);
                    x += 114.4;
                    Canvas.SetTop(matr[i, j], y);
                }
                y += 100.4;
                x = 25;
            }

            //matr[0, 0] = new Rectangle();
            //matr[0, 0].Height = 100;
            //matr[0, 0].Width = 50;
            //matr[0, 0].Fill = br;

            //matr[0, 0].MouseLeftButtonDown += rect_MouseLeftButtonDown;
            //matr[0, 0].MouseLeftButtonUp += rect_MouseLeftButtonUp;
            //matr[0, 0].MouseMove += rect_MouseMove;

            //matr[0, 0].Margin = new Thickness(10, 10, 0, 0);



            //tavola.Children.Add(matr[0, 0]);

            //shared.trec.stop();
        }

        public void send()
        {
            writer.WriteLine(sender.Text);
            //writer.Flush();            
        }


        public void listener()
        {
            string line;
            while (true)
            {
                while ((line = reader.ReadLine()) != null)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => { MessageBox.Show(line); }));
                }
            }
        }

        private void btnsend_Click(object sender, RoutedEventArgs e)
        {
            send();
        }


        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isRectDragInProg = true;
            matr[0, 0].CaptureMouse();
        }

        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isRectDragInProg = false;
            matr[0, 0].ReleaseMouseCapture();
        }

        private void rect_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isRectDragInProg) return;

            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(tavola);

            // center the rect on the mouse
            double left = mousePos.X - (matr[0, 0].ActualWidth / 2);
            double top = mousePos.Y - (matr[0, 0].ActualHeight / 2);
            double bottom = mousePos.Y - (matr[0, 0].ActualHeight / 2);
            double right = mousePos.Y - (matr[0, 0].ActualHeight / 2);
            if (left >= 502)
            {
                left = 502;
            }
            else if (top >= 408)
            {
                top = 408;
            }
            else if (top <= 0)
            {
                top = 0;
            }
            else if (left <= 0)
            {
                left = 0;
            }
            coordinate.Content = "x: " + top + " y: "+left;
            Canvas.SetLeft(matr[0, 0], left);
            Canvas.SetTop(matr[0, 0], top);
        }

    }
}
