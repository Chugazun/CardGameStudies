using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class WoodenShield : Card
    {
        public WoodenShield()
        {
            Name = "Wooden Shield";
            Weight = 2;
            Desc = "Gives you ■ Shield (Max 3)";
            DiceNeeded = 1;
            act = Action;
        }

        public WoodenShield(byte id) : this()
        {
            ID = id;
        }

        public void Action(int diceVal)
        {
            if (diceVal <= 3)
            {
                Game.GainShield(Game.GetCurrentPlayer(), diceVal);                
                Used = true;
            }
        }
    }
}

