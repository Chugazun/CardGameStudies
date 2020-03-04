using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Entities.Cards
{
    class BrokenMirror : Card
    {
        public BrokenMirror()
        {
            Name = "Broken Mirror (E)";
            Desc = "Next turn, gain +1 dice this fight, and curse yourself";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public BrokenMirror(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (diceVal % 2 == 0) return base.ConditionCheck(diceVal);
            return false;
        }

        public override void Action(int diceVal)
        {
            Game.AddTurnStartEffect(GainPermanentDie);
            Game.GetCurrentPlayer().Status.Curse++;
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Broken Mirror- (<=2)";
            Desc = Regex.Replace(Desc, "Even Only", "Max 2");
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal <= 2) return base.ConditionCheck(diceVal);
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Broken Mirror (E)";
            Desc = Regex.Replace(Desc, "Max 2", "Even Only");
            IsWeakened = false;

            condCheck = ConditionCheck;
        }

        private void GainPermanentDie()
        {
            Game.CreateDie(Game.GetCurrentPlayer(), new Random().Next(1, 7));
        }
    }
}
