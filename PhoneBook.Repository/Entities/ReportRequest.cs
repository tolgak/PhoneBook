using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public DateTime DateCompleted{ get; set; }
    public ReportStatus Status { get; set; }
    public string RequestLocation { get; set; }
  }


}
