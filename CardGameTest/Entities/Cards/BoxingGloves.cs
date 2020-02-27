using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class BoxingGloves : Card
    {
        public BoxingGloves()
        {
            Name = "Boxing Gloves (<=5)";
            Desc = "Deal ■ damage, on 1 gain 1 Resistance (Max 5)";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            act += CheckResistanceGain;
            condCheck = ConditionCheck;
        }

        public BoxingGloves(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (diceVal <= 5) return base.ConditionCheck(diceVal);
            return false;
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), diceVal);            
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Boxing Gloves- (<=5)";
            Desc = "Deal ■ damage (Max 5)";
            IsWeakened = true;

            act -= CheckResistanceGain;

        }

        public override void Normalize()
        {
            Name = "Boxing Gloves (<=5)";
            Desc = "Deal ■ damage, on 1 gain 1 Resistance (Max 5)";
            IsWeakened = false;

            act += CheckResistanceGain;
        }

        private void CheckResistanceGain(int diceVal)
        {
            if (diceVal == 1) Game.GetCurrentPlayer().Status.Resistance++;
        }
    }
}
