using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SkyStones
{
    class SharedResources
    {
        private static SharedResources _instance;
        public List<Card> collection;
        public List<Upgrade> upgrades;
        public List<Invite> invites;
        public List<LocalPlayer> playersFound;
        public TCPReceive trec { set; get; }
        public TcpClient gameSocket { set; get;}
        public string nickname;
        private static readonly Object _sync = new Object();
        private SharedResources() {
            collection = ReadAllCards();
            upgrades = ReadAllUpgrades();
            invites = new List<Invite>();
            playersFound = new List<LocalPlayer>();
            Random rand = new Random();
            nickname = "guest" + rand.Next(10000, 99999).ToString();
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
                    BitmapImage img = new BitmapImage(new Uri(items.ElementAt(i).Ipath, UriKind.Relative));
                    Card tmp = new Card(items.ElementAt(i).ID, items.ElementAt(i).att, img,items.ElementAt(i).tipo);
                    tmp.nome = items.ElementAt(i).nome;
                    cards.Add(tmp);
                }
                return cards;
            }
        }
        public class tempCard
        {
            public int ID;
            public int[] att;
            public string nome;
            public string Ipath;
            public string tipo;
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
        public void addInvite(Invite inv)
        {
            invites.Add(inv);
        }
        public void removeInvite(Invite inv)
        {
            invites.Remove(inv);
        }
        public List<Card> getCategory(string cat)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection.ElementAt(i).tipo == cat)
                {
                    cards.Add(collection.ElementAt(i));
                }
            }
            return cards;
        }
    }
}
