using CardGameTest.Utils;
using System.Collections.Generic;

namespace CardGameTest.Entities
{
    abstract class Entity
    {
        public int MaxHp { get; protected set; }
        public int CurrentHp { get; protected set; }        
        public List<int> Dice { get; set; } = new List<int>();
        public StatusSheet Status { get; set; }

        protected Entity(int hp)
        {
            MaxHp = hp;
            CurrentHp = MaxHp;
            Status = new StatusSheet();            
        }

        public void TakeDamage(int dmgVal)
        {
            CurrentHp -= dmgVal;
        }

        public void TakeHealing(int healVal)
        {
            CurrentHp += healVal;
        }

        public void GainShield(int shieldVal)
        {
            Status.Shield += shieldVal;
        }
    }
}
