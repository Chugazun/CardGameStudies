using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class BearHug : Card
    {
        public BearHug()
        {
            Name = "Bear Hug";
            Desc = "Deal 2x ■ damage, but lose a die";
            DiceNeeded = 1;
            Weight = 2;
            act = Action;
            condCheck = ConditionCheck;
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), diceVal * 2);
            Game.GetCurrentPlayer().Dice.RemoveAt(new Random().Next(Game.GetCurrentPlayer().Dice.Count));
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
            Name = "Bear Hug";
            Desc = "Deal 2x ■ damage, but lose a die";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
