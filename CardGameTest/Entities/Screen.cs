using System;
using System.Text;

namespace CardGameTest.Entities
{
    class Screen
    {
        string playerInfo = "";
        public void PrintScreen(Player currentPlayer, Monster currentMonster)
        {   
            string playerHp = $"Player HP: {currentPlayer.CurrentHp}/{currentPlayer.MaxHp} ";
            string monsterHp = $"Monster HP: {currentMonster.CurrentHp}/{currentMonster.MaxHp} ";
            
            string monsterInfo = "";

            playerInfo = Game.CheckPlayerInfo(playerInfo);
            Console.WriteLine($"----------------------------------------\n{monsterHp}  {monsterInfo}\n{playerHp}  {{{playerInfo}}}");
            PrintPlayerHand(currentPlayer);
            PrintPlayerDice(currentPlayer);
            PrintMenu();            
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

        public int RequestPlayerAction()
        {
            //TEMPORARY
            Console.Write("\nSelected desired action: ");
            return int.Parse(Console.ReadLine());
        }

        private void PrintMenu()
        {
            Console.WriteLine("\nActions");
            Console.WriteLine("1 - Attack.\n" +
                              "2 - End turn.");
        }
    }
}
