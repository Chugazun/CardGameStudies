using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class Chainsaw : Card
    {
        private int aux = 0;
        private string currentDesc;
        public Chainsaw()
        {
            Name = "Chainsaw (2D)(=6)";
            Desc = "Deal ■ ■ damage, if enemy HP is full, deal double damage (NEEDS 2 Dice) (NEEDS 6)";
            currentDesc = "(NEEDS 6)";
            Weight = 1;
            DiceNeeded = 2;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Chainsaw(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            return TestCondition(diceVal, diceVal == 6);
        }

        public override void Action(int diceVal)
        {
            Monster currentMonster = Game.GetCurrentMonster();
            int finalDamage = currentMonster.CurrentHp == currentMonster.MaxHp ? aux * 2 : aux;

            Game.Damage(currentMonster, finalDamage);
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Chainsaw- (2D)(=3)";
            Desc = "Deal ■ ■ damage, if enemy HP is full, deal double damage (NEEDS 2 Dice) (NEEDS 3)";
            currentDesc = "(NEEDS 3)";
            IsWeakened = true;

            condCheck = diceVal => TestCondition(diceVal, diceVal == 3);
        }

        public override void Normalize()
        {
            Name = "Chainsaw (2D)(=6)";
            Desc = "Deal ■ ■ damage, if enemy HP is full, deal double damage (NEEDS 2 Dice) (NEEDS 6)";
            currentDesc = "(NEEDS 6)";
            IsWeakened = false;

            condCheck = ConditionCheck;
        }

        private bool TestCondition(int diceVal, bool condition)
        {
            if (condition)
            {
                Game.ValidAction();
                if (aux == 0)
                {
                    aux += diceVal;
                    UpdateData();
                }
                else
                {
                    aux += diceVal;
                    return true;
                }
            }
            return false;
        }

        public override void ResetCard()
        {
            base.ResetCard();
            Desc = "Deal ■ ■ damage, if enemy HP is full, deal double damage (NEEDS 2 Dice) " + currentDesc;
            aux = 0;
        }

        private void UpdateData()
        {
            Desc = "Deal ■ ■ damage, if enemy HP is full, deal double damage (NEEDS 1 more Die) " + currentDesc;
        }
    }
}
