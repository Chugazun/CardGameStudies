using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Shocked : Card
    {
        public Shocked()
        {
            Name = "(Shocked)";
            Desc = "■: Remove Shock";
            DiceNeeded = 1;
            act = Action;
        }

        public Shocked(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            Game.ValidAction();
            Game.RemoveShock(Game.GetCurrentPlayer(), ID);
            Used = true;
        }
    }
}
