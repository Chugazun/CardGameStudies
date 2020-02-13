using CardGameTest.Utils;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities
{
    class Player : Entity
    {
        public int Gold { get; private set; }
        public int Level { get; private set; }
        public int Exp { get; set; }
        public PlayerBag PlayerBag { get; set; } = new PlayerBag();
        public int DiceQuant { get; private set; }

        public Player(int hp) : base(hp)
        {
            Level = 1;
            DiceQuant = 5;
        }

        public string PlayerDice()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Dice.Count; i++)
            {
                sb.Append("#D" + (i + 1));
                sb.Append(": ");
                sb.Append(Dice[i]);
                sb.Append(", ");
            }

            return sb.ToString()[0..^2];
        }

        public override List<Card> GetCards()
        {
            return PlayerBag.GetCards();
        }

        public override Card GetCardAt(int handPos)
        {
            return PlayerBag.GetCardAt(handPos);
        }
    }
}
