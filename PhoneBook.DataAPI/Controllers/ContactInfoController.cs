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
  public class ContactInfoController : ControllerBase
  {

    private readonly IContactInfoRepository _contactInfoRepository;

    public ContactInfoController(IContactInfoRepository contactInfoRepository)
    {
      _contactInfoRepository = contactInfoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactInfo>>> Get()
    {
      var contactInfos = await _contactInfoRepository.GetAll();
      return Ok(contactInfos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContactInfo>> Get(Guid id)
    {
      var contactInfo = await _contactInfoRepository.Get(id);
      if (contactInfo == null)
        return NotFound();

      return Ok(contactInfo);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ContactInfo contactInfo)
    {
      await _contactInfoRepository.Add(contactInfo);
      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] ContactInfo contactInfo)
    {
      await _contactInfoRepository.Update(id, contactInfo);
      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
      await _contactInfoRepository.Delete(id);
      return Ok();
    }
  }
}
