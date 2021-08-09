using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeePayRollAdo.Net
{
    public class EmployeeThreads
    {
        //adding server name and database name
        public static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Initial Catalog = payroll_services;";
        //Createing conection object to connect with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public int AddNewEmployee(EmployeeModel model)
        {
            try
            {
                DateTime date = Convert.ToDateTime("2020-10-30");
                SqlCommand command = new SqlCommand("InsertDataIntoERTable", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@companyId", model.CompanyId);
                command.Parameters.AddWithValue("@employeName", model.EmployeName);
                command.Parameters.AddWithValue("@phoneNumber", model.PhoneNumber);
                command.Parameters.AddWithValue("@address", model.Address);
                command.Parameters.AddWithValue("@gender", model.Gender);
                command.Parameters.AddWithValue("@basePay", model.Base_pay);
                command.Parameters.AddWithValue("@deduction", model.Deductions);
                command.Parameters.AddWithValue("@incomeTax", model.IncomeTax);
                command.Parameters.AddWithValue("@departmentId", model.DepartmentId);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@isActive", model.IsActive);
                sqlConnection.Open();
                int result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Updated");
                    return result;
                }
                else
                {
                    Console.WriteLine("Not Updated");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void ForCalculatingTime(List<EmployeeModel> employeeModels)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            AddContactListToDBWithoutThread(employeeModels);
            stopwatch.Stop();
            Console.WriteLine($"Time taken for Without Thread :{stopwatch.ElapsedMilliseconds}");
            stopwatch.Start();
            AddContactListToDBWithThread(employeeModels);
            stopwatch.Stop();
            Console.WriteLine($"Time taken for With Thread :{stopwatch.ElapsedMilliseconds}");
        }
        // Method to Add List of Contacts To DB Without Thread..................
        public void AddContactListToDBWithoutThread(List<EmployeeModel> contactList)
        {

            contactList.ForEach(contact =>
            {
                Console.WriteLine("Contact being added: " + contact.EmployeName);
                this.AddNewEmployee(contact);
                Console.WriteLine("Contact added: " + contact.EmployeName);
            });
        }
        // Method to Add List of Contacts To DB With Thread......................
        public void AddContactListToDBWithThread(List<EmployeeModel> contactList)
        {
            contactList.ForEach(contact =>
            {
                Thread thread = new Thread(() =>
                {
                    Console.WriteLine("Contact adding start" + contact.EmployeName);
                    this.AddNewEmployee(contact);
                    Console.WriteLine("Contact adding end: " + contact.EmployeName);
                });
                thread.Start();
                thread.Join();
            });
        }
    }
}
