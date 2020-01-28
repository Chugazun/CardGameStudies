using CardGameTest.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Entities
{
    class Game
    {
        static private Player _player;
        static private Monster _monster;
        static private bool _dbUpdated = true;
        static private SeedingService _seedingService;

        public static void StartCombat(Player player, Monster monster)
        {
            _player = player;
            _monster = monster;
        }
        
        public static void PlayCard(Card card, int diceVal)
        {
            if (!card.checkUsed()) card.act(diceVal);
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

        public static void GameInit(SeedingService seedingService)
        {
            _seedingService = seedingService;
            _dbUpdated = _seedingService.Seed(_dbUpdated);
        }

        public static Card GetCardFromDb(string name)
        {
            Card card = _seedingService.FindByName(name);
            return card;
        }
    }
}
