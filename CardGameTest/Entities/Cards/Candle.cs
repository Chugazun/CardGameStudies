using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Candle : Card
    {
        public Candle()
        {
            Name = "Candle (>=2)(R)";
            Desc = "Die value -1, Lose 1 Hp (Min 2) (Reusable)";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Candle(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            return diceVal >= 2;
        }

        public override void Action(int diceVal)
        {
            Game.ChangeDiceValue(Game.GetCurrentPlayer(), diceVal - 1);
            Game.TrueDamage(Game.GetCurrentPlayer(), 1);
            Game.CardsUsed++;
        }

        public override void Weaken()
        {
            Name = "Candle- (>=4)(R)";
            Desc = "Die value -1, Lose 1 Hp (Min 4) (Reusable)";
            IsWeakened = true;

            condCheck = diceVal => diceVal >= 4;
        }

        public override void Normalize()
        {
            Name = "Candle (>=2)(R)";
            Desc = "Die value -1, Lose 1 Hp (Min 2) (Reusable)";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
