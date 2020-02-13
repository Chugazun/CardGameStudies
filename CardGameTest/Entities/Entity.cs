using CardGameTest.Utils;
using System;
using System.Collections.Generic;

namespace CardGameTest.Entities
{
    abstract class Entity
    {
        public int MaxHp { get; protected set; }
        public int CurrentHp { get; protected set; }
        public List<int> DiceOld { get; set; } = new List<int>();
        public List<Die> Dice { get; set; } = new List<Die>();
        public StatusSheet Status { get; set; }

        protected Entity(int hp)
        {
            MaxHp = hp;
            CurrentHp = MaxHp;
            Status = new StatusSheet();
        }

        public void TakeDamage(int dmgVal)
        {
            CurrentHp -= (dmgVal - Status.Resistance);
        }

        public void TakeHealing(int healVal)
        {
            CurrentHp += healVal;
        }

        public void GainShield(int shieldVal)
        {
            Status.Shield += shieldVal;
        }

        public int DamageShield(int dmgVal)
        {
            int aux = dmgVal;
            dmgVal -= Status.Shield;
            Status.Shield = Math.Max(Status.Shield - aux, 0);
            return dmgVal;
        }

        public virtual List<Card> GetCards()
        {
            return null;
        }

        public virtual Card GetCardAt(int handPos)
        {
            return null;
        }
    }
}
