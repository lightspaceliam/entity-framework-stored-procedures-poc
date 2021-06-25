using Entities;
using Entities.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Service
{
    public class PersonService : IEntityService<Person>
    {
        protected readonly EfStoredProcedureDbContext _context;
        protected readonly ILogger<Person> _logger;

        public PersonService(
            EfStoredProcedureDbContext context,
            ILogger<Person> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Person> InsertAsync(Person entity)
        {
            var firstName = new SqlParameter("firstName", entity.FirstName);
            var lastName = new SqlParameter("lastName", entity.LastName);

            var people = await _context.People
                .FromSqlRaw("EXECUTE dbo.InsertPerson @firstName, @lastName", firstName, lastName)
                .ToListAsync();

            return people
                .FirstOrDefault();
        }

        public async Task<List<Person>> FindAsync()
        {
            var id = new SqlParameter("id", DBNull.Value);

            var people = await _context.People
                .FromSqlRaw("EXECUTE dbo.FindPeople @id", id)
                .ToListAsync();

            return people;
        }

        public async Task<Person> FindByIdAsync(int id)
        {
            var personId = new SqlParameter("id", id);

            var people = await _context.People
                .FromSqlRaw("EXECUTE dbo.FindPeople @id", personId)
                .ToListAsync();

            return people
                .FirstOrDefault();
        }

        public async Task<Person> UpdateAsync(Person entity)
        {
            var personId = new SqlParameter("id", entity.Id);
            var firstName = new SqlParameter("firstName", entity.FirstName);
            var lastName = new SqlParameter("lastName", entity.LastName);

            var people = await _context.People
                .FromSqlRaw("EXECUTE dbo.UpdatePerson @id, @firstName, @lastName", personId, firstName, lastName)
                .ToListAsync();

            return people
                .FirstOrDefault();
        }

        public async Task DeleteAsync(int id)
        {
            var personId = new SqlParameter("id", id);
            
            await _context.Database
                .ExecuteSqlInterpolatedAsync($"EXECUTE dbo.DeletePerson {personId}");
        }
    }
}
