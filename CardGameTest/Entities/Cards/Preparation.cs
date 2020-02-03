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
        }

        public Preparation(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            //Game.ValidAction();
            Game.ChangeDiceValue(Game.GetCurrentPlayer(), diceVal + 1);
            Used = true;
        }
    }
}