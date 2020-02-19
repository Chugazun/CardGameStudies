using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities.Cards
{
    class BatteringRam : Card
    {
        private int aux = 0;
        public BatteringRam()
        {
            Name = "Battering Ram (2D=)";
            Desc = "Deal 2x your Shield as damage, but also depletes it (NEEDS doubles)";
            Weight = 1;
            DiceNeeded = 2;
            act = Action;
            condCheck = ConditionCheck;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if (aux == 0)
            {
                Game.ValidAction();
                aux = diceVal;
                UpdateData();
                return false;
            }
            else
            {
                if (diceVal == aux)
                {
                    Game.ValidAction();
                    return true;
                }
                return false;
            }
        }

        public override void Action(int diceVal)
        {
            Game.Damage(Game.GetCurrentMonster(), Game.GetCurrentPlayer().Status.Shield * 2);
            Game.GetCurrentPlayer().Status.Shield = 0;
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name = "Battering Ram- (2D)(=6)";
            Desc = "Deal your Shield as damage (NEEDS 2 Dice) (Only 6)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (aux == 0 && diceVal == 6)
                {
                    Game.ValidAction();
                    aux = diceVal;
                    UpdateData();
                    return false;
                }
                else
                {
                    if (diceVal == aux)
                    {
                        Game.ValidAction();
                        return true;
                    }
                    return false;
                }
            };

            act = diceVal =>
            {
                Game.Damage(Game.GetCurrentMonster(), Game.GetCurrentPlayer().Status.Shield);
                Game.CardsUsed++;
                Used = true;
            };
        }

        public override void Normalize()
        {
            Name = "Battering Ram (2D)";
            Desc = "Deal 2x your Shield as damage, but also depletes it (NEEDS 2 Dice)";
            IsWeakened = true;

            condCheck = ConditionCheck;
            act = Action;
        }

        private void UpdateData()
        {
            Desc += "(Dice: 1/2)";
        }

        public override void ResetCard()
        {
            base.ResetCard();
            aux = 0;
        }
    }
}
