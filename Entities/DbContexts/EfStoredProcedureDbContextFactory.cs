using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Entities.DbContexts
{
    public class EfStoredProcedureDbContextFactory : IDesignTimeDbContextFactory<EfStoredProcedureDbContext>
    {
        public EfStoredProcedureDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfStoredProcedureDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=EfStoredProcedureDbContext;Integrated Security=True;MultipleActiveResultSets=True");

            return new EfStoredProcedureDbContext(optionsBuilder.Options);
        }
    }
}
