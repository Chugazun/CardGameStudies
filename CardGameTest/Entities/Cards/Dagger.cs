using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Dagger : Card
    {
        public Dagger()
        {
            Uses = 2;
            Name = "Dagger (x" + Uses + ")";
            Weight = 1;            
            Desc = "Deal 3 Damage (Uses: " + Uses + ")";
            DiceNeeded = 1;            
            act = Action;
        }

        public Dagger(byte id) : this()
        {
            ID = id;
        }

        public override void Action(int diceVal)
        {
            if (Uses > 0)
            {
                Game.ValidAction();
                Game.Damage(Game.GetCurrentMonster(), 3);                
                Uses--;
                UpdateData();
            }
            if (Uses <= 0) Used = true;
        }

        public void UpdateData()
        {
            Name = "Dagger (x" + Uses + ")";
            Desc = "Deal 3 Damage (Uses: " + Uses + ")";
        }

        public override void ResetCard()
        {
            base.ResetCard();
            Uses = 2;
            UpdateData();
        }
    }
}
