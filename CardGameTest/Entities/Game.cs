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
        static private bool _dbUpdated = true, isValidAction = false;
        static private SeedingService _seedingService;
        static private int auxPShieldValue = 0;
        public static bool Initialized { get; set; }

        public static void StartCombat(Player player, Monster monster)
        {
            _player = player;
            _monster = monster;
            NewTurn();
        }

        public static void PlayCard(Card card, int diceVal, int dicePos)
        {
            if (!card.Used) card.act(diceVal);
            if (isValidAction) UseDie(dicePos);
        }

        private static void UseDie(int pos)
        {
            _player.Dice.RemoveAt(pos);
            isValidAction = false;
        }

        public static void PlayerAction()
        {
            bool checkSelection = true;

            do
            {
                //TEMPORARY
                Console.Write("\nSelected desired action: ");
                int selectedAction = int.Parse(Console.ReadLine());
                checkSelection = VerifySelection(selectedAction);
            } while (!checkSelection);
            Console.Clear();
        }

        public static void NewTurn()
        {
            _player.Dice = RollDice(_player.DiceQuant);
            _player.PlayerBag.ResetHandCards();
        }

        public static List<int> RollDice(int diceQuant)
        {
            List<int> aux = new List<int>();
            Random rand = new Random();

            for (int i = 0; i < diceQuant; i++)
            {
                aux.Add(rand.Next(1, 7));
            }

            return aux;
        }

        private static void AttackAction()
        {
            Console.Write("\n(TEMP!)Select card to play: ");

            Card selectedCard = _player.PlayerBag.GetCardAt(int.Parse(Console.ReadLine()) - 1);

            Console.Write("\n(TEMP!)Select desired die: ");
            int dicePos = int.Parse(Console.ReadLine()) - 1;
            int selectedDie = _player.Dice[dicePos];

            PlayCard(selectedCard, selectedDie, dicePos);
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
            }
            else
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

        public static void ValidAction()
        {
            isValidAction = true;
        }

        private static bool VerifySelection(int selection)
        {
            switch (selection)
            {
                case 1:
                    AttackAction();
                    break;

                case 2:
                    NewTurn();
                    break;

                default:
                    return false;
            }
            return true;
        }
    }
}
