using EmployeePayRollAdo.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestForEmployeePayroll
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeRepository repository;
        EmployeeModel model;
        [TestInitialize]
        public void setup()
        {
            repository = new EmployeeRepository();
            model = new EmployeeModel();
        }
        [TestMethod]
        [TestCategory("UPDATE")]
        public void UpdatingEmployeBasePayByUsingQuery()
        {
            int expected = 1;
            int actual = repository.UpdateSalaryUsingQuery();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [TestCategory("UPDATE")]
        public void UpdatingEmployeBasePayByUsingProcedure()
        {
            int expected = 1;
            model.EmployeId = 6;
            model.EmployeName = "Terissa";
            model.Base_pay = 500000;
            int actual = repository.UpdateSalaryUsingProcedure(model);
            Assert.AreEqual(actual, expected);
        }
    }
}
