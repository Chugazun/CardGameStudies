using CardGameTest.Entities;
using CardGameTest.Entities.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExercisesCore.Test_Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            Player player = new Player(30);
            Screen sc = new Screen();

            Game.StartCombat(player, new Monster("Test", rand.Next(10, 21)));
            player.PlayerBag.AddCard(new Potion());
            while (Game.GetCurrentMonster().CurrentHp > 0)
            {
                sc.PrintScreen();
            }
        }
    }
}
