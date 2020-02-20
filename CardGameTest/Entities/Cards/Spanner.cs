using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Entities.Cards
{
    class Spanner : Card
    {
        private int aux = 0;
        private string currentDesc = "";

        public Spanner()
        {
            Name = "Spanner (2D)";
            Weight = 1;
            Desc = "■ ■: Combine Dice (NEEDS 2 Dice)";
            currentDesc = Desc;
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
            Desc = Regex.Replace(Desc, "2", "1");
            Desc += " (Current value: " + aux + ")";
        }

        public override void ResetCard()
        {
            base.ResetCard();
            Desc = currentDesc;
            aux = 0;
        }

        public override void Weaken()
        {
            Name = "Spanner- (2D)(<=3)";
            Desc = "■ ■: Combine Dice (NEEDS 2 Dice) (Max 3)";
            currentDesc = Desc;
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal <= 3) return ConditionCheck(diceVal);
                return false;
            };
        }
        public override void Normalize()
        {
            Name = "Spanner (2D)";
            Desc = "■ ■: Combine Dice (NEEDS 2 Dice)";
            currentDesc = Desc;
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
