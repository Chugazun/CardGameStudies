using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Blight : Card
    {
        private int aux = 24;
        private string currentName;
        public Blight()
        {
            Name = "Blight (" + aux + ")";
            currentName = "Blight";
            Desc = "Double poison";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Blight(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            aux = Math.Max(aux -= diceVal, 0);
            UpdateData();
            Game.ValidAction();
            return aux == 0;
        }

        public override void Action(int diceVal)
        {
            Game.GetCurrentMonster().Status.Poison *= 2;
            Game.CardsUsed++;
            aux = 24;
            Used = true;            
        }

        public override void Weaken()
        {
            Name = "Blight- (" + aux + ")";
            currentName = "Blight-";
            Desc = "Increase poison by 50 percent (Countdown 24)";
            IsWeakened = true;

            act = diceVal =>
            {
                double halfPoison = Game.GetCurrentMonster().Status.Poison / 2;
                Game.GetCurrentMonster().Status.Poison += (int)Math.Floor(halfPoison);
                Game.CardsUsed++;
                aux = 24;
                Used = true;
            };
        }

        public override void Normalize()
        {
            Name = "Blight (" + aux + ")";
            currentName = "Blight";
            Desc = "Double poison (Countdown 24)";
            IsWeakened = false;

            act = Action;
        }

        private void UpdateData()
        {
            Name = currentName + " (" + aux + ")";
        }

        public override void ResetCard()
        {
            base.ResetCard();            
            Name = "Blight (" + aux + ")";
            currentName = "Blight";
        }

    }
}
