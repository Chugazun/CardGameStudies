using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class ColdWind : Card
    {
        private int aux;
        private string currentDesc;
        public ColdWind()
        {
            Name = "Cold Wind (2D=)";
            Desc = "Inflict 1 Freeze, return both dice (NEEDS double)";
            currentDesc = Desc;
            Weight = 1;
            DiceNeeded = 2;
            act = Action;
            act += ReturnDice;
            condCheck = ConditionCheck;
        }

        public ColdWind(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (aux == 0)
            {
                Game.ValidAction();
                aux = diceVal;
                UpdateData();
            }
            else if (diceVal == aux)
            {
                Game.ValidAction();
                return true;
            }
            return false;
        }

        public override void Action(int diceVal)
        {
            Game.GetCurrentPlayer().Status.Frost++;
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Cold Wind- (2D=)";
            Desc = "Inflict 1 Freeze (NEEDS double)";
            currentDesc = Desc;
            IsWeakened = true;

            act -= ReturnDice;
        }

        public override void Normalize()
        {
            Name = "Cold Wind (2D=)";
            Desc = "Inflict 1 Freeze, return both dice (NEEDS double)";
            currentDesc = Desc;
            IsWeakened = false;

            act += ReturnDice;
        }

        private void UpdateData()
        {
            Desc += "(Current Value: " + aux + ")";            
        }

        public override void ResetCard()
        {
            base.ResetCard();
            Desc = currentDesc;
            aux = 0;
        }

        private void ReturnDice(int diceVal)
        {
            Player currentPlayer = Game.GetCurrentPlayer();
            Game.CreateDie(currentPlayer, diceVal);
            Game.CreateDie(currentPlayer, diceVal);
        }
    }
}
