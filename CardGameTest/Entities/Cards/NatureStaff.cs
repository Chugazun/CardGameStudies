using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class NatureStaff : Card
    {
        public NatureStaff()
        {
            Name = "Nature Staff";
            Weight = 2;
            Desc = "Deals ■ Damage, on 6, heals for 2";
            DiceNeeded = 1;
            act = Action;
        }

        public NatureStaff(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            Game.ValidAction();
            Game.Damage(Game.GetCurrentMonster(), diceVal);
            if (diceVal == 6) Game.Heal(Game.GetCurrentPlayer(), 2);
            Used = true;
        }
    }
}
