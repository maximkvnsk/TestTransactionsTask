﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;
using TestTransactionsTask.Authentication;

namespace TestTransactionsTask.Models
{
    public class TestDBContext : IdentityDbContext<ApplicationUser>, ITestDBContext
    {

        public TestDBContext(DbContextOptions<TestDBContext> options) : base(options)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Data\\TestTransactionsDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                base.OnConfiguring(optionsBuilder);
            });
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        new public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}

