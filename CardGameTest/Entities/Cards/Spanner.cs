﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Spanner : Card
    {
        private int aux = 0;

        public Spanner()
        {
            Name = "Spanner (2D)";
            Weight = 1;
            Desc = "■ ■: Combine Dice";
            DiceNeeded = 2;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Spanner(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {

            if (aux == 0)
            {
                Game.ValidAction();
                aux += diceVal;
                UpdateData();
            }
            else
            {
                aux += diceVal;
                return true;
            }
            return false;
        }

        public override void Action(int diceVal)
        {            
            Game.ChangeDiceValue(Game.GetCurrentPlayer(), aux);
            Game.CardsUsed++;
            Used = true;
        }

        private void UpdateData()
        {
            Desc = "■ ■: Combine Dice (NEEDS 1 more Die) (Current value: " + aux + ")";
        }

        public override void ResetCard()
        {
            base.ResetCard();
            Desc = "■ ■: Combine Dice";
            aux = 0;
        }

    }
}
