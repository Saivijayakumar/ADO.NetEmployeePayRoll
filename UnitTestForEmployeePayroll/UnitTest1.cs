using EmployeePayRollAdo.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestForEmployeePayroll
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeRepository repository;
        [TestInitialize]
        public void setup()
        {
            repository = new EmployeeRepository();
        }
        [TestMethod]
        public void UpdatingEmployeBasePayByUsingQuery()
        {
            int expected = 1;
            int actual = repository.UpdateSalaryUsingQuery();
            Assert.AreEqual(expected, actual);
        }
    }
}
