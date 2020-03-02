using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Entities.Cards
{
    class CactusShield : Card
    {
        public CactusShield()
        {
            Name = "Cactus Shield (O)(R)";
            Desc = "Add 1 Thorns (Odd Only) (Reusable)";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public CactusShield(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (diceVal % 2 != 0) return base.ConditionCheck(diceVal);
            return false;
        }

        public override void Action(int diceVal)
        {
            Game.GetCurrentPlayer().Status.Thorns++;
            Game.CardsUsed++;
        }

        public override void Weaken()
        {
            Name = "Cactus Shield- (=1)(R)";
            Desc = Regex.Replace(Desc, "Odd Only", "NEEDS 1");
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal == 1) return base.ConditionCheck(diceVal);
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Cactus Shield (O)(R)";
            Desc = Regex.Replace(Desc, "NEEDS 1", "Odd Only");
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
