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
                Console.Write("What do you want to do?\n(A)dd an employee\n(F)ind an employee\n(S)how all the employees\n(Q)uit\n:");
                var choice = Console.ReadLine().ToUpper();

                if (choice == "Q")
                {
                    //They said quit, so set our keepGoing to false
                    keepGoing = false;
                }
                else
                if (choice == "F")
                {
                    Console.WriteLine("FINDING");
                    // - Create a varialbe named **'foundEmployee'**  null value
                    Employee foundEmployee = null;

                    // Prompt for the name
                    var nameToSearchFor = PromptForString("What name are you looking for?");
                    //Show the use of LINQ method shortcut style to search for something.!SUPERPOWER.!!!!!!
                    Employee foundEmployee = employees.FirstOrDefault(employee => employee.Name == nameToSearchFor);

                    /*/Loop through the list to look for a match
                    foreach (var employee in employees)
                    {
                        //If we find one, update 'foundEmployee'
                        if (employee.Name == nameToSearchFor)
                        {
                            foundEmployee = employee;
                        }
                    }*///the above is no longer need this look when we utilize LINQ method FirstOrDefault().

                    //After the loop, 'foundEmployee' is either 'null' (not found) or refers to the matching item
                    if (foundEmployee == null)
                    {
                        Console.WriteLine("No such person!");
                    }
                    //Show a message if 'null', otherwise show the details.


                }

                else//the following adds Show employee 
                if (choice == "S")
                {
                    // READ(out of CREATE - READ - UPDATE - DELETE)
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"{employee.Name} is in department {employee.Department} and makes ${employee.Salary}");
                    }
                }
                else
                {

                    //Make a new employee object

                    //Prompt for values and save them in the employee's properties
                    employee.Name = PromptForString("What is your name? ");
                    employee.Department = PromptForInteger("What is your department number? ");
                    employee.Salary = PromptForInteger("What is your yearly salary (in dollars)? ");

                    Console.WriteLine($"Hello, {employee.Name} you make {employee.MonthlySalary()} dollars per month.");

                    // Add it to the list
                    employees.Add(employee);
                }
                // end of the 'while' statement
            }





            //end of Main
        }
    }
}