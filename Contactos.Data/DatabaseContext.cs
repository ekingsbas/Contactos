using Contactos.Data.Configurations;
using Contactos.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Contactos.Data
{
    public class DatabaseContext : DbContext
    {

        private readonly string cnString;
        public DatabaseContext(string connString)
        {
           
            cnString = connString;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ContactConfiguration());
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(cnString);
        }

        public DbSet<Contact> Contact { get; set; }
    }
}
