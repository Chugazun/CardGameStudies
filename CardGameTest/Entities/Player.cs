using CardGameTest.Utils;
using System.Collections.Generic;

namespace CardGameTest.Entities
{
    class Player : Entity
    {       
        public int Gold { get; private set; }
        public int Level { get; private set; }
        public int Exp { get; set; }
        public PlayerBag PlayerBag { get; set; } = new PlayerBag();

        public Player(int hp) : base (hp)
        {            
            Level = 1;
        }
    }
}
