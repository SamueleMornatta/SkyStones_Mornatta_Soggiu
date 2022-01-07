using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStones
{
    class Upgrade
    {
        private int ID;
        private string desc;
        private int[] incatt;

        public Upgrade()
        {
            ID = 0;
            desc = "";
            incatt = new int[4] {0,0,0,0};            
        }

        public Upgrade(int iD, string desc, int[] incatt)
        {
            ID = iD;
            this.desc = desc;
            this.incatt = incatt;
        }
    }
}
