using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStones
{
    class LocalPlayer
    {
        string nickname,ip;
        public LocalPlayer(string nickname, string ip)
        {
            this.nickname = nickname;
            this.ip = ip;
        }

        public override string ToString()
        {
            return nickname + " " + ip;
        }
    }
}
