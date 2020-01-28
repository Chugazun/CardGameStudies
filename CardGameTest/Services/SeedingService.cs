using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGameTest.Data;
using CardGameTest.Entities;
using CardGameTest.Entities.Cards;

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
            
            _context.Cards.Add(new Sword());
            _context.SaveChanges();

            return true;
        }

        public Card FindByName(string name)
        {
            return _context.Cards.FirstOrDefault(c => c.Name.Contains(name));
        }
    }
}
