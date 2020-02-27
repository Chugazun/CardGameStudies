using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Entities.Cards
{
    class Breadcrumbs : Card
    {
        private int aux = 1;
        public Breadcrumbs()
        {
            Name = "Breadcrumbs (=1)(2D)(R)";
            Desc = "■ ■: Heals 1, gain 1 Shield (NEEDS 1) (NEEDS 2 Dice) (Reusable)";
            Weight = 2;
            DiceNeeded = 2;
            act = Action;
            act += HealingEffect;
            condCheck = ConditionCheck;
        }

        public Breadcrumbs(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (diceVal == 1 && aux < DiceNeeded)
            {
                Game.ValidAction();
                aux++;
                UpdateData();
            }
            else if (diceVal == 1)
            {
                Game.ValidAction();
                return true;
            }
            return false;
        }

        public override void Action(int diceVal)
        {
            Game.GainShield(Game.GetCurrentPlayer(), 1);
            Game.CardsUsed++;
            ResetCard();
        }

        public override void Weaken()
        {
            Name = Regex.Replace(Name, " ", "- ");
            Desc = "■ ■: Gain 1 Shield (NEEDS 1) (NEEDS 2 Dice) (Reusable)";
            IsWeakened = true;

            act -= HealingEffect;
        }

        public override void Normalize()
        {
            Name = "Breadcrumbs (=1)(2D)(R)";
            Desc = "■ ■: Heals 1, gain 1 Shield (NEEDS 1) (NEEDS 2 Dice) (Reusable)";
            IsWeakened = false;

            act += HealingEffect;
        }

        private void UpdateData()
        {
            Name = Regex.Replace(Name, "2", "1");
            Desc = Regex.Replace(Desc, "2 Dice", "1 more Die");
        }

        public override void ResetCard()
        {
            base.ResetCard();
            Name = Regex.Replace(Name, "1D", "2D");
            Desc = Regex.Replace(Desc, "1 more Die", "2 Dice");
            aux = 1;
        }

        private void HealingEffect(int diceVal)
        {
            Game.Heal(Game.GetCurrentPlayer(), 1);
        }
    }
}
