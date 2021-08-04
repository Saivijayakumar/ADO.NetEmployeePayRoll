using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRollAdo.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\tWelcome to Employee PayRoll Using ADO.NET\n");
            EmployeeRepository repository = new EmployeeRepository();
            repository.GetTotalInformationFromTable();
            Console.ReadLine();
        }
    }
}
