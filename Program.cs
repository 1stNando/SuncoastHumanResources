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
            DisplayGreeting();

            //Should we keep showing the menu?      ????????????
            var keepGoing = true;

            //While the user hasn't said QUIT yet..
            while (keepGoing)
            {
                //Insert a blank line then promt them and get their answer (force uppercase)
                Console.WriteLine();
                Console.Write("What do you want to do?\n(A)dd an employee\n(D)elete an employee\n(F)ind an employee\n(U)pdate an employee\n(S)how all the employees\n(Q)uit\n:");
                var choice = Console.ReadLine().ToUpper();

                if (choice == "Q")
                {
                    //They said quit, so set our keepGoing to false
                    keepGoing = false;
                }
                else
                if (choice == "D")
                {
                    //THIS IS DELETE - out of (CREATE, READ, UPDATE, DELETE)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                    //get employee name
                    var nameToSearchFor = PromptForString("What name are you looking for? ");

                    //Search database to see if they exist!
                    Employee foundEmployee = employees.FirstOrDefault(employee => employee.Name == nameToSearchFor);
                    //If we did not find an employee
                    if (foundEmployee == null)
                    {
                        //Show that the person doesn't exist
                        Console.WriteLine("No such employee! ");
                    }
                    //If we found an employee
                    else
                    {
                        // - We did find the employee
                        // - Show the details
                        Console.WriteLine($"{foundEmployee.Name} is in department {foundEmployee.Department} and makes ${foundEmployee.Salary}");
                        // - Ask to confirm
                        var confirm = PromptForString($"Are you sure you want to delete {foundEmployee.Name}? [Y/N] ").ToUpper();
                        if (confirm == "Y")
                        {
                            //Delete them
                            employees.Remove(foundEmployee);
                        }
                    }
                }


                //this is the FIND employee functionality
                else if (choice == "F")
                {
                    // Prompt for the name
                    var nameToSearchFor = PromptForString("What name are you looking for? ");
                    //Show the use of LINQ method shortcut style to search for something.!SUPERPOWER.!!!!!!
                    Employee foundEmployee = employees.FirstOrDefault(employee => employee.Name == nameToSearchFor);

                    //After the loop, 'foundEmployee' is either 'null' (not found) or refers to the matching item
                    if (foundEmployee == null)
                    {
                        Console.WriteLine("No such person!");
                    }
                    else
                    {
                        //Show a message if 'null', 
                        //otherwise show the details.
                        Console.WriteLine($"{foundEmployee.Name} is in department {foundEmployee.Department} and makes ${foundEmployee.Salary}");
                    }
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
                if (choice == "U")
                {
                    //UPDATE - from CREATE, READ, UPDATE, DELETE!!!!!!!!!!!!!!!!!!
                    Console.WriteLine("UPDATING!");
                    //Get the employee name we are searching for
                    var nameToSearchFor = PromptForString("What name are you looking for? ");

                    //Search the database to see if they exist!
                    Employee foundEmployee = employees.FirstOrDefault(employee => employee.Name == nameToSearchFor);

                    // If we did NOT find anyone
                    if (foundEmployee == null)
                    {
                        //Show that the person doesn't exist
                        Console.WriteLine("No such employee! ");
                    }
                    //If we found an employee
                    else
                    {
                        Console.WriteLine($"{foundEmployee.Name} is in department {foundEmployee.Department} and makes ${foundEmployee.Salary}");
                        var changeChoice = PromptForString("What do you want to change [(N)ame/(D)epartment/(S)alary]?").ToUpper();

                        // --What do we want to change?
                        //  -if name
                        if (changeChoice == "N")
                        {
                            //  -prompt for a new name
                            foundEmployee.Name = PromptForString("What is the new name? ");
                        }
                        //  -if the department
                        if (changeChoice == "D")
                        {
                            //  -Prompt for a new department
                            foundEmployee.Department = PromptForInteger("What is the new department? ");
                        }
                        //  -if salary
                        if (changeChoice == "S")
                        {
                            //  -prompt for new salary
                            foundEmployee.Salary = PromptForInteger("What is the new salary? ");
                        }


                    }
                }

                else
                if (choice == "A")
                {
                    //CREATE (out of CREATE, READ, UPDATE, DELETE)!!!!!!!!!!!!!!!!!!!!!!!!!!!!(otherwise called "Add a person")
                    //Make a new employee object
                    var employee = new Employee();

                    //Prompt for values and save them in the employee's properties
                    employee.Name = PromptForString("What is your name? ");
                    employee.Department = PromptForInteger("What is your department number? ");
                    employee.Salary = PromptForInteger("What is your yearly salary (in dollars)? ");

                    // Add it to the list
                    employees.Add(employee);
                }

                else
                {
                    Console.WriteLine("Nope! Not an option... ");
                }

            }// end of the 'while' statement




        }//end of Main
    }//end of PROGRAM
}//end of Mainspace

