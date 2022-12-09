using System.Linq;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace SuncoastHumanResources
{
    class EmployeeDatabase
    {
        //Create our first private database, other people do not have access to this information by default. 
        private List<Employee> Employees { get; set; } = new List<Employee>();

        //Lastly...we can add this, This allows us to make only ONE place where we have to write "employees.csv". This makes much more sense when we consider changing the .csv file to use another one and only have to change the name of the file in one location
        static private string FileName = "employees.csv";

        ///////////////////////////Below we will write the different behaviors/methods we want this class to do.////////////////////////////////

        //Method to LOAD employees, doesn't return anything just populates Employees List ®
        public void LoadEmployees()
        {
            if (File.Exists(FileName))
            {
                //Create a READER from employees.csv
                var fileReader = new StreamReader(FileName);

                //Create csvReader off of fileReader
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);

                //Replace our BLANK list of employees with the ones that are int the CSV file. This READS records.
                Employees = csvReader.GetRecords<Employee>().ToList();

                fileReader.Close();
            }
        }
        //ability to WRITE the Employee list to a file! 
        public void SaveEmployees()
        {
            var fileWriter = new StreamWriter(FileName);

            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(Employees);

            fileWriter.Close();
        }



        //CREATE Add Employee. We make this public because we want other users to be able to add new people. 
        public void AddEmployee(Employee newEmployee)
        {
            //this right here is the "behavior" we want
            Employees.Add(newEmployee);
        }

        //READ Get all employee
        public List<Employee> GetAllEmployees()
        {
            return Employees;
        }
        //also READ Find one emp.
        public Employee FindOneEmployee(string nameToFind)
        {
            //Updated to be able to search either by first or last name! Via nameToFind. This normalizes the case.
            Employee foundEmployee = Employees.FirstOrDefault(employee => employee.Name.ToUpper().Contains(nameToFind.ToUpper()));
            return foundEmployee;
        }
        //Delete Employee 
        public void DeleteEmployee(Employee employeeToDelete)
        {
            Employees.Remove(employeeToDelete);
        }

        //We do not need UPDATE 
    }
}//end of Mainspace

