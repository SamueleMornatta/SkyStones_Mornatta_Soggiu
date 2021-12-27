using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SkyStones
{
    class Card
    {
        public int ID { get; set; }
        public int[] att { get; set; }
        public BitmapImage I { get; set; }
        public String tipo { get; set; }


        public Card()
        {
            ID = 0;
            att = new int[4];
            for (int i = 0; i < att.Length; i++)
            {
                att[i] = 0;
            }
            I = new BitmapImage();
            tipo = "";
        }

        public Card(int iD, int[] att, BitmapImage i, string tipo)
        {
            ID = iD;
            this.att = att;
            I = i;
            this.tipo = tipo;
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
