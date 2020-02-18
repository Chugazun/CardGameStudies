using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Entities.Cards
{
    class Execute : Card
    {
        private int aux = 0;
        private string currentDesc = "";
        public Execute()
        {
            Name = "Execute (=1)";
            Weight = 1;
            Desc = "(NEEDS 1) Deals " + Game.CardsUsed + " Damage. Plus 1 Damage for each attack used";
            currentDesc = Desc;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Execute(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (diceVal == 1)
            {
                return base.ConditionCheck(diceVal);
            }
            return false;
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), Game.CardsUsed);
            Game.CardsUsed++;
            Used = true;
        }

        public override string ToString()
        {
            Desc = "(NEEDS 1) Deals " + Game.CardsUsed + " Damage. Plus 1 Damage for each attack used " + currentDesc;
            return base.ToString();
        }
        public override void ResetCard()
        {
            base.ResetCard();
            aux = 0;
            Desc = currentDesc;
        }

        public override void Weaken()
        {
            Name += "(2D)";
            currentDesc = "(NEEDS 2 Dice)";
            DiceNeeded = 2;
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal == 1 && aux < DiceNeeded)
                {
                    Game.ValidAction();
                    aux++;
                    currentDesc = Regex.Replace(currentDesc, currentDesc.Substring(currentDesc.IndexOf(" ") + 1, 1), aux.ToString());
                }

                return aux == DiceNeeded;
            };
        }

        public override void Normalize()
        {
            Name = "Execute (=1)";
            currentDesc = "";
            DiceNeeded = 1;

            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
