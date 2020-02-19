using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class WoodenShield : Card
    {
        public WoodenShield()
        {
            Name = "Wooden Shield (<=4)";
            Weight = 2;
            Desc = "Gives you ■ Shield (Max 4)";
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public WoodenShield(byte id) : this()
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
            Game.GainShield(Game.GetCurrentPlayer(), diceVal);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Wooden Shield- (<=2)";
            Desc = "Gives you ■ Shield (Max 2)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal <= 2) return base.ConditionCheck(diceVal);
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Wooden Shield (<=4)";
            Desc = "Gives you ■ Shield (Max 4)";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}

