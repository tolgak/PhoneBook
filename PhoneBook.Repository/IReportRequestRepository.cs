using PhoneBook.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
  public interface IReportRequestRepository
  {
    Task<ReportRequest> Get(Guid id);
    Task<IEnumerable<ReportRequest>> GetAll();
    Task<Guid> Add(Location location);
    Task Request(Location location, Guid reportId, string contactApi, string reportApiUrl);
    Task PatchAsync(ReportPatch patch);
  }
}
