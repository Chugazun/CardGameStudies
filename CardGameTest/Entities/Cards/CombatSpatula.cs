using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Entities.Cards
{
    class CombatSpatula : Card
    {
        private int uses = 2;
        public CombatSpatula()
        {
            Name = "Combat Spatula (x2)";
            Desc = "Flip a die upside down (2 uses remaining)";
            Weight = 2;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public CombatSpatula(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            uses--;
            UpdateData();
            if (uses == 0) Used = true;
            return true;
        }

        public override void Action(int diceVal)
        {
            Game.ChangeDiceValue(Game.GetCurrentPlayer(), 7 - diceVal);
            Game.CardsUsed++;
        }

        public override void Weaken()
        {
            Name = "Combat Spatula- (x2)(E)";
            Desc = "Flip a die upside down (2 uses remaining) (Even Only)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal % 2 == 0) return ConditionCheck(diceVal);
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Combat Spatula (x2)";
            Desc = "Flip a die upside down (2 uses remaining)";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }

        public override void ResetCard()
        {
            base.ResetCard();
            uses = 2;
            Name = Regex.Replace(Name, "x1", "x2");
            Desc = Regex.Replace(Desc, "1 use", "2 uses");
        }

        private void UpdateData()
        {
            Name = Regex.Replace(Name, "x2", "x1");
            Desc = Regex.Replace(Desc, "2 uses", "1 use");
        }
    }
}
