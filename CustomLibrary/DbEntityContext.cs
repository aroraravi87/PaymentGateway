using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomLibrary.Models;

namespace CustomLibrary
{
    class DbEntityContext : DbContext
    {

        static DbEntityContext()
        {
            Database.SetInitializer<DbEntityContext>(null);
        }

        public DbEntityContext()
            : base("name=CustomerDBString")
        {
           
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Seller> Sellers { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using Fluent API here
            modelBuilder.Entity<Customer>().HasKey(e => e.ID);
            modelBuilder.Entity<Customer>().Property(e => e.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Customer>().MapToStoredProcedures();
            modelBuilder.Entity<Product>().MapToStoredProcedures();
            modelBuilder.Entity<Order>().MapToStoredProcedures();
            modelBuilder.Entity<Seller>().MapToStoredProcedures();
            base.OnModelCreating(modelBuilder);
        }

      
    }
}
