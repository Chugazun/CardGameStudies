using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Sword : Card
    {
        public Sword()
        {
            Name = "Sword";
            Weight = 2;
            Desc = "Deals ■ damage";
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Sword(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), diceVal);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            condCheck = diceVal =>
            {
                if (diceVal <= 5) return base.ConditionCheck(diceVal);
                return false;
            };

            Name += "- (<=5)";
            Desc += "(Max 5)";
            IsWeakened = true;
        }

        public override void Normalize()
        {
            condCheck = base.ConditionCheck;
            Name = "Sword";
            Desc = "Deals ■ damage";
            IsWeakened = false;
        }
    }
}
