using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class SpikedShield : Card
    {
        public SpikedShield()
        {
            Name = "Spiked Shield";
            Weight = 2;
            Desc = "Even: Deal ■ Damage \nOdd: Gives ■ Shield \n(Max 5)";
            DiceNeeded = 1;
            act = Action;
        }

        public SpikedShield(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            if (diceVal <= 5)
            {
                Game.ValidAction();
                if (diceVal % 2 == 0)
                {
                    Game.Damage(Game.GetCurrentMonster(), diceVal);                    
                } else
                {
                    Game.GainShield(Game.GetCurrentPlayer(), diceVal);
                }
                Game.CardsUsed++;
                Used = true;
            }
        }
    }
}
