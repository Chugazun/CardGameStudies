using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Avalanche : Card
    {
        public Avalanche()
        {
            Name = "Avalanche (>=3)";
            Weight = 1;
            Desc = "Deals 3 damage, reroll with lower value (Min 3)";
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Avalanche(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            return diceVal >= 3;
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), 3);
            Game.ChangeDiceValue(Game.GetCurrentPlayer(), new Random().Next(1, diceVal));
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Avalanche- (>=4)";
            Desc = "Deals 3 damage, reroll with lower value (Min 4)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                return diceVal >= 4;
            };            
        }

        public override void Normalize()
        {
            Name = "Avalanche (>=3)";
            Desc = "Deals 3 damage, reroll with lower value (Min 3)";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
