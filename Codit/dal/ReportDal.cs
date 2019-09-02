using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Codit.Models;
namespace Codit.dal
{
    public class ReportDal: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Report>().ToTable("Reports");
        }
        public DbSet<Report> reportslist { get; set; }
    }
}