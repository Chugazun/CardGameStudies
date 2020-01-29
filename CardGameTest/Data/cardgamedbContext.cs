using System;
using CardGameTest.Entities;
using CardGameTest.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CardGameTest.Data
{
    public partial class cardgamedbContext : DbContext
    {

        public DbSet<Card> Cards { get; set; }
        public DbSet<CardName> CardsName { get; set; }


        public cardgamedbContext()
        {
        }

        public cardgamedbContext(DbContextOptions<cardgamedbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;user id=root;password=22aa55bba2018;database=cardgamedb", x => x.ServerVersion("8.0.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
