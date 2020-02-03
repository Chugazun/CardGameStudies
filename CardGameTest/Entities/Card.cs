using System;
using System.Text;

namespace CardGameTest.Entities
{
    public class Card
    {
        public byte ID { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int DiceNeeded { get; set; }
        public string Desc { get; set; }
        public bool Used { get; set; }
        public int Uses { get; set; }

        public Action<int> act;

        public virtual void Action(int diceVal)
        {

        }

        public Card()
        {

        }

        public virtual void ResetCard()
        {
            Used = false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Name.Contains("(") ? Name.Substring(0, Name.IndexOf("(")) : Name);
            sb.Append("Dice Needed: ");
            sb.AppendLine(DiceNeeded.ToString());
            sb.Append("Effect: ");
            sb.AppendLine(Desc);

            return sb.ToString();
        }
    }
}
