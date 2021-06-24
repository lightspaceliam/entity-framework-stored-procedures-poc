using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DbContexts
{
    public class EfStoredProcedureDbContext : DbContext
    {
        public EfStoredProcedureDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions) { }

        public EfStoredProcedureDbContext() { }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
