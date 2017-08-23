using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Demo
{
    public class DemoDbContext : DbContext
    {

        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
            Console.WriteLine("Accessing...");
            Console.WriteLine(options.ContextType);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localhost);Database=demo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MessageTag>()
                .HasKey(c => new { c.MessageId, c.Tag });
            
        }

        public DbSet<Message> Message { get; set; }

        public DbSet<MessageTag> MessageTag { get; set; }

    }
}
