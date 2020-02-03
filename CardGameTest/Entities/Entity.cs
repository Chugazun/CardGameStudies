using System.Collections.Generic;

namespace CardGameTest.Entities
{
    abstract class Entity
    {
        public int MaxHp { get; protected set; }
        public int CurrentHp { get; protected set; }
        public int Shield { get; set; }
        public List<int> Dice { get; set; } = new List<int>();

        protected Entity(int hp)
        {
            MaxHp = hp;
            CurrentHp = MaxHp;
        }

        public void TakeDamage(int dmgVal)
        {
            CurrentHp -= dmgVal;
        }

        public void TakeHealing(int healVal)
        {
            CurrentHp += healVal;
        }

        public void GetShield(int shieldVal)
        {
            Shield += shieldVal;
        }
    }
}
