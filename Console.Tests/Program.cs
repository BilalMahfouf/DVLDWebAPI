
using Core.Interfaces.Repositories.Common;
using DataAccessLayer;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

public  class Tests
{

    public static void test()
    {

    }
}
public class Program
{
    static async Task Main(string[] args)
    {

        Repository<Person> repo = new Repository<Person>(new DvldDBContext());
        var people = await repo.GetAllAsync();
        foreach(var person in people)
        {
            Console.WriteLine($"Full Name: {person.FirstName} {person.SecondName} {person.ThirdName} {person.LastName}");
        }

    }
}

