﻿using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ExpenseTrackerDbContext:DbContext
    {
        public DbSet<Expense>Expenses { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Account>Accounts { get; set; }



        public ExpenseTrackerDbContext(DbContextOptions options) : base(options)
        {
            
        }
        
    }
}
