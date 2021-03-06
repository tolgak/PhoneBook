using Microsoft.AspNetCore.Mvc;
using PhoneBook.Repository;
using PhoneBook.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBook.DataAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PersonController : ControllerBase
  {
    private readonly IPersonRepository _personRepository;

    public PersonController(IPersonRepository personRepository)
    {
      _personRepository = personRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> Get()
    {
      var people = await _personRepository.GetAll();
      return Ok(people);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> Get(Guid id)
    {
      var person = await _personRepository.Get(id);
      if (person == null)
        return NotFound();

      return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Person person)
    {
      await _personRepository.Add(person);
      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] Person person)
    {
      await _personRepository.Update(id, person);
      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
      await _personRepository.Delete(id);
      return Ok();
    }
  }
}
