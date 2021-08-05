using EmployeePayRollAdo.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestForEmployeePayroll
{
    [TestClass]
    public class UnitTest1
    {
        EmployeeRepository repository;
        ERRepo errepo;
        EmployeeModel model;
        [TestInitialize]
        public void setup()
        {
            repository = new EmployeeRepository();
            model = new EmployeeModel();
            errepo = new ERRepo();
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
        //-----------------ER Diagram--------------
        [TestMethod]
        public void GivenSelectQueryReturnCount()
        {
            //we already know how many data we have
            int expected = 5;
            int actual=errepo.RetriveAllData();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GiveingValuesToUpdateReturnSuccessCount()
        {
            int expected = 1;
            model.EmployeName = "vijaya";
            model.Base_pay = 80000;
            int actual = errepo.ChangeSalaryBasedOnNameAndID(model);
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void CallingMethodGetingCountOfResult()
        {
            int expected = 3;
            int actual = errepo.GetEmployeAccordingToDate();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CallingMethodGettingResultInTheFormOfString()
        {
            string expected = "9168400 80000 9088400 4584200 2";
            string actual = errepo.AggregateFunctionBasedOnGender();
            Assert.AreEqual(expected, actual);
        }
    }
}
