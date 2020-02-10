using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities
{
    class Die
    {
        public int Value { get; set; }
        public bool IsBurned { get; set; }
        public bool IsLocked { get; set; }

        public Die(int value)
        {
            Value = value;
            IsBurned = false;
            IsLocked = false;
        }

        public int GetValue(Entity target)
        {
            if (IsBurned) Game.Damage(target, 2);

            return Value;
        }

        public override string ToString()
        {            
            if (IsLocked) return "Locked";
            if (IsBurned) return Value.ToString() + "(B)";

            return Value.ToString();
        }
    }
}
