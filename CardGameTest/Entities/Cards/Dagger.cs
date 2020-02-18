using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Dagger : Card
    {
        public Dagger()
        {
            Name = "Dagger (R)(<=3)";
            Weight = 1;
            Desc = "Deal ■ Damage (Reusable) (Max 3)";
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Dagger(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (diceVal <= 3) return base.ConditionCheck(diceVal);
            return false;
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), 3);
            Game.CardsUsed++;
        }

        public override void Weaken()
        {
            Name = "Dagger- (R)(=1)"; ;
            Desc = "Deal ■ Damage (Reusable) (NEEDS 1)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal == 1) return base.ConditionCheck(diceVal);
                return false;
            };            
        }

        public override void Normalize()
        {
            Name = "Dagger (R)(<=3)"; ;
            Desc = "Deal ■ Damage (Reusable) (Max 3)";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
