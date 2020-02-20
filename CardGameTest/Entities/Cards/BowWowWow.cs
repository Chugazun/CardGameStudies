using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class BowWowWow : Card
    {
        public BowWowWow()
        {
            Name = "Bow Wow Wow (E)";
            Desc = "Make unavailable cards available again, return die (Even only)";
            Weight = 2;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public BowWowWow(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            return diceVal % 2 == 0;
        }

        public override void Action(int diceVal)
        {
            List<Card> usedCards = Game.GetCurrentPlayer().GetCards().Where(c => c.Used).ToList();
            usedCards.ForEach(c => c.ResetCard());
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Bow Wow Wow (>=5)";
            Desc = "Make unavailable cards available again, return die (Min 5)";
            IsWeakened = true;

            condCheck = diceVal => diceVal >= 5;
        }

        public override void Normalize()
        {
            Name = "Bow Wow Wow (E)";
            Desc = "Make unavailable cards available again, return die (Even only)";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
