using CardGameTest.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Entities.Cards
{
    class Catastrophe : Card
    {
        private int aux = 7;
        private string currentName;
        public Catastrophe()
        {
            Name = "Catastrophe (7)";
            Desc = "Inflict Shock, Burn and Frost (Countdown 7)";
            currentName = "Catastrophe";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Catastrophe(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            Game.ValidAction();
            aux = Math.Max(aux - diceVal, 0);
            UpdateData();
            return aux == 0;
        }

        public override void Action(int diceVal)
        {
            Player currentPlayer = Game.GetCurrentPlayer();
            currentPlayer.Status.Shock++;
            currentPlayer.Status.Burn++;
            currentPlayer.Status.Frost++;

            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Catastrophe- (" + aux + ")";
            currentName = "Catastrophe-";
            Desc = "Inflict Shock, Burn or Frost (Countdown 7)";
            IsWeakened = true;

            act = diceVal =>
            {
                string[] statusList = { "Shock", "Burn", "Frost" };

                Type targetType = Game.GetCurrentPlayer().Status.GetType();
                StatusSheet targetStatusSheet = Game.GetCurrentPlayer().Status;

                PropertyInfo statusName = targetType.GetProperty(statusList[new Random().Next(3)]);
                int statusNewValue = (int)statusName.GetValue(targetStatusSheet) + 1;

                statusName.SetValue(targetStatusSheet, statusNewValue);

                Game.CardsUsed++;
                Used = true;
            };
        }

        public override void Normalize()
        {
            Name = "Catastrophe (" + aux + ")";
            currentName = "Catastrophe";
            Desc = "Inflict Shock, Burn and Frost (Countdown 7)";
            IsWeakened = false;

            act = Action;
        }

        public override void ResetCard()
        {
            base.ResetCard();
            aux = 7;
            UpdateData();
        }

        private void UpdateData()
        {
            Name = currentName + " (" + aux + ")";
        }
    }
}
