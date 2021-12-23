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

        public Upgrade()
        {
            ID = 0;
            desc = "";
        }

        public Upgrade(int iD, string desc)
        {
            ID = iD;
            this.desc = desc;
        }
    }
}
