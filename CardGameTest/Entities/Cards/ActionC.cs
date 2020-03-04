using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class ActionC : Card
    {
        private int aux;
        public ActionC()
        {
            Name = "Action!";
            Desc = "Keep a ■ for next turn";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public ActionC(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            aux = diceVal;
            Game.AddTurnStartEffect(DiceSave);
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
            Name = "Action!";
            Desc = "Keep a ■ for next turn";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }

        private void DiceSave()
        {
            Player currentPlayer = Game.GetCurrentPlayer();
            ActionC card = currentPlayer.GetCardAt(ID) as ActionC;
            Game.CreateDie(currentPlayer, card.aux);
            Game.RemoveTurnStartEffect(DiceSave);
        }
    }
}
