using System;

namespace PhoneBook.Repository.Entities
{
  public enum ReportStatus
  {
    Undefined = 0,
    InProgress = 1,
    Completed = 2,
  }

  public class ReportRequest
  {
    public Guid Id { get; set; }
    public DateTime DateRequested { get; set; }
    public DateTime? DateCompleted { get; set; }
    public ReportStatus Status { get; set; }
    public string RequestLocation { get; set; }
    public int cntPeopleNearBy{ get; set; }
    public int cntPhonesNearBy { get; set; }
  }


}
