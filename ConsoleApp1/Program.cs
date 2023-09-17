using Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Channels;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
        IConfiguration _configuration = builder.Build();
        var myConnectionString1 = _configuration.GetConnectionString("MyConnection");

        Person p = new Person("jeffin", "reji");

        Person p1 = new Person();
        p1.Id = 1;
        //Console.WriteLine("what is ur name");
        p1.FirstName = "Jeffin";
        //Console.WriteLine("Whats is ur last name");
        p1.LastName = "Reji";
        //Console.WriteLine("whats is ur Email");
        p1.Email = "jr@2002";
        //Console.WriteLine("what is ur age");
        p1.Age = 21;
        p1.CanVote = p1.Age > 18;
        p1.FamilyName = "hello";

        Person p2 = new Person();
        p2.Id = 2;
        //Console.WriteLine("what is ur name");
        p2.FirstName = "Jenny";
        //Console.WriteLine("Whats is ur last name");
        p2.LastName = "Reji";
        //Console.WriteLine("whats is ur Email");
        p2.Email = "jennt@2007";
        //Console.WriteLine("what is ur age");
        p2.Age = 12;
        p2.CanVote = p2.Age > 18;
        p2.FamilyName = "welcome";






        ////Person p2 = new Person();
        ////Console.WriteLine("what is ur name");
        ////p2.FirstName =Console.ReadLine();
        ////Console.WriteLine("Whats is ur last name");
        ////p2.LastName = Console.ReadLine();
        ////Console.WriteLine("whats is ur Email");
        ////p2.Email = Console.ReadLine();
        ////Console.WriteLine("what is ur age");
        ////p2.Age = Convert.ToInt32(Console.Read());
        ////p2.CanVote=p2.Age >=18;





        Person p3 = new Person();
        //Console.WriteLine("what is ur name");
        p3.FirstName = "Rennish";
       // Console.WriteLine("Whats is ur last name");
        p3.LastName = "Joseph";
        //Console.WriteLine("whats is ur Email");
        p3.Email = "Rennishj@gmail.com";
        //Console.WriteLine("what is ur age");
        p3.Age = 25;
        p3.CanVote = p3.Age >= 18;

        ////Person p4 = new Person();
        ////Console.WriteLine("what is ur name");
        ////p4.FirstName =Console.ReadLine();
        ////Console.WriteLine("Whats is ur last name");
        ////p4.LastName = Console.ReadLine();
        ////Console.WriteLine("whats is ur Email");
        ////p4.Email = Console.ReadLine();
        ////Console.WriteLine("what is ur age");
        ////p4.Age = Convert.ToInt32(Console.Read());
        ////p4.CanVote=p4.Age >=18;


        List<Person> personlist = new List<Person>();
        personlist.Add(p1);
        personlist.Add(p2);
        personlist.Add(p3);
        //personlist.Add(p4);

        //foreach (Person person in personlist) 
        //{
        //    //PersonAccess pa1= new PersonAccess();
        //    ////string connection = System.Configuration.ConfigurationManager.ConnectionStrings("NetConnectionString");
        //    //pa1.ConnectionString=myConnectionString1;
        //    //pa1.AddPerson(person);
        //    //if (person.CanVote)
        //   //{
        //    //    Console.WriteLine($"ID:{person.Id} NAME:{p1.FirstName} LastName:{p1.LastName}  EMAIL:{p1.Email}  AGE:{p1.Age}  " +
        //    //        $"ELIGIBILITY:{p1.CanVote} TITLE:{person.Title} HasMobile:{person.HasMobile}");


        //    //}
        //}
        Func<string, string> selector = str => str.ToUpper();
        // Create an array of strings.
        string[] words = { "orange", "apple", "Article", "elephant" };
        // Query the array and select strings according to the selector method.
        IEnumerable<String> aWords = words.Select(selector);

        // Output the results to the console.
        foreach (String word in aWords)
            Console.WriteLine(word);

        IEnumerable<Person> filteredlist = personlist.Where(p => p.Age > 18);

        foreach (Person person in filteredlist)
        {
            Console.WriteLine($"ID:{person.Id} NAME:{person.FirstName}" +
                $" LastName:{person.LastName}  EMAIL:{person.Email}  AGE:{person.Age}  " +
        $"ELIGIBILITY:{person.CanVote}");
        }

        Console.ReadLine();  
        

    }
}