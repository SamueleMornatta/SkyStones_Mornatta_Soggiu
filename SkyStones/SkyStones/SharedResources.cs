using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStones
{
    class SharedResources
    {
        private static SharedResources _instance;
        private static readonly Object _sync = new Object();
        private SharedResources() { }
        public static SharedResources Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_sync)
                    {
                        if (_instance == null)
                        {
                            _instance = new SharedResources();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
