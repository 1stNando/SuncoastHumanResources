using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace SuncoastHumanResources
{
    class EmployeeDatabase
    {
        //Create our first private database, other people do not have access to this information by default. 
        private List<Employee> Employees { get; set; } = new List<Employee>();


        //Method to LOAD employees, doesn't return anything just populates Employees List
        public void LoadEmployees()
        {

        }
        //ability to write the Employee list to a file!
        public void SaveEmployees()
        {
            var fileWriter = new StreamWriter("employees.csv");
        }

        //Below we will write the different behaviors we want this class to do.

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

