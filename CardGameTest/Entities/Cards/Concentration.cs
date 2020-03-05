using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Concentration : Card
    {
        private int turnsLeft, aux;
        public Concentration()
        {
            Name = "Concentration (<=4)";
            Desc = "In ■ turns, recover 2x ■ health (Max 4)";
            Weight = 2;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Concentration(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            return TestCondition(diceVal, diceVal <= 4);
        }

        public override void Action(int diceVal)
        {
            turnsLeft = diceVal;
            aux = diceVal * 2;
            Game.AddTurnStartEffect(ConcentrationCheck);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Concentration- (=4)";
            Desc = "In ■ turns, recover 2x ■ health (NEEDS 4)";
            IsWeakened = true;

            condCheck = diceVal => TestCondition(diceVal, diceVal == 4);
        }

        public override void Normalize()
        {
            Name = "Concentration (<=4)";
            Desc = "In ■ turns, recover 2x ■ health (Max 4)";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }

        private void ConcentrationCheck()
        {
            if (turnsLeft > 1)
            {
                turnsLeft--;
                Game.Log.AppendLine($"{turnsLeft} turn(s) left to heal! (Concentration)".Pastel(Color.Violet));
            }
            else
            {
                Game.Heal(Game.GetCurrentPlayer(), aux);
                Game.RemoveTurnStartEffect(ConcentrationCheck);
            }
        }

        private bool TestCondition(int diceVal, bool condition)
        {
            if (condition) return base.ConditionCheck(diceVal);
            return false;
        }
    }
}
