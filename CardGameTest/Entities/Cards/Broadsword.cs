using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Broadsword : Card
    {
        private int aux = 0;
        private string currentDesc = "";

        public Broadsword()
        {
            Name = "Broadsword (2D)";
            Weight = 2;
            Desc = "Deals ■ ■ Damage (NEEDS 2 Dice)";
            currentDesc = Desc;
            DiceNeeded = 2;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Broadsword(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            Game.ValidAction();

            if (aux == 0)
            {
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
            Game.Damage(Game.GetCurrentMonster(), aux);
            Game.CardsUsed++;
            Used = true;
        }

        private void UpdateData()
        {
            Desc += " (Current Damage: " + aux + ")";
        }

        public override void ResetCard()
        {
            base.ResetCard();
            Desc = currentDesc;
            aux = 0;
        }

        public override void Weaken()
        {
            Name += "- (<=4)";
            Desc += "(Max 4)";
            currentDesc = Desc;
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal <= 4)
                {
                    Game.ValidAction();

                    if (aux == 0)
                    {
                        aux += diceVal;
                        UpdateData();
                    }
                    else
                    {
                        aux += diceVal;
                        return true;
                    }
                }
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Broadsword (2D)";            
            Desc = "Deals ■ ■ Damage (NEEDS 2 Dice)";
            currentDesc = Desc;
            IsWeakened = false;
            condCheck = ConditionCheck;
        }
    }
}
