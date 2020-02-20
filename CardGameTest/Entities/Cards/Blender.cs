using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Blender : Card
    {
        public Blender()
        {
            Name = "Blender (>=2)";
            Desc = "Split ■ into 1s (Min 2)";
            Weight = 2;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Blender(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (diceVal >= 2) return base.ConditionCheck(diceVal);
            return false;
        }

        public override void Action(int diceVal)
        {
            for (int i = 0; i < diceVal; i++)
            {
                Game.CreateDie(Game.GetCurrentPlayer(), 1);
            }
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Blender- (E)";
            Desc = "Split ■ into 1s (Even only)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal % 2 == 0) return base.ConditionCheck(diceVal);
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Blender (>=2)";
            Desc = "Split ■ into 1s (Min 2)";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }
    }
}
