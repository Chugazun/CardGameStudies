﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Preparation : Card
    {
        public Preparation()
        {
            Name = "Preparation";
            Weight = 1;
            Desc = "■: Dice value +1";
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Preparation(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            return true;
        }

        public override void Action(int diceVal)
        {
            Game.ChangeDiceValue(Game.GetCurrentPlayer(), diceVal + 1);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name += "- (<=3)";
            Desc += " (Max 3)";
            IsWeakened = true;

            condCheck = diceVal => diceVal <= 3;
        }

        public override void Normalize()
        {
            Name = "Preparation";
            Desc = "■: Dice value +1";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
