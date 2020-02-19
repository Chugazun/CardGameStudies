using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class LesserRegeneration : Card
    {
        public LesserRegeneration()
        {
            Name = "Lesser Regeneration (O)(R)";
            Weight = 1;
            Desc = "■: Heals for 1 (Odd Only) (Reusable)";
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public LesserRegeneration(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (diceVal % 2 != 0)
            {
                return base.ConditionCheck(diceVal);
            }
            return false;
        }

        public override void Action(int diceVal)
        {
            Game.Heal(Game.GetCurrentPlayer(), 1);
            Game.CardsUsed++;
        }

        public override void Weaken()
        {
            Name = "Lesser Regeneration- (O)";
            Desc = "■: Heals for 1 (Odd Only)";
            IsWeakened = true;

            act += diceVal => Used = true;
        }

        public override void Normalize()
        {
            Name = "Lesser Regeneration (O)(R)";
            Desc = "■: Heals for 1 (Odd Only) (Reusable)";
            IsWeakened = false;

            act = Action;
        }
    }
}
