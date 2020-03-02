using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Entities.Cards
{
    class CactusSpear : Card
    {
        public CactusSpear()
        {
            Name = "Cactus Spear (E)";
            Desc = "Deal ■ damage, +1 damage for each Thorns (Even Only)";
            Weight = 2;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public CactusSpear(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (diceVal % 2 == 0) return base.ConditionCheck(diceVal);
            return false;
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), diceVal + Game.GetCurrentPlayer().Status.Thorns);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Cactus Spear- (<=2)";
            Desc = Regex.Replace(Desc, "Even Only", "Max 2");
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal <= 2) return base.ConditionCheck(diceVal);
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Cactus Spear (E)";
            Desc = Regex.Replace(Desc, "Max 2", "Even Only");
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
