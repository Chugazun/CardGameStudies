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
        static private StatusControl playerStatusC;
        static private bool _dbUpdated = true, isValidAction = false;
        static private SeedingService _seedingService;
        static private int auxPShieldValue = 0;
        static private int? dicePos;
        public static bool Initialized { get; set; }
        public static int CardsUsed { get; set; }

        public static void StartCombat(Player player, Monster monster)
        {
            _player = player;
            playerStatusC = new StatusControl(_player);
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
            bool checkSelection;
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
            Game.CardsUsed = 0;
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
            Console.WriteLine("\n" + selectedCard);

            Console.Write("\n(TEMP!)Select desired die: ");
            dicePos = int.Parse(Console.ReadLine()) - 1;
            int selectedDie = _player.Dice[dicePos.Value];

            PlayCard(selectedCard, selectedDie, dicePos.Value);
        }

        public static void Damage(Entity target, int dmgVal) => target.TakeDamage(dmgVal);

        public static void Heal(Entity target, int healVal) => target.TakeHealing(healVal);

        public static void GainShield(Entity target, int shieldVal) => target.GainShield(shieldVal);

        public static void CreateDie(Entity target, int diceValue) => target.Dice.Add(diceValue);

        public static void ChangeDiceValue(Entity target, int newValue)
        {
            if (newValue <= 6)
            {
                target.Dice[dicePos.Value] = newValue;
            }
            else
            {
                target.Dice[dicePos.Value] = 6;
                CreateDie(target, newValue - 6);
            }
        }

        public static void SplitDiceValue(Entity target, int diceVal)
        {
            if (diceVal == 1)
            {
                CreateDie(target, 1);
                return;
            }
            Random rand = new Random();
            int aux = diceVal - rand.Next(1, diceVal);
            target.Dice[dicePos.Value] = aux;
            CreateDie(target, diceVal - aux);
        }

        public static string CheckPlayerInfo(string playerStatus)
        {
            

            if (playerStatusC.HasAny())
            {
                //if (_player.Status.Shield > 0)
                //{
                //    if (!playerStatus.Contains("Shield"))
                //    {
                //        playerStatus = string.Concat(playerStatus, $"Shield: {_player.Status.Shield}   ");
                //        auxPShieldValue = _player.Status.Shield;
                //    }
                //    else
                //    {
                //        playerStatus = Regex.Replace(playerStatus, "Shield: " + auxPShieldValue, "Shield: " + _player.Status.Shield);
                //        auxPShieldValue = _player.Status.Shield;
                //    }                    
                //}
                //else if (_player.Status.Shield <= 0 && playerStatus.Contains("Shield"))
                //{
                //    playerStatus = Regex.Replace(playerStatus, "Shield: " + auxPShieldValue, "");
                //    auxPShieldValue = 0;
                //}
                //if (_player.Status.Poison > 0)
                //{
                //    if (!playerStatus.Contains("Poison"))
                //    {
                //        playerStatus = string.Concat(playerStatus, $"Poison: {_player.Status.Poison} ");                        
                //    }
                //    else
                //    {
                //        //playerStatus = Regex.Replace(playerStatus, "Shield: " + auxPShieldValue, "Shield: " + _player.Shield);                        
                //    }
                //}
                //else if (_player.Status.Poison <= 0 && playerStatus.Contains("Poison"))
                //{
                //    playerStatus.Remove(playerStatus.IndexOf("Poison: " + _player.Status.Poison));
                //}
                playerStatus = playerStatusC.GetStatusInfo(playerStatus);
                Console.WriteLine(_player.Status.Shield);
            }
            else
            {
                return "";
            }            
            return playerStatus.Trim();
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
