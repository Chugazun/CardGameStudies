using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGameTest.Data;
using CardGameTest.Entities;
using CardGameTest.Entities.Cards;
using CardGameTest.Utils;

namespace CardGameTest.Services
{
    class SeedingService
    {
        private readonly cardgamedbContext _context;

        public SeedingService(cardgamedbContext context)
        {
            _context = context;
        }

        public bool Seed(bool dbUpdated)
        {
            if (dbUpdated) return true;

            _context.CardsName.AddRange(new CardName("Axe"));
            _context.SaveChanges();

            return true;
        }

        public string FindByName(string name)
        {
            return _context.CardsName.FirstOrDefault(c => c.Name.Contains(name)).ToString();
        }
    }
}
