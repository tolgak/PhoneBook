using Microsoft.AspNetCore.Mvc;
using PhoneBook.Dto;
using PhoneBook.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

    // GET: api/<PersonController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<dtoPerson>>> Get()
    {
      var people = await _personRepository.GetAll();
      return Ok(people);
    }

    // GET api/<PersonController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<dtoPerson>> Get(Guid id)
    {
      var person = await _personRepository.Get(id);
      if (person == null)
        return NotFound();

      return Ok(person);
    }

    // POST api/<PersonController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] dtoPerson person)
    {
      await _personRepository.Add(person);
      return Ok();
    }

    // PUT api/<PersonController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] dtoPerson person)
    {
      await _personRepository.Update(id, person);
      return Ok();
    }

    // DELETE api/<PersonController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
      await _personRepository.Delete(id);
      return Ok();
    }
  }
}
