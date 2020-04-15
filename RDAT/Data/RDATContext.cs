using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDAT.Models;

namespace RDAT.Data
{
    public class RDATContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        // RDATContext : DbContext
        private Func<object, object> p;

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Batch> Batches { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<TestingLog> TestingLogs { get; set; }


        //public RDATContext(DbContextOptions<RDATContext> options)
        //: base(options)
        //{ }

        public RDATContext() {
        }

        public RDATContext(Func<object, object> p)
        {
            this.p = p;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:nfssql01.database.windows.net,1433;Initial Catalog=NFS_SQL_01;Persist Security Info=False;User ID=nfsadmin;Password=IFTAtaxes2017;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<RDAT.Models.CreateBatch> CreateBatch { get; set; }

        
    }
}
