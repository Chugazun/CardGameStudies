using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Entities.Cards
{
    class Calculator : Card
    {
        private int aux = 0;
        public Calculator()
        {
            Name = "Calculator (2D)(T=7)";
            Desc = "Multiply dice together, deal that much damage (NEEDS 2 Dice) (MUST EQUAL 7)";
            Weight = 1;
            DiceNeeded = 2;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Calculator(byte id) : this()
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
            else if (aux + diceVal == 7)
            {
                Game.ValidAction();
                return true;
            }
            return false;
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), aux * diceVal);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = Regex.Replace(Name, " ", "- ");
            Name = Regex.Replace(Name, "7", "6");
            Desc = Regex.Replace(Desc, "7", "6");
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (aux == 0)
                {
                    Game.ValidAction();
                    aux += diceVal;
                    UpdateData();
                }
                else if (aux + diceVal == 6)
                {
                    Game.ValidAction();
                    return true;
                }
                return false;
            };
        }

        public override void Normalize()
        {
            Name = Regex.Replace(Name, "- ", " ");
            Name = Regex.Replace(Name, "6", "7");
            Desc = Regex.Replace(Desc, "6", "7");
            IsWeakened = false;

            condCheck = ConditionCheck;
        }

        public override void ResetCard()
        {
            base.ResetCard();
            Name = Regex.Replace(Name, "1", "2");
            Desc = Regex.Replace(Desc, "1 More Die", "2 Dice");
            aux = 0;

        }

        private void UpdateData()
        {
            Name = Regex.Replace(Name, "2", "1");
            Desc = Regex.Replace(Desc, "2 Dice", "1 More Die");
        }

    }
}
