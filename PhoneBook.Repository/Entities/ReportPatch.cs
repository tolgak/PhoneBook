using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Repository.Entities
{

  public class ReportPatch
  {
    public Guid Id { get; set; }
    public DateTime? DateCompleted { get; set; }
    public ReportStatus Status { get; set; }
    public int cntPeopleNearBy { get; set; }
    public int cntPhonesNearBy { get; set; }
  }
}
