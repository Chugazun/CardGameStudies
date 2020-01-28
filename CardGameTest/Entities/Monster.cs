using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities
{
    class Monster : Entity
    {
        public string Name { get; set; }

        public Monster(string name, int hp) : base(hp)
        {
            Name = name;
        }
    }
}
