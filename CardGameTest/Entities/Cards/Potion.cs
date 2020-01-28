using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Potion : Card
    {
        public Potion()
        {
            Name = "Potion";
            Weight = 1;
            Desc = "Heals ■ health (Max: 3)";
            DiceNeeded = 1;
        }

        public Potion(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            if (diceVal <= 3)
            {
                Game.Heal(Game.GetCurrentPlayer(), diceVal);
                Used = true;
            }            
        }
    }
}
