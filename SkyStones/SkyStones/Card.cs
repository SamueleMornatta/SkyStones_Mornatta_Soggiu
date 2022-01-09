using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SkyStones
{
    class Card
    {
        public int ID { get; set; }
        public int[] att { get; set; }
        public BitmapImage I { get; set; }
        public String tipo { get; set; }
        public String nome { get; set; }
        public Canvas Can { get; set; }
        private bool _isRectDragInProg, canMove;
        private gameplay G;
        private Label l { get; set; }
        private Image Imm;

        private Point P;


        public Card()
        {
            ID = 0;
            att = new int[4];
            for (int i = 0; i < att.Length; i++)
            {
                att[i] = new Random().Next(0,5);
                Thread.Sleep(10);
            }
            I = new BitmapImage();
            tipo = "";
            Can = new Canvas();
            createCanvas();
        }

        public Card(int iD, int[] att, BitmapImage i, string tipo)
        {
            ID = iD;
            this.att = att;
            I = i;
            this.tipo = tipo;
            Can = new Canvas();
            createCanvas();
        }
        public void setgameplay(gameplay G)
        {
            this.G = G;
        }

        public void createCanvas()
        {
            P = new Point(0,0);
            l = new Label();
            Imm = new Image();
            SolidColorBrush br = new SolidColorBrush();
            br.Color = Colors.Gray;
            Can = new Canvas();
            Can.Height = 90;
            Can.Width = 70;
            Can.Background = br;


            Can.MouseLeftButtonDown += (sender, e) => rect_MouseLeftButtonDown(sender, e);
            Can.MouseLeftButtonUp += (sender, e) => rect_MouseLeftButtonUp(sender, e);
            Can.MouseMove += (sender, e) => rect_MouseMove(sender, e);

            Can.Children.Add(l);

            Imm.Height = 20;
            Imm.Width = 50;


            if (att[0] != 0)
            {
                Imm.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"..\..\..\..\..\images\attN" + att[0] + ".png"));
                var immagineN = Imm;
                Canvas.SetTop(immagineN, -5);
                Canvas.SetLeft(immagineN, 10);
                Can.Children.Add(immagineN);
            }
            Imm = new Image();

            Imm.Height = 20;
            Imm.Width = 50;
            if (att[1] != 0)
            {
                Imm.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"..\..\..\..\..\images\attS" + att[1] + ".png"));
                var immagineS = Imm;
                Canvas.SetBottom(immagineS, -5);
                Canvas.SetLeft(immagineS, 10);
                Can.Children.Add(immagineS);
            }
            Imm = new Image();


            Imm.Height = 50;
            Imm.Width = 20;

            if (att[2] != 0)
            {
                Imm.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"..\..\..\..\..\images\attO" + att[2] + ".png"));
                var immagineO = Imm;
                Canvas.SetTop(immagineO, 20);
                Canvas.SetLeft(immagineO, -5);
                Can.Children.Add(immagineO);
            }
            Imm = new Image();

            Imm.Height = 50;
            Imm.Width = 20;
            if (att[3] != 0)
            {
                Imm.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"..\..\..\..\..\images\attE" + att[3] + ".png"));
                var immagineE = Imm;
                Canvas.SetTop(immagineE, 20);
                Canvas.SetRight(immagineE, -5);
                Can.Children.Add(immagineE);
            }
            Imm = new Image();
        }



        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isRectDragInProg = true;
            Can.CaptureMouse();
        }

        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isRectDragInProg = false;
            
            var mousePos = e.GetPosition(G.tavola);

            // center the rect on the mouse
            double left = mousePos.X - (Can.ActualWidth / 2);
            double top = mousePos.Y - (Can.ActualHeight / 2);

            if (left >= 0 && left <= 85)
            {
                P.X = 0;
                left = 25;
            }
            else if (left >= 86 && left <= 195)
            {
                P.X = 1;
                left = 25 + 114.4;
            }
            else if (left >= 196 &&left <= 310)
            {
                P.X = 2;
                left = 25 + 114.4 * 2;
            }
            else if (left >= 311 &&left <= 424)
            {
                P.X =3;
                left = 25 + 114.4 * 3;
            }
            else if (left >= 425&& left <= 566)
            {
                P.X = 4;
                left = 25 + 114.4 * 4;
            }


            if (top >= 0 && top <= 50)
            {
                P.Y = 0;
                top = 4;
            }
            else if (top >= 51 && top <= 150)
            {
                P.Y = 1;
                top = 4 + 100.4;
            }
            else if (top >= 151 && top <= 250)
            {
                P.Y = 2;
                top = 4 + 100.4 * 2;
            }
            else if (top >= 251 && top <= 350)
            {
                P.Y = 3;
                top = 4 + 100.4 * 3;
            }
            else if (top >= 351 && top <= 450)
            {
                P.Y = 4;
                top = 4 + 100.4 * 4;
            }


            G.coordinate.Content = "x: " + left + " y: " + top+ " PUNTO: " + P.X+", "+P.Y;
            Canvas.SetLeft(Can, left);
            Canvas.SetTop(Can, top);

            Can.ReleaseMouseCapture();
        }

        private void rect_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isRectDragInProg) return;

            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(G.tavola);

            // center the rect on the mouse
            double left = mousePos.X - (Can.ActualWidth / 2);
            double top = mousePos.Y - (Can.ActualHeight / 2);
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


            G.coordinate.Content = "x: " + left + " y: " + top;
            Canvas.SetLeft(Can, left);
            Canvas.SetTop(Can, top);
        }


        public int getID()
        {
            return ID;
        }

        public BitmapImage getI()
        {
            return I;
        }
        public int[] getAtt()
        {
            return att;
        }
        public string getTipo()
        {
            return tipo;
        }

        public void setID(int ID)
        {
            this.ID = ID;
        }

        public void setI(BitmapImage I)
        {
            this.I = I;
        }

        public void setAtt(int[] att)
        {
            this.att = att;
        }

        public void setTipo(string tipo)
        {
            this.tipo = tipo;
        }


    }
}
