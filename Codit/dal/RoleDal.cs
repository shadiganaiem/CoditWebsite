using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Codit.Models;
namespace Codit.dal
{
    public class RoleDal :DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Roles>().ToTable("Roles");
        }
        public DbSet<Roles> rolesLst { get; set; }
    }
}