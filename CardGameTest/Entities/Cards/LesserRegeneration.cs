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
    }
}
