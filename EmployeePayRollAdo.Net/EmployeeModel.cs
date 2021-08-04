using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRollAdo.Net
{
    public class EmployeeModel
    {
        public int EmployeId { get; set;}
        public string EmployeName { get; set;}
        public double Base_pay { get; set;}
        public DateTime StartDate { get; set;}
        public string Gender { get; set; }
        public double PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public double TaxablePay { get; set; }
        public double Deductions { get; set; }
        public double NetPay { get; set; }
        public double IncomeTax { get; set; }

    }
}
