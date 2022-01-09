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


            //Application.Current.Dispatcher.Invoke(new Action(() => { MessageBox.Show(Directory.GetCurrentDirectory()+@"\..\.."); }));


            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Card C = new Card();
                    C.setgameplay(this);
                    matr[i, j] = C.Can;                    
                    
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


        

    }
}
