using Entities;
using Entity.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PersonController : ControllerBase
    {
        private readonly IEntityService<Person> _entityService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(
            IEntityService<Person> entityService,
            ILogger<PersonController> logger)
        {
            _entityService = entityService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Index()
        {
            var people = await _entityService.FindAsync();

            return Ok(people);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Person>> FindById(int id)
        {
            var person = await _entityService.FindByIdAsync(id);

            if(person == null)
            {
                _logger.LogWarning($"{typeof(Person).Name}: {id} not found.");
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost("new")]
        public async Task<ActionResult<Person>> New(Person person)
        {
            try
            {
                var response = await _entityService.InsertAsync(person);

                return CreatedAtAction(nameof(FindById), new { id = response.Id }, response);
            }
            catch (DbUpdateException ex)
            {
                // TODO: not for production. Requires more granular strategy. Also, MVP stored procedure, not appropriately written. Out of scope.
                _logger.LogCritical(ex, $"{typeof(DbUpdateException).Name} raised whist attempting to create {typeof(Person).Name}.");
                return BadRequest();
            }
            
        }

        [HttpPut("{id:int}/update")]
        public async Task<ActionResult<Person>> Update(int id, Person person)
        {
            if(id != person.Id)
            {
                return BadRequest();
            }

            try
            {
                var response = await _entityService.UpdateAsync(person);
                return Ok(response);

                /*
                 * Recommended by official MS Docs: Tutorial: Create a web API with ASP.NET Core
                 * https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio
                 * 
                 * But I like to see the results of my update operation.
                 */
                //return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // TODO: not for production. Requires more granular strategy. Also, MVP stored procedure, masks concurrency. Out of scope.
                _logger.LogCritical(ex, $"{typeof(DbUpdateConcurrencyException).Name} raised whist attempting to update {typeof(Person).Name}.");
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<ActionResult<Person>> Delete(int id)
        {
            var person = await _entityService.FindByIdAsync(id);
            if(person == null)
            {
                return NotFound();
            }

            try
            {
                await _entityService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // TODO: not for production. Requires more granular strategy. Also, MVP stored procedure. Out of scope.
                _logger.LogCritical(ex, $"{typeof(DbUpdateException).Name} raised whist attempting to delete {typeof(Person).Name}.");
                return BadRequest();
            }
        }
    }
}
