using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Entities.Cards
{
    class Befuddle : Card
    {
        private int aux, auxDicePos;
        public Befuddle()
        {
            Name = "Befuddle";
            Desc = "■ ■: Duplicate one die, return the other (NEEDS 2 Dice)";
            Weight = 2;
            DiceNeeded = 2;
            act = Action;
            condCheck = ConditionCheck;
        }

        public Befuddle(byte id) : this()
        {
            ID = id;
        }

        public override bool ConditionCheck(int diceVal)
        {
            if(aux == 0) 
            {
                aux = diceVal;
                auxDicePos = Game.GetCurrentDicePos();
                Game.ValidAction();
                return false;
            } else
            {
                return true;
            }
        }

        public override void Action(int diceVal)
        {
            if(new Random().Next(2) == 0)
            {
                ManageDice(diceVal, Game.GetCurrentDicePos() + 1, aux, auxDicePos);
            } else
            {
                ManageDice(aux, auxDicePos + 1, diceVal, Game.GetCurrentDicePos());
            }
            Game.CardsUsed++;
            Used = true;
        }

        public override void Weaken()
        {
            Name += "- (<=3)";
            Desc += " (Max 3)";
            IsWeakened = true;

            condCheck = diceVal =>
            {
                if (diceVal <= 3) return ConditionCheck(diceVal);
                return false;
            };
        }

        public override void Normalize()
        {
            Name = "Befuddle";
            Desc = "■ ■: Duplicate one die, return the other (NEEDS 2 Dice)";            
            IsWeakened = false;

            condCheck = ConditionCheck;
        }

        private void UpdateData()
        {
            Desc = Regex.Replace(Desc, "2", "1");
        }

        public override void ResetCard()
        {
            base.ResetCard();
            Name = "Befuddle";
            Desc = "■ ■: Duplicate one die, return the other (NEEDS 2 Dice)";
            aux = 0;
        }

        private void ManageDice(int returnDie, int returnDiePos, int duplicateDie, int duplicateDiePos)
        {
            Game.ChangeDiceValue(Game.GetCurrentPlayer(), returnDie);
            Game.CreateDie(Game.GetCurrentPlayer(), duplicateDie);
            Game.CreateDie(Game.GetCurrentPlayer(), duplicateDie);

            Game.Log.Append($"#D{returnDiePos}");
            Game.Log.Append($": {returnDie}");
            Game.Log.AppendLine(" was returned!");

            Game.Log.Append($"#D{duplicateDiePos}");
            Game.Log.Append($": {duplicateDie}");
            Game.Log.AppendLine(" was duplicated!");
        }
    }
}
