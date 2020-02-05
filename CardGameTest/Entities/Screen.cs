using System;
using System.Text;

namespace CardGameTest.Entities
{
    class Screen
    {
        string playerInfo = "";
        public void PrintScreen()
        {
            Player playerAux = Game.GetCurrentPlayer();
            Monster monsterAux = Game.GetCurrentMonster();
            string playerHp = $"Player HP: {playerAux.CurrentHp}/{playerAux.MaxHp} ";
            string monsterHp = $"Monster HP: {monsterAux.CurrentHp}/{monsterAux.MaxHp} ";
            
            string monsterInfo = "";

            playerInfo = Game.CheckPlayerInfo(playerInfo);
            Console.WriteLine($"----------------------------------------\n{monsterHp}  {monsterInfo}\n{playerHp}  {{{playerInfo}}}");
            PrintPlayerHand(playerAux);
            PrintPlayerDice(playerAux);
            PrintMenu();
            Game.PlayerAction();
        }

        private void PrintPlayerHand(Player player)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nPlayer Hand: {{{player.PlayerBag.PlayerHand()}}}");
            Console.ForegroundColor = aux;
        }

        private void PrintPlayerDice(Player player)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nAvailable Dice: {{{player.PlayerDice()}}}");
            Console.ForegroundColor = aux;
        }

        private void PrintMenu()
        {
            Console.WriteLine("\nActions");
            Console.WriteLine("1 - Attack.\n" +
                              "2 - End turn.");
        }
    }
}
