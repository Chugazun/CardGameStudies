using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class SuckerPunch : Card
    {
        public SuckerPunch()
        {
            Name = "Sucker Punch";
            Weight = 1;
            Desc = "■: Deals 1 Damage. Get a new Die";
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public SuckerPunch(byte id) : this()
        {
            ID = id;
        }        

        public override void Action(int diceVal)
        {            
            Game.Damage(Game.GetCurrentMonster(), 1);
            Random rand = new Random();
            Game.CreateDie(Game.GetCurrentPlayer(), rand.Next(1, 7));
            Game.CardsUsed++;
            Used = true;            
        }

        public override void Weaken()
        {
            Name += "-";
            Desc = "■: Deals 1 Damage. Roll a 1";
            IsWeakened = true;

            act = diceVal =>
            {
                Game.Damage(Game.GetCurrentMonster(), 1);
                Game.CreateDie(Game.GetCurrentPlayer(), 1);
                Game.CardsUsed++;
                Used = true;
            };
        }

        public override void Normalize()
        {
            Name = "Sucker Punch";            
            Desc = "■: Deals 1 Damage. Get a new Die";
            IsWeakened = false;

            act = Action;
        }
    }
}
