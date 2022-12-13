namespace SuncoastHumanResources
{
    class Employee
    {//note that the order in which these attributes are laid determines the overall layout of the employees.csv file!
        public string Name { get; set; }
        public int Department { get; set; }
        public int Salary { get; set; }
        public int MonthlySalary()
        {
            return Salary / 12;
        }
    }
}//end of Mainspace

