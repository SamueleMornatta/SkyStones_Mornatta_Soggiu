using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStones
{
    class LocalPlayer
    {
        string nickname { set; get; }
        string ip { set; get; }
        public LocalPlayer(string nickname, string ip)
        {
            this.nickname = nickname;
            this.ip = ip;
        }
    }
}
