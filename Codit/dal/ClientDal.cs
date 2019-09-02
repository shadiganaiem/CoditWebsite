using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Codit.Models;
namespace Codit.dal
{
    public class ClientDal: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>().ToTable("Clients");
        }
        public DbSet<Client> clientsList { get; set; }
    }
}