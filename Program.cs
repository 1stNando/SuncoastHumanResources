using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace SuncoastHumanResources
{

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
            //Update for API. change name to reference database AND change where the List<Employee> was created and instead reference our new EmployeeDatabase(); by calling its method.

            //Make new database to save into
            var database = new EmployeeDatabase();//!! important "EmployeeDatabase()" method that references this key Class to where all of our data will be linked to CsvHelper.

            //Only need one instance at the beginning. This method gets the next link in the chain that connects our Class where the CSV reader/writer lives. 
            database.LoadEmployees();

            //show the greeting via method call
            DisplayGreeting();

            //Should we keep showing the menu?      
            var keepGoing = true;

            //While the user hasn't said QUIT..
            while (keepGoing)
            {
                //Insert a blank line then promt them and get their answer (force uppercase)
                Console.WriteLine();
                Console.Write("What do you want to do?\n(A)dd an employee\n(D)elete an employee\n(F)ind an employee\n(U)pdate an employee\n(S)how all the employees\n(Q)uit\n:");
                var choice = Console.ReadLine().ToUpper();//This ReadLine() method is an important pause in the UI, that waits for the user to respond.IMPORTANT//////////////////////////logic
                //Notice the connection we are able to make using this variable called "choice" to be equal to "ReadLine"() method.Connecting the interface to the user. 

                //Now lets change this to include SWITCH CASE instead to optimize function

                switch (choice)//Key location of the structure for the UI part of the program. /////////////////references into the multiple methods we created under the Class EmployeeDatabase!!!!!!!!!!!!!!!!!!!!!!!!!
                {
                    //They said quit, so set our keepGoing to false
                    case "Q":
                        keepGoing = false;
                        break;

                    case "D":
                        DeleteEmployee(database);
                        //This location allows the database to be updated instantly. Even if we abort the program. The location of this save matters in the logic of improving the program.
                        database.SaveEmployees();
                        break;

                    case "F":
                        ShowEmployee(database);
                        break;


                    case "S":
                        ShowAllEmployees(database);
                        break;

                    case "U":
                        UpdateEmployee(database);
                        database.SaveEmployees();
                        break;

                    case "A":
                        AddEmployee(database);
                        database.SaveEmployees();
                        break;
                    default:
                        Console.WriteLine("Nope! Not an option... ");
                        break;

                }// end of the 'while' statement

                //One instance at the end of program SAVE Employees
                database.SaveEmployees();
            }
        }//end of Main

        private static void DeleteEmployee(EmployeeDatabase database)
        {
            //THIS IS DELETE - out of (CREATE, READ, UPDATE, DELETE)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //get employee name
            var nameToSearchFor = PromptForString("What name are you looking for? ");

            //Search database to see if they exist!
            Employee foundEmployee = database.FindOneEmployee(nameToSearchFor);/////Important link chain, Employee references our lonely Employee class that sits alone in its own .cs file in this case
            //super important to understand how this "foundEmployee" variable is utilized to describe a behavior we want to occur in our database. Which ultimately is to find an employee. Allowing us to manipulate/toss around it in conjunction with everything! 

            //If we did not find an employee
            if (foundEmployee == null)
            {
                //Show that the person doesn't exist
                Console.WriteLine("No such employee! ");
            }

            else
            {
                // - Show the details
                Console.WriteLine($"{foundEmployee.Name} is in department {foundEmployee.Department} and makes ${foundEmployee.Salary}");
                // - Ask to confirm
                var confirm = PromptForString($"Are you sure you want to delete {foundEmployee.Name}? [Y/N] ").ToUpper();

                if (confirm == "N")
                {
                    //do nothing
                    Console.WriteLine($"Ok, not doing anything to {foundEmployee}");
                }

                else if (confirm == "Y")
                {
                    //Delete them
                    database.DeleteEmployee(foundEmployee);
                }

            }
        }//End of DELETE method that goes into our database.//////////////////////////////////////////void method

        private static void ShowEmployee(EmployeeDatabase database)/////////////////////////READ from database method.
        {
            // Prompt for the name
            var nameToSearchFor = PromptForString("What name are you looking for? ");
            //Show the use of LINQ method shortcut style to search for something.!SUPERPOWER.!!!!!!
            Employee foundEmployee = database.FindOneEmployee(nameToSearchFor);///////notice how we are once again doing the same thing we did with the method above..

            //After the loop, 'foundEmployee' is either 'null' (not found) or refers to the matching item
            if (foundEmployee == null)
            {   ////Show a message if 'null', 
                Console.WriteLine("We unfortunately did not find this person in our database! Sorry.");
            }
            else
            {

                //otherwise show the details.
                Console.WriteLine($"{foundEmployee.Name} is in department {foundEmployee.Department} and makes ${foundEmployee.Salary}");
            }
        }


        private static void UpdateEmployee(EmployeeDatabase database)/////////////////////////////////////////////////////////////////UPDATE our database method!////////////////////More work. More complex method logic. 
        {

            Console.WriteLine("UPDATING!");
            //Get the employee name we are searching for
            var nameToSearchFor = PromptForString("What name are you looking for? ");/////////Once again...we are doing the same thing we did to start in the other methods created. 

            //Search the database to see if they exist!
            Employee foundEmployee = database.FindOneEmployee(nameToSearchFor); //Once again.. We are repeating the same process we did on the Delete method above. ///////////////Important///////

            // If we did NOT find anyone
            if (foundEmployee == null)
            {
                //Show that the person doesn't exist
                Console.WriteLine("No such employee! ");
            }

            //If we found an employee
            else//IMPORTANT location of the logic in our UI where we tie in everything we created at earlier. Like the methods to describe the behaviors we needed to occur and affect our database.. The End. 
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

        private static void AddEmployee(EmployeeDatabase database)
        {
            //CREATE (out of CREATE, READ, UPDATE, DELETE)!(otherwise called "Add a person")//////////////////////////////////////////////////////////////////////////////////////////////
            //Make a new employee object
            var employee = new Employee();//IMPORTANT to understand how we are INVOKING a new instance of our existing class of Employee.////////////////////////////////////////Remember this link in the chain of logic! 

            //Prompt for values and save them in the employee's properties. NOticE how we manipulate the variable employee in order to connect it to the actual Employee class that exists. And the use of a dot followed by the name of the attribute we want to target in our file.
            employee.Name = PromptForString("What is your name? ");
            employee.Department = PromptForInteger("What is your department number? ");
            employee.Salary = PromptForInteger("What is your yearly salary (in dollars)? ");

            // Add it to the list
            database.AddEmployee(employee);
        }

        private static void ShowAllEmployees(EmployeeDatabase database)
        {
            // READ(out of CREATE - READ - UPDATE - DELETE)
            foreach (var employee in database.GetAllEmployees())
            {
                Console.WriteLine($"{employee.Name} is in department {employee.Department} and makes ${employee.Salary}");
            }
        }


    }//end of Program

}
