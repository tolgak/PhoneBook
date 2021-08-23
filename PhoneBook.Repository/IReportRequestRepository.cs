using PhoneBook.Repository.Entities;
using System;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
  public interface IReportRequestRepository
  {
    Task<Guid> Add(Location location);
    Task Request(Location location, Guid reportId, string contactApi, string reportApiUrl);
    Task PatchAsync(ReportPatch patch);
  }
}
