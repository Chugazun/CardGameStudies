using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Antidote : Card
    {
        private int aux = 0;
        public Antidote()
        {
            Name = "Antidote (2D=)";
            Weight = 1;
            Desc = "Cure ■ poison (NEEDS doubles)";
            DiceNeeded = 2;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Antidote(byte id) : this()
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
                return false;
            }
            else
            {
                if (diceVal == aux)
                {
                    Game.ValidAction();
                    return true;
                }
                return false;
            }
        }

        public override void Action(int diceVal)
        {
            Game.GetCurrentPlayer().Status.Poison = Math.Max(Game.GetCurrentPlayer().Status.Poison -= diceVal, 0);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Antidote- (2D=)";
            Desc = "Cure ■ poison (NEEDS doubles) (for both you and enemy)";
            IsWeakened = true;

            act += diceVal => Game.GetCurrentMonster().Status.Poison = Math.Max(Game.GetCurrentMonster().Status.Poison -= diceVal, 0);
        }

        public override void Normalize()
        {
            Name = "Antidote (2D=)";
            Desc = "Cure ■ poison (NEEDS doubles)";
            IsWeakened = false;

            act = Action;
        }

        private void UpdateData()
        {
            Desc += "(Current Value: " + aux + ")";
        }

        public override void ResetCard()
        {
            base.ResetCard();
            aux = 0;
        }
    }
}
