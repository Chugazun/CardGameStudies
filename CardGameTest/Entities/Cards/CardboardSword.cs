namespace CardGameTest.Entities.Cards
{
    class CardboardSword : Card
    {
        private int aux = 9;
        private string currentDesc;
        public CardboardSword()
        {
            Name = "Cardboard Sword (E)";
            Desc = "Deal " + aux + " damage, loses 2 damage each use (Even Only)";
            currentDesc = "(Even Only)";
            Weight = 2;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public CardboardSword(byte id) : this()
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
            Game.Damage(Game.GetCurrentMonster(), aux);
            aux -= 2;
            UpdateData();
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Cardboard Sword- (<=2)";
            currentDesc = "(Max 2)";
            Desc = "Deal " + aux + " damage, loses 2 damage each use "+ currentDesc;
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal <= 2) return base.ConditionCheck(diceVal);
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Cardboard Sword (E)";
            currentDesc = "(Even Only)";
            Desc = "Deal " + aux + " damage, loses 2 damage each use " + currentDesc;
            IsWeakened = false;

            condCheck = ConditionCheck;
        }

        private void UpdateData()
        {
            Desc = "Deal " + aux + " damage, loses 2 damage each use " + currentDesc;
        }
    }
}
