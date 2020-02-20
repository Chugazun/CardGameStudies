using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class BloodCauldron : Card
    {
        public BloodCauldron()
        {
            Name = "Blood Cauldron";
            Desc = "Drain 1 health, get a new die";
            Weight = 1;
            DiceNeeded = 1;
            act = Action;
            condCheck = ConditionCheck;
        }

        public BloodCauldron(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), 1);
            Game.Heal(Game.GetCurrentPlayer(), 1);
            Game.CreateDie(Game.GetCurrentPlayer(), new Random().Next(1, 7));
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name += "-";
            Desc = "Drain 1 health";
            IsWeakened = true;

            act = diceVal =>
            {
                Game.Damage(Game.GetCurrentMonster(), 1);
                Game.Heal(Game.GetCurrentPlayer(), 1);
                Game.CardsUsed++;
                Used = true;
            };
        }

        public override void Normalize()
        {
            Name = "Blood Cauldron";
            Desc = "Drain 1 health, get a new die";
            IsWeakened = false;

            act = Action;
        }
    }
}
