using Microsoft.AspNetCore.Mvc;
using PhoneBook.Repository;
using PhoneBook.Repository.Entities;
using System;
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
    public async Task<ActionResult> GetAll()
    {
      var reports = await _reportRequestRepository.GetAll();
      return Ok(reports);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
      var report = await _reportRequestRepository.Get(id);
      if (report == null)
        return NotFound();

      return Ok(report);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Location location)
    {
      var apiBasePath = Request.Host.HasValue ? Request.Host.Value : string.Empty;
      var contactApi = $"http://{apiBasePath}/api/ContactInfo";
      var reportApi = $"http://{apiBasePath}/api/Report";

      var reportId = await _reportRequestRepository.Add(location);
      await _reportRequestRepository.Request(location, reportId, contactApi, reportApi);
      return Ok();
    }

    [HttpPatch]
    public async Task<ActionResult> Patch([FromBody] ReportPatch patch)
    {
      await _reportRequestRepository.PatchAsync(patch);
      return Ok();
    }



  }
}
