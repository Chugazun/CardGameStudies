using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Boomerang : Card
    {
        public Boomerang()
        {
            Name = "Boomerang";
            Desc = "Deal 2x ■ damage, but also ■ damage to yourself";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Boomerang(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), diceVal * 2);
            Game.Damage(Game.GetCurrentPlayer(), diceVal);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name += "- (<=4)";
            Desc += " (Max 4)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal <= 4) return base.ConditionCheck(diceVal);
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Boomerang";
            Desc = "Deal 2x ■ damage, but also ■ damage to yourself";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
