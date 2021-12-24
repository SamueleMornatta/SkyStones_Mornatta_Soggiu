﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStones
{
    class Deck
    {
        const int maxCards = 30;
        private List<Card> deck;
        private SharedResources shared;
        private static Deck _instance;
        private static readonly Object _sync = new Object();
        private Deck() {
            shared = SharedResources.Instance;
            deck = ReadDeck();
        }
        public static Deck Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_sync)
                    {
                        if (_instance == null)
                        {
                            _instance = new Deck();
                        }
                    }
                }
                return _instance;
            }
        }
        public List<Card> ReadDeck()
        {
            using (StreamReader r = new StreamReader("Deck.json"))
            {
                string json = r.ReadToEnd();
                List<cardID> items = JsonConvert.DeserializeObject<List<cardID>>(json);
                List<Card> cards = new List<Card>();
                for (int i = 0; i < items.Count; i++)
                {
                    for (int j = 0; j < shared.collection.Count; i++)
                    {
                        if (shared.collection.ElementAt(j).getID() == items.ElementAt(i).ID)
                        {
                            cards.Add(shared.collection.ElementAt(j));
                        }
                    }
                }
                return cards;
            }
        }
        public class cardID {
            public int ID;
        };
        public void deckToJSON()
        {
            using (StreamWriter file = File.CreateText(@"Deck.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, deck);
            }
        }
        public int getAmmountCards()
        {
            return deck.Count;
        }
        public bool addCard(Card card)
        {
            if (getAmmountCards() < maxCards)
            {
                deck.Add(card);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool removeCard(Card card)
        {
            if (getAmmountCards() > 0)
            {
                deck.Remove(card);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
