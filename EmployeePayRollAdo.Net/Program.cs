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
            Console.WriteLine("1.Display Table\n2.Retive Data Based on Date\n3.Retrive Agregate function result\n4.ER Table Data\n5.Change salary in ER \n6.Retive Data Based on Date in ER Table\n" +
                "7.Retrive Agregate function result IN ER diagram\n8.Insert multiple contacts using thread ");
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
                case "8":
                    EmployeeThreads threads = new EmployeeThreads();
                    List<EmployeeModel> employees = new List<EmployeeModel>();
                    EmployeeModel employee1 = new EmployeeModel();
                    EmployeeModel employee2 = new EmployeeModel();
                    //first employee details
                    employee1.CompanyId = 2;
                    employee1.EmployeName = "Ragu";
                    employee1.PhoneNumber = 948484849;
                    employee1.Address = "Gandi address";
                    employee1.Gender = "M";
                    employee1.Base_pay = 47474;
                    employee1.Deductions = 34;
                    employee1.IncomeTax = 300;
                    employee1.DepartmentId = 2;
                    employee1.IsActive = 1;
                    //adding to list
                    employees.Add(employee1);
                    //second Employe Details
                    employee2.CompanyId = 2;
                    employee2.EmployeName = "Ramya";
                    employee2.PhoneNumber = 93333334849;
                    employee2.Address = "Gandi address";
                    employee2.Gender = "F";
                    employee2.Base_pay = 47474;
                    employee2.Deductions = 300;
                    employee2.IncomeTax = 320;
                    employee2.DepartmentId = 1;
                    employee2.IsActive = 0;
                    //adding to list
                    employees.Add(employee2);
                    //calling Thread methods
                    threads.ForCalculatingTime(employees);
                    break;
                default:
                    Console.WriteLine("Enter Valid Choice");
                    break;
            }
            Console.ReadLine();
        }
    }
}
