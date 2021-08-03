using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRollAdo.Net
{
    class EmployeeRepository
    {
        public static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Initial Catalog = payroll_services;";
        //Createing conection object to connect with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);
    }
}
