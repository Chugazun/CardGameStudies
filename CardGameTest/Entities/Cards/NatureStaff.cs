using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class NatureStaff : Card
    {
        public NatureStaff()
        {
            Name = "Nature Staff";
            Weight = 2;
            Desc = "Deals ■ Damage, on 6, heals for 2";
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public NatureStaff(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), diceVal);
            if (diceVal == 6) Game.Heal(Game.GetCurrentPlayer(), 2);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name += "-";
            Desc = "Deals ■ Damage, on 3, heals for 1 (Max 3)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal <= 3) return base.ConditionCheck(diceVal);
                return false;
            };

            act = diceVal =>
            {
                Game.Damage(Game.GetCurrentMonster(), diceVal);
                if (diceVal == 3) Game.Heal(Game.GetCurrentPlayer(), 1);
                Game.CardsUsed++;
                Used = true;
            };
        }

        public override void Normalize()
        {
            Name = "Nature Staff";
            Desc = "Deals ■ Damage, on 6, heals for 2";
            IsWeakened = false;

            condCheck = ConditionCheck;
            act = Action;
        }
    }
}
