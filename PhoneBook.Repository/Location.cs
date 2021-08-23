using System;

namespace PhoneBook.Repository
{

  public class MqPayload
  {
    public Location Location { get; set; }
    public Guid ReportId { get; set; }
    public string ContactInfoApiUrl{ get; set; }
    public string ReportApiUrl { get; set; }
  }

  public class Location
  {
    public double Latitude { get; set; }
    public double Longitude { get; set; }
  }

}
