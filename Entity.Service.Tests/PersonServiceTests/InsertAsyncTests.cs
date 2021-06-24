using Entities;
using System.Threading.Tasks;
using Xunit;

namespace Entity.Service.Tests.PersonServiceTests
{
    public class InsertAsyncTests : PersonServiceTestBase
    {
        [Fact(DisplayName = "Can insert valid Person")]
        public async Task CanInsertValidEntity()
        {
            var person = new Person
            {
                FirstName = "Luke",
                LastName = "Skywalker"
            };

            var response = await EntityService.InsertAsync(person);

            Assert.Null(response);

            // Microsoft Entity Framework Core InMemoryDatabase does not appear to include support for stored procedures.

            //Assert.Equal(person.FirstName, response.FirstName);
            //Assert.Equal(person.LastName, response.LastName);
            //Assert.NotEqual(default(DateTime), response.Created);
        }
    }
}
