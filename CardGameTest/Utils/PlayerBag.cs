using CardGameTest.Entities;
using CardGameTest.Entities.Cards;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CardGameTest.Utils
{
    class PlayerBag
    {
        private byte _id = 1;
        private List<Card> handCards = new List<Card>();
        public List<Card> inventoryCards = new List<Card>();

        public PlayerBag()
        {
            TempAddCardFromDb("Sword");
        }

        public void TempAddCards() => handCards.AddRange(new Card[] { new Sword(SetID()), new Potion(SetID()), new Sword(SetID()) });
        public void TempAddCardFromDb(string name)
        {
            //Card card = Game.GetCardFromDb(name);
            Type t = Type.GetType("CardGameTest.Entities.Cards.Sword");
            Card card2 = (Card)Activator.CreateInstance(t);
            //MethodInfo methodInfo = t.GetMethod("Action", new Type[] { typeof(int) }, null);           

            //card.ID = SetID();
            //AddCard(card);
            AddCard(card2);
        }

        private byte SetID()
        {
            return _id++;
        }

        public void AddCard(Card card)
        {
            card.ID = SetID();
            handCards.Add(card);
        }

        public Card GetCardAt(int handPos)
        {
            return handCards[handPos];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Card card in handCards)
            {
                if (!card.checkUsed())
                {
                    sb.Append(handCards.FindIndex(x => x.ID == card.ID) + 1);
                    sb.Append(": ");
                    sb.Append(card.Name);
                    sb.Append(", ");
                }
            }
            return sb.ToString().Substring(0, sb.ToString().Length - 2);
        }
    }
}
