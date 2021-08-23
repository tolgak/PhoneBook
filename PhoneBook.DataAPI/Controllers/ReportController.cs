using Microsoft.AspNetCore.Mvc;
using PhoneBook.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PhoneBook.DataAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReportController : ControllerBase
  {
    private readonly IReportRequestRepository _reportRequestRepository;

    public ReportController(IReportRequestRepository reportRequestRepository)
    {
      _reportRequestRepository = reportRequestRepository;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Location location)
    {
      await _reportRequestRepository.Add(location);
      return Ok();
    }


  }
}
