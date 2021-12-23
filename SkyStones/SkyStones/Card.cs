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
        private int ID;
        private int[] att;
        private BitmapImage I;
        private String tipo;


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

        int getID()
        {
            return ID;
        }

        BitmapImage getI()
        {
            return I;
        }
        int[] getAtt()
        {
            return att;
        }
        string getTipo()
        {
            return tipo;
        }
        
        void setID(int ID)
        {
            this.ID = ID;
        }

        void setI(BitmapImage I)
        {
            this.I = I;
        }

        void setAtt(int[] att)
        {
            this.att = att;
        }

        void setTipo(string tipo)
        {
            this.tipo = tipo;
        }


    }
}
