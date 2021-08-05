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
            ERRepo eRRepo = new ERRepo();
            Console.WriteLine("1.Display Table\n2.Retive Data Based on Date\n3.Retrive Agregate function result\n4.ER Table Data\n5.Change salary in ER \n6.Retive Data Based on Date in ER Table\n7.Retrive Agregate function result IN ER diagram");
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
                case "4":
                    eRRepo.RetriveAllData();
                    break;
                case "5":
                    model.EmployeName = "vijaya";
                    model.Base_pay = 3000000;
                    eRRepo.ChangeSalaryBasedOnNameAndID(model);
                    break;
                case "6":
                    eRRepo.GetEmployeAccordingToDate();
                    break;
                case "7":
                    eRRepo.AggregateFunctionBasedOnGender();
                    break;
                default:
                    Console.WriteLine("Enter Valid Choice");
                    break;
            }
            Console.ReadLine();
        }
    }
}
