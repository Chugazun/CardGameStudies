using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Axe : Card
    {
        public Axe()
        {
            Name = "Axe";
            Weight = 2;
            Desc = "Deals 2x ■ Damage (Max 4)";
            DiceNeeded = 1;
            act = Action;
        }

        public Axe(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            if (diceVal <= 4)
            {
                Game.ValidAction();
                Game.Damage(Game.GetCurrentMonster(), diceVal * 2);
                Used = true;
            }            
        }
    }
}
