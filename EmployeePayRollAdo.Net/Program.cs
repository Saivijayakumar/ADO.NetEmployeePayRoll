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
            EmployeeModel model = new EmployeeModel();
            Console.WriteLine("1.Display Table\n2.Retive Date Based on Date\n3.Retrive Agregate function result ");
            Console.Write("Enter Your choice:");
            switch (Console.ReadLine())
            {
                case "1":
                    repository.GetTotalInformationFromTable();
                    break;
                case "2":
                    repository.RetriveDataBasedOnDateRange(model);
                    break;
                case "3":
                    repository.AgregateFunctionBasedOnGender();
                    break;
                default:
                    Console.WriteLine("Enter Valid Choice");
                    break;
            }
            Console.ReadLine();
        }
    }
}
