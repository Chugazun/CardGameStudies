using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Potion : Card
    {
        public Potion()
        {
            Name = "Potion (<=4)";
            Weight = 1;
            Desc = "Heals for ■ (Max 4)";
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Potion(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (diceVal <= 4) return base.ConditionCheck(diceVal);
            return false;
        }

        public override void Action(int diceVal)
        {            
            Game.Heal(Game.GetCurrentPlayer(), diceVal);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Potion- (<=2)";
            Desc = "Heals for ■ (Max 2)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal <= 2) return base.ConditionCheck(diceVal);
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Potion (<=4)";
            Desc = "Heals for ■ (Max 4)";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
