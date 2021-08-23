namespace MockDataGenerator
{
  class Program
  {
    static void Main()
    {
      var repository = new RandomDataRepository();
      var people = repository.GetPeople();

      //File.WriteAllText("data.json", JsonSerializer.Serialize(people), Encoding.UTF8);

      using (var context = new ConsoleDataContext())
      {
        context.People.AddRange(people);
        context.SaveChanges();
      }

    }

  }


}
