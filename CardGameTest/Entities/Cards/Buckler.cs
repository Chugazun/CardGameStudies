using System;
namespace CardGameTest.Entities.Cards
{
    class Buckler : Card
    {
        private int aux = 7;
        private string currentName;
        public Buckler()
        {
            Name = "Buckler (" + aux + ")";
            Desc = "Gain 4 Shield (Countdown 7)";
            currentName = "Buckler";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Buckler(byte id) : this()
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
            Game.GainShield(Game.GetCurrentPlayer(), 4);
            Game.CardsUsed++;
            aux = 7;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Buckler- (" + aux + ")";
            currentName = "Buckler-";
            Desc = "Gain 2 Shield (Countdown 7)";
            IsWeakened = true;

            act = diceVal =>
            {
                Game.GainShield(Game.GetCurrentPlayer(), 2);
                Game.CardsUsed++;
                aux = 7;
                Used = true;
            };
        }

        public override void Normalize()
        {
            Name = "Buckler (" + aux + ")";
            currentName = "Buckler";
            Desc = "Gain 4 Shield (Countdown 7)";
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
            Name = "Buckler (" + aux + ")";
            currentName = "Buckler";
        }
    }
}
