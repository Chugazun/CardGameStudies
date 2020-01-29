using CardGameTest.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Entities
{
    class Game
    {
        static private Player _player;
        static private Monster _monster;
        static private bool _dbUpdated = true;
        static private SeedingService _seedingService;
        static private int auxPShieldValue = 0;
        public static bool Initialized { get; set; }

        public static void StartCombat(Player player, Monster monster)
        {
            _player = player;
            _monster = monster;
        }

        public static void PlayCard(Card card, int diceVal)
        {
            if (!card.Used) card.act(diceVal);
        }

        public static void PlayerAction()
        {
            //TEMPORARY
            Console.Write("\n(TEMP!)Select card to play: ");

            Card card = _player.PlayerBag.GetCardAt(int.Parse(Console.ReadLine()) - 1);
            PlayCard(card, 3);
        }

        public static void Damage(Entity target, int dmgVal) => target.TakeDamage(dmgVal);

        public static void Heal(Entity target, int healVal) => target.TakeHealing(healVal);

        public static void GainShield(Entity target, int shieldVal) => target.GetShield(shieldVal);

        public static string CheckPlayerInfo(string playerStatus)
        {

            StatusControl status = new StatusControl(_player);

            if (status.HasAny())
            {                
                if (_player.Shield > 0)
                {
                    if (!playerStatus.Contains("Shield"))
                    {
                        playerStatus = string.Concat(playerStatus, $"Shield: {_player.Shield}, ");
                        auxPShieldValue = _player.Shield;
                    }
                    else
                    {
                        playerStatus = Regex.Replace(playerStatus, "Shield: " + auxPShieldValue, "Shield: " + _player.Shield);
                        auxPShieldValue = _player.Shield;
                    }
                }               
            } else
            {
                return "";
            }

            return "{ " + playerStatus.Substring(0, playerStatus.Length - 2) + " }";
        }

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
            Initialized = _dbUpdated;
            _seedingService = seedingService;
            _dbUpdated = _seedingService.Seed(_dbUpdated);

        }

        public static string GetCardNameFromDb(string name)
        {
            return _seedingService.FindByName(name);
        }


    }
}
