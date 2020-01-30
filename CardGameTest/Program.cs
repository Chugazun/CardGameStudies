using CardGameTest.Data;
using CardGameTest.Entities;
using CardGameTest.Entities.Cards;
using CardGameTest.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExercisesCore.Test_Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.GameInit(new SeedingService(new cardgamedbContext()));
            Random rand = new Random();
            Player player = new Player(30);
            Screen sc = new Screen();

            Game.StartCombat(player, new Monster("Test", rand.Next(10, 21)));
            Game.Damage(player, 2);

            if (Game.Initialized)
            {
                while (Game.GetCurrentMonster().CurrentHp > 0)
                {
                    sc.PrintScreen();

                }
            } else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("DATABASE UPDATED, SET _dbUpdated IN GAME CLASS TO TRUE");
                Console.ForegroundColor = aux;
            }
        }
    }
}
