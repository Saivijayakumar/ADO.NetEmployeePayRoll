﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRollAdo.Net
{
    public class EmployeeTransaction
    {
        public static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Initial Catalog = payroll_services;";
        //Createing conection object to connect with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public string InserIntoTableUsingTransaction()
        {
            //Opening the connection
            sqlConnection.Open();
            //Begin the Transaction and creating transaction object
            SqlTransaction transaction = sqlConnection.BeginTransaction();
            //creating command object and createcommand
            SqlCommand command = sqlConnection.CreateCommand();
            //set Command to transaction
            command.Transaction = transaction;
            try
            {
                //set command text to command object
                command.CommandText = @"INSERT INTO Employee VALUES (2,'Arun kana',984747484,'130 Park avenue','2014-08-09','M');";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO EMPPayroll(EmployeeIdentity,BasicPay,Deductions,IncomeTax) values (10,374848,484,2000);";
                command.ExecuteNonQuery();
                command.CommandText = @"UPDATE EMPPayroll SET TaxablePay = BasicPay-Deductions;";
                command.ExecuteNonQuery();
                command.CommandText = @"UPDATE EMPPayroll SET NetPay=TaxablePay-IncomeTax;";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO EmployeeDepartment VALUES (4,10);";
                command.ExecuteNonQuery();
                //If all commands execute then only it commit.
                transaction.Commit();
                return "Success";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                //If any command is not executed then it rollback
                transaction.Rollback();
                return "Unsuccess";
            }
            finally
            {
                sqlConnection.Close();
            }

        }

    }
}
