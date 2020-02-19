using CardGameTest.Utils;
using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;

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

        public int TakeHealing(int healVal)
        {
            if (CurrentHp != MaxHp)
            {
                if (CurrentHp + healVal < MaxHp)
                {
                    CurrentHp += healVal;
                }
                else
                {
                    healVal = MaxHp - CurrentHp;
                    CurrentHp = MaxHp;
                }
                return healVal;
            }
            else
            {
                return 0;
            }
        }

        public void GainShield(int shieldVal)
        {
            Status.Shield += shieldVal;
        }

        public int DamageShield(int dmgVal)
        {
            int aux = dmgVal;
            dmgVal -= Status.Shield;
            if (Status.Shield > 0) Game.Log.AppendLine($"{GetType().Name} lost {Math.Min(aux, Status.Shield)} Shield".Pastel(Color.Orange));
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
