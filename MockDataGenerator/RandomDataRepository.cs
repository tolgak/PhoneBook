using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Bogus;
using PhoneBook.Repository.Entities;
using PhoneBook.Repository;

namespace MockDataGenerator
{
  public class RandomDataRepository
  {
    private class ValueHolder { public string Value { get; set; } }

    public IEnumerable<PhoneBook.Repository.Entities.Person> GetPeople()
    {
      Randomizer.Seed = new Random(123456);
      var index = new int[3] { 2, 4, 8 };

      var infoGenerator = new Faker<ContactInfo>()
          .RuleFor(o => o.Id, f => f.Random.Uuid())
          .RuleFor(o => o.ContactInfoType, f => f.PickRandom(index));

      var personGenerator = new Faker<PhoneBook.Repository.Entities.Person>()
          .RuleFor(c => c.Id, f => f.Random.Uuid())
          .RuleFor(c => c.First_Name, f => f.Person.FirstName)
          .RuleFor(c => c.Last_Name, f => f.Person.LastName)
          .RuleFor(c => c.Company, f => f.Company.CompanyName())
          .RuleFor(c => c.ContactInfos, f => infoGenerator.Generate(f.Random.Number(2)).ToList());

      var people = personGenerator.Generate(100);
      foreach (var p in people)
      {
        foreach (var i in p.ContactInfos)
        {
          i.PersonId = p.Id;
          i.Info = MockInfo(i.ContactInfoType);
        }
      }

      return people;
    }

    private string MockInfo(int infoType)
    {
      var generator = infoType switch
      {
        2 => new Faker<ValueHolder>().RuleFor(o => o.Value, f => f.Phone.PhoneNumber()),
        4 => new Faker<ValueHolder>().RuleFor(o => o.Value, f => f.Internet.Email()),
        8 => new Faker<ValueHolder>().RuleFor(o => o.Value, f => MockGeolocation(f.Random.Double(), f.Random.Double())),

        _ => throw new ArgumentOutOfRangeException($"Not a valid contact info type: {infoType}")
      };
        
      return generator.Generate(1).First().Value;
    }

    private string MockGeolocation(double u, double v) 
    {
      var a = new Location{ Latitude = Math.Round((u * 180) - 90, 4), Longitude = Math.Round((v * 360) - 180, 4) };
      return JsonSerializer.Serialize(a);
    }




  }
}
