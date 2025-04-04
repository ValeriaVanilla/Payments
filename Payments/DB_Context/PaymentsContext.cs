using Payments.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Payments.DB_Context
{
    public class PaymentsContext : DbContext 
    {
        public PaymentsContext():base("name=paymentsDB") {
        
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<PaymentsContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet <payment> payments { get; set; }

        public DbSet <account> accounts { get; set; }

    }
}