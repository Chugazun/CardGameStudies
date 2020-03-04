using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class ChangeMachine : Card
    {
        private int aux = 0;
        private string currentName;
        public ChangeMachine()
        {
            Name = "Change Machine (2D)(T=4)";
            Desc = "Return four 1s (NEEDS 2 Dice) (MUST EQUAL 4)";
            currentName = "Change Machine";
            Weight = 1;
            DiceNeeded = 2;
            act = Action;
            condCheck = ConditionCheck;
        }

        public ChangeMachine(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            return TestCondition(diceVal, aux == 0, aux + diceVal == 4);
        }

        public override void Action(int diceVal)
        {
            Player currentPlayer = Game.GetCurrentPlayer();
            for (int i = 0; i < 4; i++)
            {
                Game.CreateDie(currentPlayer, 1);
            }

            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Change Machine- (2D)(T=4)(=3)";
            Desc = "Return four 1s (NEEDS 2 Dice) (MUST EQUAL 4) (FIRST SLOT NEEDS 3)";
            currentName = "Change Machine-";
            IsWeakened = true;
        }

        public override void Normalize()
        {
            Name = "Change Machine (2D)(T=4)";
            Desc = "Return four 1s (NEEDS 2 Dice) (MUST EQUAL 4)";
            currentName = "Change Machine";
            IsWeakened = false;
        }

        private bool TestCondition(int diceVal, bool condition_1, bool condition_2)
        {
            if (condition_1)
            {
                Game.ValidAction();
                aux = diceVal;
                UpdateData();
            }
            else if (condition_2)
            {
                Game.ValidAction();
                return true;
            }
            return false;
        }

        public override void ResetCard()
        {
            base.ResetCard();
            Name = currentName + " (2D)(T=4)";
            Desc = "Return four 1s (NEEDS 1 More Die) (MUST EQUAL 4)";
            if (IsWeakened)
            {
                Name += "(=3)";
                Desc += " (FIRST SLOT NEEDS 3)";
            }

            aux = 0;
        }

        private void UpdateData()
        {
            Name = currentName + " (2D)(T=4)";
            Desc = "Return four 1s (NEEDS 1 More Die) (MUST EQUAL 4)";
        }
    }
}
