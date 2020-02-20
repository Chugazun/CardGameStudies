using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class BerlinKey : Card
    {
        public BerlinKey()
        {
            Name = "Berlin Key";
            Desc = "Set a random die to ■, return the die";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public BerlinKey(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            Die die = Game.GetCurrentPlayer().Dice[Game.GetCurrentDicePos()];
            Game.ChangeDiceValue(Game.GetCurrentPlayer(), new Random().Next(Game.GetCurrentPlayer().Dice.Count), diceVal);
            Game.CreateDie(Game.GetCurrentPlayer(), die);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name += "- (<=3)";
            Desc += " (Max 3)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal <= 3) return base.ConditionCheck(diceVal);
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Berlin Key";
            Desc = "Set a random die to ■, return the die";
            IsWeakened = false;

            condCheck = ConditionCheck; 
        }
    }
}
