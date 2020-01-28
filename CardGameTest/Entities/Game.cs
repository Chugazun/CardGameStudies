using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities
{
    class Game
    {
        static private Player _player;
        static private Monster _monster;

        public static void StartCombat(Player player, Monster monster)
        {
            _player = player;
            _monster = monster;
        }
        
        public static void PlayCard(Card card, int diceVal)
        {
            if (!card.Used) card.Action(diceVal);
        }

        public static void Damage(Entity target, int dmgVal) => target.TakeDamage(dmgVal);

        public static void Heal(Entity target, int healVal) => target.TakeHealing(healVal);

        public static Monster GetCurrentMonster()
        {
            return _monster;
        }

        public static Player GetCurrentPlayer()
        {
            return _player;
        }
    }
}
