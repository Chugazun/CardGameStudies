using System;
using System.Text;

namespace CardGameTest.Entities
{
    class Screen
    {
        public void PrintScreen()
        {
            Player playerAux = Game.GetCurrentPlayer();
            Monster monsterAux = Game.GetCurrentMonster();
            string playerHp = $"Player HP: {playerAux.CurrentHp}/{playerAux.MaxHp} ";
            string monsterHp = $"Monster HP: {monsterAux.CurrentHp}/{monsterAux.MaxHp} ";
            string playerInfo = "";
            string monsterInfo = "";
            
            playerInfo = Game.CheckPlayerInfo(playerInfo);
            Console.WriteLine($"----------------------------------------\n{monsterHp}  {monsterInfo}\n{playerHp}  {playerInfo}");
            PrintPlayerHand(playerAux);
            Game.PlayerAction();
        }

        private void PrintPlayerHand(Player player)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nPlayer Hand: {{{player.PlayerBag}}}");
            Console.ForegroundColor = aux;
        }
    }
}
