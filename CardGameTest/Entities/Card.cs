using System.Text;

namespace CardGameTest.Entities
{
    abstract class Card
    {
        public byte ID { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int DiceNeeded { get; set; }
        public string Desc { get; set; }
        public bool Used { get; set; }        

        public abstract void Action(int diceVal);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Name);
            sb.Append("Dice Needed: ");
            sb.AppendLine(DiceNeeded.ToString());
            sb.Append("Effect: ");
            sb.AppendLine(Desc);

            return sb.ToString();
        }
    }
}
