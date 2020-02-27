using System;
using System.Text.RegularExpressions;
using CardGameTest.Extensions;

namespace CardGameTest.Entities.Cards
{
    class BronzeCauldron : Card
    {
        public BronzeCauldron()
        {
            Name = "Bronze Cauldron (E)(R)";
            Desc = "Deal 2 damage, get an odd die (Even Only) (Reusable)";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public BronzeCauldron(byte id) : this()
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
            Game.Damage(Game.GetCurrentMonster(), 2);
            Game.CreateDie(Game.GetCurrentPlayer(), new Random().Next(new int[] { 1, 3, 5 }));
            Game.CardsUsed++;
        }

        public override void Weaken()
        {
            Name = "Bronze Cauldron- (E)";
            Desc = "Deal 1 damage (Even Only)";
            IsWeakened = true;

            act = diceVal =>
            {
                Game.Damage(Game.GetCurrentMonster(), 1);
                Game.CardsUsed++;
                Used = true;
            };
        }

        public override void Normalize()
        {
            Name = "Bronze Cauldron (E)(R)";
            Desc = "Deal 2 damage, get an odd die (Even Only) (Reusable)";
            IsWeakened = false;

            act = Action;
        }
    }
}
