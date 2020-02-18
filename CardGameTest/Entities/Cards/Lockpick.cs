using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Lockpick : Card
    {
        public Lockpick()
        {
            Name = "Lockpick";
            Weight = 1;
            Desc = "■: Split Die in Two";
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Lockpick(byte id) : this()
        {
            ID = id;
        }        

        public override void Action(int diceVal)
        {            
            Game.SplitDiceValue(Game.GetCurrentPlayer(), diceVal);
            Game.CardsUsed++;
            Used = true;           
        }
    }
}
