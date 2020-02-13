using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Potion : Card
    {
        public Potion()
        {
            Name = "Potion (E)";
            Weight = 1;
            Desc = "Heals for 2 (Even Only)";
            DiceNeeded = 1;
            act = Action;
        }

        public Potion(byte id) : this()
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
            Game.Heal(Game.GetCurrentPlayer(), 2);
            Game.CardsUsed++;
            Used = true;
        }
    }
}
