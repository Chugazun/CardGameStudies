using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Axe : Card
    {
        public Axe()
        {
            Name = "Axe (<=4)";
            Weight = 2;
            Desc = "Deals 2x ■ Damage (Max 4)";
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Axe(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (diceVal <= 4)
            {
                base.ConditionCheck(diceVal);
            }
            return false;
        }

        public override void Action(int diceVal)
        {            
            Game.Damage(Game.GetCurrentMonster(), diceVal * 2);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            condCheck = diceVal =>
            {
                if (diceVal <= 3) return base.ConditionCheck(diceVal);
                return false;
            };

            Name += "- (<=3)";
            Desc = "Deals 2x ■ damage (Max 3)";
            IsWeakened = true;
        }

        public override void Normalize()
        {
            condCheck = base.ConditionCheck;
            Name = "Axe (<=4)";            
            Desc = "Deals 2x ■ Damage (Max 4)";
            IsWeakened = false;
        }
    }
}
