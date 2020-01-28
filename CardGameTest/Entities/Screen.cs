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
            Console.WriteLine($"----------------------------------------\nMonster HP: {monsterAux.CurrentHp}/{monsterAux.MaxHp}\nPlayer HP: {playerAux.CurrentHp}/{playerAux.MaxHp}");
            PrintPlayerHand(playerAux);
            PlayerAction(playerAux);
        }

        private void PlayerAction(Player player)
        {
            //TEMPORARY
            Console.Write("\n(TEMP!)Select card to play: ");

            Card card = player.PlayerBag.GetCardAt(int.Parse(Console.ReadLine()) - 1);
            Game.PlayCard(card, 4);
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
