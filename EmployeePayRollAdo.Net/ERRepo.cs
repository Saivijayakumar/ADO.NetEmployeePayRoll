using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRollAdo.Net
{
    public class ERRepo
    {
        //adding server name and database name
        public static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Initial Catalog = payroll_services;";
        //Createing conection object to connect with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public int RetriveAllData()
        {
            int count = 0;
            try
            {
                //create object for employeeModel
                EmployeeModel ermodel = new EmployeeModel();
                SqlCommand command = new SqlCommand("SelectForErTable", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        //calling method to display values
                        DisplayTotalData(ermodel, reader);
                        count++;
                    }
                }
                else
                {
                    Console.WriteLine("Data Not Found");
                }
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
            return count;
        }
        public int ChangeSalaryBasedOnNameAndID(EmployeeModel ermodel)
        {
            try
            {
                SqlCommand command = new SqlCommand("ChangeBasePayForER", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", ermodel.EmployeName);
                command.Parameters.AddWithValue("@BasePay", ermodel.Base_pay);
                sqlConnection.Open();
                int result = command.ExecuteNonQuery();
                if(result!=0)
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
        public int GetEmployeAccordingToDate()
        {
            int count = 0;
            try
            {
                string query = @"SELECT EmployeeID,EmployeeName,CompanyName,StartDate FROM Company INNER JOIN Employee ON Company.CompanyID = Employee.CompanyIdentity where StartDate BETWEEN Cast('2000-11-12' as Date) and cast('2010-02-02' as Date)";
                //create object for employeeModel
                EmployeeModel ermodel = new EmployeeModel();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ermodel.CompanyName = reader["CompanyName"].ToString();
                        ermodel.EmployeId = Convert.ToInt32(reader["EmployeeID"]);
                        ermodel.EmployeName = reader["EmployeeName"].ToString();
                        ermodel.StartDate = Convert.ToDateTime(reader["StartDate"]);
                        Console.WriteLine($"EmployeId:{ermodel.EmployeId}|EmployeName:{ermodel.EmployeName}|CompanyName:{ermodel.CompanyName}|StartDate:{ermodel.StartDate}");
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
            }
            finally
            {
                sqlConnection.Close();
            }
            return count;
        }
        public string AggregateFunctionBasedOnGender()
        {
            string resultString = "";
            string query = @"select sum(EMPPayroll.BasicPay),min(EMPPayroll.BasicPay),max(EMPPayroll.BasicPay),Round(AVG(EMPPayroll.BasicPay),0),COUNT(*)  from Employee inner join EMPPayroll on Employee.EmployeeId = EMPPayroll.EmployeeIdentity where Employee.Gender = 'F' group by Employee.Gender";
            try
            {
                using (sqlConnection)
                {
                    ////query execution
                    SqlCommand command = new SqlCommand(query, this.sqlConnection);
                    //open sql connection
                    sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("TotalSalary: {0}  MinimumSalary: {1}  MaximumSalary: {2}  AverageSalary: {3}  FemaleCount: {4}", reader[0], reader[1], reader[2], reader[3], reader[4]);
                            resultString += reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3] + " " + reader[4];
                        }
                    }
                    //close reader
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            //returns the Result as string
            return resultString;
        }
        public static void DisplayTotalData(EmployeeModel ermodel,SqlDataReader reader)
        {
            ermodel.CompanyId = Convert.ToInt32(reader["CompanyID"]);
            ermodel.CompanyName = reader["CompanyName"].ToString();
            ermodel.EmployeId = Convert.ToInt32(reader["EmployeeID"]);
            ermodel.EmployeName = reader["EmployeeName"].ToString();
            ermodel.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
            ermodel.Department= reader["DepartName"].ToString();
            ermodel.Gender = reader["Gender"].ToString();
            ermodel.StartDate = Convert.ToDateTime(reader["StartDate"]);
            ermodel.Base_pay = Convert.ToDouble(reader["BasicPay"]);
            ermodel.PhoneNumber = Convert.ToInt64(reader["EmployeePhoneNumber"]);
            ermodel.Address = reader["EmployeeAddress"].ToString();
            ermodel.NetPay= Convert.ToDouble(reader["NetPay"]);
            ermodel.TaxablePay= Convert.ToDouble(reader["TaxablePay"]);
            ermodel.IncomeTax= Convert.ToDouble(reader["IncomeTax"]);
            ermodel.Deductions= Convert.ToDouble(reader["Deductions"]);
            ermodel.IsActive = Convert.ToInt32(reader["IsActive"]);
            Console.WriteLine($"\nCompanyID:{ermodel.CompanyId}|CompanyName:{ermodel.CompanyName}|EmployeeID:{ermodel.EmployeId}|EmployeeName:{ermodel.EmployeName}|" +
                $"DepartmentID:{ermodel.DepartmentId}|DepartmentName:{ermodel.Department}|Gender:{ermodel.Gender}|StartDate:{ermodel.StartDate}|BasePay:{ermodel.Base_pay}" +
                $"|Phone:{ermodel.PhoneNumber}|Address:{ermodel.Address}|NetPay:{ermodel.NetPay}|TaxablePay:{ermodel.TaxablePay}|IncomeTax:{ermodel.IncomeTax}|Deduction:{ermodel.Deductions}|IsActive:{ermodel.IsActive}");
        }
    }
}
