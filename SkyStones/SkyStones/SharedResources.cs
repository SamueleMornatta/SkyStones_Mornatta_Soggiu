using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SkyStones
{
    class SharedResources
    {
        private static SharedResources _instance;
        private List<Card> collection;
        private List<Upgrade> upgrades;
        private static readonly Object _sync = new Object();
        private SharedResources() {
            collection = ReadAllCards();
            upgrades = ReadAllUpgrades();
        }
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
        public List<Card> ReadAllCards()
        {
            using (StreamReader r = new StreamReader("Cards.json"))
            {
                string json = r.ReadToEnd();
                List<tempCard> items = JsonConvert.DeserializeObject<List<tempCard>>(json);
                List<Card> cards = new List<Card>();
                for (int i = 0; i < items.Count; i++)
                {
                    Card tmp = new Card();
                    tmp.setID(items.ElementAt(i).ID);
                    tmp.setAtt(items.ElementAt(i).att);
                    tmp.setTipo(items.ElementAt(i).tipo);
                    BitmapImage img = new BitmapImage(new Uri(items.ElementAt(i).Ipath));
                    tmp.setI(img);
                    cards.Add(tmp);
                }
                return cards;
            }
        }
        public class tempCard
        {
            public int ID;
            public int[] att;
            public String Ipath;
            public String tipo;
        }
        public List<Upgrade> ReadAllUpgrades()
        {
            using (StreamReader r = new StreamReader("Upgrades.json"))
            {
                string json = r.ReadToEnd();
                List<Upgrade> items = JsonConvert.DeserializeObject<List<Upgrade>>(json);
                return items;
            }
        }
    }
}
