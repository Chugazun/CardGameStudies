using CardGameTest.Entities;
using CardGameTest.Entities.Cards;
using System;
using System.Collections.Generic;
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
            TempAddCardFromDb("WoodenShield");
            TempAddCardFromDb("Broadsword");
        }

        public void TempAddCards() => handCards.AddRange(new Card[] { new Sword(SetID()), new Potion(SetID()), new Sword(SetID()) });
        public void TempAddCardFromDb(string name)
        {
            string cardName = Game.GetCardNameFromDb(name);
            Type cardType = Type.GetType("CardGameTest.Entities.Cards." + cardName);
            Card card = (Card)Activator.CreateInstance(cardType);
            AddCard(card);
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

        public List<Card> GetCards()
        {
            return handCards;
        }

        public void ResetHandCards()
        {
            handCards.ForEach(c => { c.ResetCard(); });
        }

        public string PlayerHand()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Card card in handCards)
            {
                if (!card.Used)
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
