using System;
using System.Collections.Generic;
using System.Data;
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
                command.CommandText = @"INSERT INTO Employee VALUES (2,'last',984747484,'130 Park avenue','2014-08-09','M');";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO EMPPayroll(EmployeeIdentity,BasicPay,Deductions,IncomeTax) values (5,374848,484,2000);";
                command.ExecuteNonQuery();
                command.CommandText = @"UPDATE EMPPayroll SET TaxablePay = BasicPay-Deductions;";
                command.ExecuteNonQuery();
                command.CommandText = @"UPDATE EMPPayroll SET NetPay=TaxablePay-IncomeTax;";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO EmployeeDepartment VALUES (4,5);";
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
        public string DeleteUsingCascade()
        {
            sqlConnection.Open();
            SqlTransaction transaction = sqlConnection.BeginTransaction();
            SqlCommand command = sqlConnection.CreateCommand();
            command.Transaction = transaction;
            try
            {
                command.CommandText = @"DELETE FROM Employee WHERE EmployeeID=5;";
                command.ExecuteNonQuery();
                transaction.Commit();
                return "success";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                return "Unsuccess";
            }
        }
        public string AddingIsActiveColoumToTable()
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
                command.CommandText = @"ALTER TABLE Employee ADD IsActive int NOT NULL default 1;";
                command.ExecuteNonQuery();
                command.CommandText = @"UPDATE Employee SET IsActive = 0 WHERE EmployeeID=1";
                command.ExecuteNonQuery();
                //now we cant see sai name in employe.
                //If all commands execute then only it commit.
                transaction.Commit();
                return "Success";
            }
            catch (Exception ex)
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
        public int RetriveDataIfIsActiveIsOne()
        {
            int count = 0;
            try
            {
                //create object for employeeModel
                EmployeeModel ermodel = new EmployeeModel();
                SqlCommand command = new SqlCommand("SelectdataifIsActiveIsOne", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //calling method to display values
                        ERRepo.DisplayTotalData(ermodel, reader);
                        count++;
                    }
                }
                else
                {
                    Console.WriteLine("Data Not Found");
                }
                reader.Close();
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
            return count;
        }
    }
}
