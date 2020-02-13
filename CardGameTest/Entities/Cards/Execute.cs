using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Execute : Card
    {
        public Execute()
        {
            Name = $"Execute (=1)";
            Weight = 1;
            Desc = "(NEEDS 1) Deals " + Game.CardsUsed + " Damage. Plus 1 Damage for each attack used";
            DiceNeeded = 1;
            act = Action;
        }

        public Execute(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (diceVal == 1)
            {
                return base.ConditionCheck(diceVal);
            }
            return false;
        }

        public override void Action(int diceVal)
        {            
            Game.Damage(Game.GetCurrentMonster(), Game.CardsUsed);
            Game.CardsUsed++;
            Used = true;            
        }

        public override string ToString()
        {
            Desc = "(NEEDS 1) Deals " + Game.CardsUsed + " Damage. Plus 1 Damage for each attack used";
            return base.ToString();
        }
        public override void ResetCard()
        {
            base.ResetCard();
            Desc = "(NEEDS 1) Deals " + Game.CardsUsed + " Damage. Plus 1 Damage for each attack used";
        }
    }
}
