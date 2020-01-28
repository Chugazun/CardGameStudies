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
        }

        public Sword(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), diceVal);
            Used = true;
        }
    }
}
