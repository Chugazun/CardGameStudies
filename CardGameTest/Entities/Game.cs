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
        static private StatusControl playerStatusC, monsterStatusC;
        static private Screen _screen;
        static private bool _dbUpdated = true, isValidAction = false;
        static private SeedingService _seedingService;
        static private int? dicePos;
        public static StringBuilder Log { get; set; } = new StringBuilder();
        public static bool Initialized { get; set; }
        public static int CardsUsed { get; set; }

        public static void StartCombat(Player player, Monster monster)
        {
            _player = player;
            playerStatusC = new StatusControl(_player);
            _monster = monster;
            monsterStatusC = new StatusControl(_monster);
            _player.Status.Poison = 2;
            _player.Status.ReEquip = 1;
            ResetPlayer();
            playerStatusC.HasTurnStart();
            playerStatusC.ActivateStatus();
        }

        public static void PlayCard(Entity entity, Card card, int diceVal, int dicePos)
        {
            if (!card.Used && card.ConditionCheck(diceVal) && playerStatusC.OnCardPlayStatus(card, diceVal)) card.act(diceVal);

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
                int selectedAction = _screen.RequestPlayerAction();
                checkSelection = VerifySelection(selectedAction);
            } while (!checkSelection);
            //Console.Clear();            
        }

        public static void UpdateScreen()
        {
            Console.Clear();
            _screen.PrintScreen(GetCurrentPlayer(), GetCurrentMonster());
        }

        public static void NewTurn()
        {
            //playerStatusC.CheckStatus();                    
            ResetPlayer();
            playerStatusC.HasTurnStart();
            playerStatusC.ActivateStatus();
            //UpdateScreen();
        }

        public static void EndTurn()
        {
            playerStatusC.ResetEndTurnStatus();
            monsterStatusC.ResetEndTurnStatus();
            NewTurn();
        }

        private static void ResetPlayer()
        {
            _player.Dice = RollDice(_player.DiceQuant);
            CardsUsed = 0;
            _player.PlayerBag.ResetHandCards();
        }

        public static List<Die> RollDice(int diceQuant)
        {
            List<Die> aux = new List<Die>();
            Random rand = new Random();

            for (int i = 0; i < diceQuant; i++)
            {
                aux.Add(new Die(rand.Next(1, 7)));
            }

            return aux;
        }

        private static void AttackAction()
        {
            Card selectedCard;
            do
            {
                Console.Write("\n(TEMP!)Select card to play: ");
                int handPos = int.Parse(Console.ReadLine()) - 1;

                selectedCard = _player.PlayerBag.GetCardAt(handPos).Used ? null : _player.PlayerBag.GetCardAt(handPos);
            } while (selectedCard == null);

            Console.WriteLine("\n" + selectedCard);

            int selectedDie;
            do
            {
                Console.Write("\n(TEMP!)Select desired die: ");
                dicePos = int.Parse(Console.ReadLine()) - 1;

                selectedDie = _player.Dice[dicePos.Value].IsLocked ? 0 : _player.Dice[dicePos.Value].GetValue(_player);
            } while (selectedDie == 0);
            if (_player.Dice[dicePos.Value].IsBurned) _player.Dice[dicePos.Value].IsBurned = false;

            if (_player.Status.Curse > 0 && !playerStatusC.Curse(selectedCard.ID))
            {
                UseDie(dicePos.Value);
                return;
            }

            PlayCard(_player, selectedCard, selectedDie, dicePos.Value);
        }

        public static void Damage(Entity target, int dmgVal)
        {
            int remainingDamage = target.DamageShield(dmgVal);
            if (remainingDamage > 0) target.TakeDamage(remainingDamage);
        }

        public static void TrueDamage(Entity target, int dmgVal) => target.TakeDamage(dmgVal);

        public static void Heal(Entity target, int healVal) => target.TakeHealing(healVal);

        public static void GainShield(Entity target, int shieldVal) => target.GainShield(shieldVal);

        public static void CreateDie(Entity target, int diceValue) => target.Dice.Add(new Die(diceValue));

        public static void CreateDie(Entity target, Die die) => target.Dice.Add(die);

        public static void ChangeDiceValue(Entity target, int newValue)
        {
            if (newValue <= 6)
            {
                target.Dice[dicePos.Value].Value = newValue;
            }
            else
            {
                target.Dice[dicePos.Value].Value = 6;
                CreateDie(target, newValue - 6);
            }
        }

        public static void SplitDiceValue(Entity target, int diceVal)
        {
            if (diceVal == 1)
            {
                if (target.Dice[dicePos.Value].IsBlinded)
                {
                    CreateDie(target, CreateBlindDie(1));
                    return;
                }
                CreateDie(target, 1);
                return;
            }
            Random rand = new Random();
            int aux = diceVal - rand.Next(1, diceVal);
            target.Dice[dicePos.Value].Value = aux;
            if (target.Dice[dicePos.Value].IsBlinded)
            {
                CreateDie(target, CreateBlindDie(diceVal - aux));
                return;
            }
            CreateDie(target, diceVal - aux);
        }

        private static Die CreateBlindDie(int diceVal)
        {
            Die aux = new Die(diceVal)
            {
                IsBlinded = true
            };
            return aux;
        }

        public static string CheckPlayerInfo(string playerStatus)
        {
            //if (playerStatusC.HasAny())
            //{

            //    //if (_player.Status.Shield > 0)
            //    //{
            //    //    if (!playerStatus.Contains("Shield"))
            //    //    {
            //    //        playerStatus = string.Concat(playerStatus, $"Shield: {_player.Status.Shield}   ");
            //    //        auxPShieldValue = _player.Status.Shield;
            //    //    }
            //    //    else
            //    //    {
            //    //        playerStatus = Regex.Replace(playerStatus, "Shield: " + auxPShieldValue, "Shield: " + _player.Status.Shield);
            //    //        auxPShieldValue = _player.Status.Shield;
            //    //    }                    
            //    //}
            //    //else if (_player.Status.Shield <= 0 && playerStatus.Contains("Shield"))
            //    //{
            //    //    playerStatus = Regex.Replace(playerStatus, "Shield: " + auxPShieldValue, "");
            //    //    auxPShieldValue = 0;
            //    //}
            //    //if (_player.Status.Poison > 0)
            //    //{
            //    //    if (!playerStatus.Contains("Poison"))
            //    //    {
            //    //        playerStatus = string.Concat(playerStatus, $"Poison: {_player.Status.Poison} ");                        
            //    //    }
            //    //    else
            //    //    {
            //    //        //playerStatus = Regex.Replace(playerStatus, "Shield: " + auxPShieldValue, "Shield: " + _player.Shield);                        
            //    //    }
            //    //}
            //    //else if (_player.Status.Poison <= 0 && playerStatus.Contains("Poison"))
            //    //{
            //    //    playerStatus.Remove(playerStatus.IndexOf("Poison: " + _player.Status.Poison));
            //    //}

            //}
            //else
            //{
            //    return "";
            //}
            playerStatus = playerStatusC.GetStatusInfo(playerStatus);
            return playerStatus.Trim();
        }

        public static string CheckMonsterInfo(string monsterStatus)
        {
            monsterStatus = monsterStatusC.GetStatusInfo(monsterStatus);
            return monsterStatus.Trim();
        }

        public static void RemoveShock(Entity target, int id)
        {
            if (target is Player) playerStatusC.RemoveShock(id);
        }

        public static Monster GetCurrentMonster()
        {
            return _monster;
        }

        public static Player GetCurrentPlayer()
        {
            return _player;
        }

        public static void GameInit(SeedingService seedingService, Screen screen)
        {
            _screen = screen;
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
                    EndTurn();
                    break;

                default:
                    return false;
            }
            return true;
        }

        public static int GetCurrentDicePos()
        {
            return dicePos.Value;
        }
    }
}
