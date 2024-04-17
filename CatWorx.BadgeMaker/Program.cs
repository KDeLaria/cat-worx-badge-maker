using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
    class Program
    {
        // Get Employees Method
        static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            while (true)
            {
                // Move the initial prompt inside the loop, so it repeats for each employee
                Console.WriteLine("Enter first name (leave empty to exit): ");

                // change input to firstName
                string firstName = Console.ReadLine() ?? "";
                if (firstName == "")
                {
                    break;
                }

                // add a Console.ReadLine() for each value
                Console.Write("Enter last name: ");
                string lastName = Console.ReadLine() ?? "";
                Console.Write("Enter ID: ");
                int id = Int32.Parse(Console.ReadLine() ?? "");
                Console.Write("Enter Photo URL:");
                string photoUrl = Console.ReadLine() ?? ""; // "https://loremflickr.com/400/400";
                Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);
                employees.Add(currentEmployee);
            }

            return employees;
        }

        async static Task Main(string[] args)
        {
            Console.WriteLine("Do you wish to download (randomized) data from the API?");
            String answer = Console.ReadLine() ?? "";
            List<Employee> employees;
            if (answer == "y"){
                employees = await PeopleFetcher.GetFromApi();
            }
            else {
                employees = GetEmployees();
            }

            Util.WriteToCSV(employees);
            Util.PrintEmployees(employees);
            await Util.MakeBadges(employees);
        }
    }
}


