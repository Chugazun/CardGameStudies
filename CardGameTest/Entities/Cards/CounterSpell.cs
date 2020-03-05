using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class CounterSpell : Card
    {
        private int aux;
        public CounterSpell()
        {
            Name = "Counter Spell";
            Desc = "If enemy rolls ■, that die is Locked";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public CounterSpell(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            aux = diceVal;
            Game.AddTurnStartEffect(CheckRoll);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name += "- (O)";
            Desc += " (Odd Only)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal % 2 != 0) return base.ConditionCheck(diceVal);
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Counter Spell";
            Desc = "If enemy rolls ■, that die is Locked";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }

        private void CheckRoll()
        {
            Player currentPlayer = Game.GetCurrentPlayer();
            List<Die> selectedDice = currentPlayer.Dice
                                                   .Where(d => d.Value == aux && !d.IsLocked)                                                   
                                                   .ToList();
            selectedDice.ForEach(die => die.IsLocked = true);
            Game.RemoveTurnStartEffect(CheckRoll);
        }
    }
}
