using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Counterfeit : Card
    {
        public Counterfeit()
        {
            Name = "Counterfeit";
            Weight = 2;
            Desc = "■: Duplicate a Die";
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Counterfeit(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            return true;
        }

        public override void Action(int diceVal)
        {
            Player player = Game.GetCurrentPlayer();
            Game.CreateDie(player, player.Dice[Game.GetCurrentDicePos()]);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name += "- (<=3)";
            Desc += "(Max 3)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                return diceVal <= 3;
            };
        }

        public override void Normalize()
        {
            Name = "Counterfeit";            
            Desc = "■: Duplicate a Die";
            IsWeakened = false;
            condCheck = ConditionCheck;            
        }
    }
}
