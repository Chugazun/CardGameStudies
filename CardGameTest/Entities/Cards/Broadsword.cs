using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Broadsword : Card
    {
        private int aux = 0;

        public Broadsword()
        {
            Name = "Broadsword (2D)";
            Weight = 2;
            Desc = "Deals ■ ■ Damage (NEEDS 2 Dice)";
            DiceNeeded = 2;
            act = Action;
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
            Desc = "Deals ■ ■ Damage (NEEDS 1 more Die) (Current Damage: " + aux + ")";
        }

        public override void ResetCard()
        {
            base.ResetCard();
            Desc = "Deals ■ ■ Damage (NEEDS 2 Dice)";
            aux = 0;
        }
    }
}
