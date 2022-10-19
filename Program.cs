using System;
using System.Linq;
using System.Collections.Generic;

namespace SuncoastHumanResources
{
    class Employee
    {
        public string Name { get; set; }
        public int Department { get; set; }
        public int Salary { get; set; }
        public int MonthlySalary()
        {
            return Salary / 12;
        }
    }

    class Program
    {
        static void DisplayGreeting()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("    Welcome to Our Employee Database    ");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }

        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();

            return userInput;
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                return 0;
            }
        }

        static void Main(string[] args)
        {
            //first ability added, allows creation of NEW List of employeeS.
            var employees = new List<Employee>();

            var employee = new Employee();

            DisplayGreeting();

            //Should we keep showing the menu?
            var keepGoing = true;

            //While the user hasn't said QUIT yet..
            while (keepGoing)
            {
                //Insert a blank line then promt them and get their answer (force uppercase)
                Console.WriteLine();
                Console.Write("What do you want to do? (A)dd an employee or (Q)uit:");
                var choice = Console.ReadLine().ToUpper();

                if (choice == "Q")
                {
                    //They said quit, so set our keepGoing to false
                    keepGoing = false;
                }
                else
                {

                    //Make a new employee object

                    //Prompt for values and save them in the employee's properties
                    employee.Name = PromptForString("What is your name? ");
                    employee.Department = PromptForInteger("What is your department number? ");
                    employee.Salary = PromptForInteger("What is your yearly salary (in dollars)? ");

                    Console.WriteLine($"Hello, {employee.Name} you make {employee.MonthlySalary()} dollars per month.");

                    //Keep employee data around by:
                    employees.Add(employee);
                }
                // end of the 'while' statement
            }





            //end of Main
        }
    }
}