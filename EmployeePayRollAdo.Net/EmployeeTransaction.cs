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
    public class EmployeeTransaction
    {
        public static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Initial Catalog = payroll_services;";
        //Createing conection object to connect with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        List<EmployeeModel> employeesList = new List<EmployeeModel>();
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
        //----------------Using Threads From Here----------------
        public int TransverDataToListWithoutUsingThreads()
        {
            int count = 0;
            try
            {
                //create object for employeeModel
                EmployeeModel ermodel = new EmployeeModel();
                Stopwatch stopwatch = new Stopwatch();
                SqlCommand command = new SqlCommand("SelectForErTable", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    stopwatch.Start();
                    while (reader.Read())
                    {
                        //calling method to display values
                        DisplayAndAddToList(ermodel, reader);
                        count++;
                    }
                    stopwatch.Stop();
                    Console.WriteLine($"Duration without Thread excecution : {stopwatch.ElapsedMilliseconds} ");
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
            }
            finally
            {
                sqlConnection.Close();
            }
            return count;
        }
        public void DisplayAndAddToList(EmployeeModel ermodel, SqlDataReader reader)
        {
            ermodel.CompanyId = Convert.ToInt32(reader["CompanyID"]);
            ermodel.CompanyName = reader["CompanyName"].ToString();
            ermodel.EmployeId = Convert.ToInt32(reader["EmployeeID"]);
            ermodel.EmployeName = reader["EmployeeName"].ToString();
            ermodel.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
            ermodel.Department = reader["DepartName"].ToString();
            ermodel.Gender = reader["Gender"].ToString();
            ermodel.StartDate = Convert.ToDateTime(reader["StartDate"]);
            ermodel.Base_pay = Convert.ToDouble(reader["BasicPay"]);
            ermodel.PhoneNumber = Convert.ToInt64(reader["EmployeePhoneNumber"]);
            ermodel.Address = reader["EmployeeAddress"].ToString();
            ermodel.NetPay = Convert.ToDouble(reader["NetPay"]);
            ermodel.TaxablePay = Convert.ToDouble(reader["TaxablePay"]);
            ermodel.IncomeTax = Convert.ToDouble(reader["IncomeTax"]);
            ermodel.Deductions = Convert.ToDouble(reader["Deductions"]);
            ermodel.IsActive = Convert.ToInt32(reader["IsActive"]);
            Console.WriteLine($"\nCompanyID:{ermodel.CompanyId}|CompanyName:{ermodel.CompanyName}|EmployeeID:{ermodel.EmployeId}|EmployeeName:{ermodel.EmployeName}|" +
                $"DepartmentID:{ermodel.DepartmentId}|DepartmentName:{ermodel.Department}|Gender:{ermodel.Gender}|StartDate:{ermodel.StartDate}|BasePay:{ermodel.Base_pay}" +
                $"|Phone:{ermodel.PhoneNumber}|Address:{ermodel.Address}|NetPay:{ermodel.NetPay}|TaxablePay:{ermodel.TaxablePay}|IncomeTax:{ermodel.IncomeTax}|Deduction:{ermodel.Deductions}|IsActive:{ermodel.IsActive}");
            employeesList.Add(ermodel);
        }
        public int TransverDataToListUsingThreads()
        {
            int count = 0;
            try
            {
                //create object for employeeModel
                EmployeeModel ermodel = new EmployeeModel();
                Stopwatch stopwatch = new Stopwatch();
                SqlCommand command = new SqlCommand("SelectForErTable", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    stopwatch.Start();
                    while (reader.Read())
                    {
                        //calling method to display values
                        DisplayAndAddToListUsingThread(ermodel, reader);
                        count++;
                    }
                    stopwatch.Stop();
                    Console.WriteLine($"Duration without Thread excecution : {stopwatch.ElapsedMilliseconds} ");
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
            }
            finally
            {
                sqlConnection.Close();
            }
            return count;
        }
        public void DisplayAndAddToListUsingThread(EmployeeModel ermodel, SqlDataReader reader)
        {
            ermodel.CompanyId = Convert.ToInt32(reader["CompanyID"]);
            ermodel.CompanyName = reader["CompanyName"].ToString();
            ermodel.EmployeId = Convert.ToInt32(reader["EmployeeID"]);
            ermodel.EmployeName = reader["EmployeeName"].ToString();
            ermodel.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
            ermodel.Department = reader["DepartName"].ToString();
            ermodel.Gender = reader["Gender"].ToString();
            ermodel.StartDate = Convert.ToDateTime(reader["StartDate"]);
            ermodel.Base_pay = Convert.ToDouble(reader["BasicPay"]);
            ermodel.PhoneNumber = Convert.ToInt64(reader["EmployeePhoneNumber"]);
            ermodel.Address = reader["EmployeeAddress"].ToString();
            ermodel.NetPay = Convert.ToDouble(reader["NetPay"]);
            ermodel.TaxablePay = Convert.ToDouble(reader["TaxablePay"]);
            ermodel.IncomeTax = Convert.ToDouble(reader["IncomeTax"]);
            ermodel.Deductions = Convert.ToDouble(reader["Deductions"]);
            ermodel.IsActive = Convert.ToInt32(reader["IsActive"]);
            Thread thread = new Thread(() =>
            {
                Console.WriteLine($"\nCompanyID:{ermodel.CompanyId}|CompanyName:{ermodel.CompanyName}|EmployeeID:{ermodel.EmployeId}|EmployeeName:{ermodel.EmployeName}|" +
                $"DepartmentID:{ermodel.DepartmentId}|DepartmentName:{ermodel.Department}|Gender:{ermodel.Gender}|StartDate:{ermodel.StartDate}|BasePay:{ermodel.Base_pay}" +
                $"|Phone:{ermodel.PhoneNumber}|Address:{ermodel.Address}|NetPay:{ermodel.NetPay}|TaxablePay:{ermodel.TaxablePay}|IncomeTax:{ermodel.IncomeTax}|Deduction:{ermodel.Deductions}|IsActive:{ermodel.IsActive}");
                employeesList.Add(ermodel);
            });
            thread.Start();
            Console.WriteLine($"Thread Id: {thread.ManagedThreadId} ");
        }
    }
}
