using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRollAdo.Net
{
    public class EmployeeRepository
    {
        public static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Initial Catalog = payroll_services;";
        //Createing conection object to connect with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public void GetTotalInformationFromTable()
        {
            try
            {
                EmployeeModel model = new EmployeeModel();
                string query = @"select * from employee_payroll";
                //Command Object for executing query against database
                SqlCommand command = new SqlCommand(query,sqlConnection);
                //Opening connection to database
                sqlConnection.Open();
                //reader it contains result data of query
                SqlDataReader reader = command.ExecuteReader();
                DisplayEmployeeData(reader, model);
                reader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public int UpdateSalaryUsingQuery()
        {
            try
            {
                string query = @"update employee_payroll set Base_Pay=3000000 where Id=6 and Name='Terissa'";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                int result = command.ExecuteNonQuery();
                if (result != 0)
                    Console.WriteLine("Salary is Updated Successful");
                else
                    Console.WriteLine("Update Fail");
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public int UpdateSalaryUsingProcedure(EmployeeModel model)
        {
            try
            {
                using(sqlConnection)
                {
                    SqlCommand command = new SqlCommand("UpdateBasePay", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", model.EmployeId);
                    command.Parameters.AddWithValue("Name", model.EmployeName);
                    command.Parameters.AddWithValue("Base_Pay", model.Base_pay);
                    sqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result != 0)
                        Console.WriteLine("Salary is Updated Successful");
                    else
                        Console.WriteLine("Update Fail");
                    return result;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void RetriveDataBasedOnDateRange(EmployeeModel model)
        {
            try
            {
                string query = @"select * from employee_payroll where StartDate between cast('2020-01-1' as date) and getdate()";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DisplayEmployeeData(reader, model);
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public static void DisplayEmployeeData(SqlDataReader reader,EmployeeModel model)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model.EmployeId = Convert.ToInt32(reader["Id"]);
                    model.EmployeName = reader["Name"].ToString();
                    model.Base_pay = Convert.ToDouble(reader["Base_pay"]);
                    //without converting you can pass the index value it automaticaly convert
                    model.StartDate = reader.GetDateTime(3);
                    model.Gender = reader.GetString(4);
                    model.PhoneNumber = Convert.ToDouble(reader["PhoneNumber"]);
                    model.Department = reader.GetString(6);
                    model.Address = reader.GetString(7);
                    model.TaxablePay = reader.GetDouble(8);
                    model.Deductions = reader.GetDouble(9);
                    model.NetPay = reader.GetDouble(10);
                    model.IncomeTax = reader.GetDouble(11);
                    Console.WriteLine($"|{model.EmployeId}|{model.EmployeName}|{model.Department}|{model.Gender}|{model.Base_pay}|{model.StartDate}|{model.Address}|{model.PhoneNumber}|{model.TaxablePay}|{model.Deductions}|{model.NetPay}|{model.IncomeTax}\n");
                }
            }
            else
            {
                Console.WriteLine("Data Not Found");
            }
        }
    }
}
