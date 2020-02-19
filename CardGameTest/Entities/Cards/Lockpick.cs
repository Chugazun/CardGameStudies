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

        public override bool ConditionCheck(int diceVal)
        {
            return true;
        }

        public override void Action(int diceVal)
        {
            Game.SplitDiceValue(Game.GetCurrentPlayer(), diceVal);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name += "-";
            Desc = "■: Split Value in Two";
            IsWeakened = true;

            act = diceVal =>
            {
                if (diceVal != 1)
                {
                    double result = diceVal / 2;
                    Game.ChangeDiceValue(Game.GetCurrentPlayer(), (int)Math.Floor(result));                    
                }
                Game.CardsUsed++;
                Used = true;
            };
        }

        public override void Normalize()
        {
            Name = "Lockpick";
            Desc = "■: Split Dice in Two";
            IsWeakened = false;

            act = Action;
        }
    }
}
