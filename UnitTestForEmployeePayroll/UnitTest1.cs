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
        EmployeeTransaction transaction;
        [TestInitialize]
        public void setup()
        {
            repository = new EmployeeRepository();
            model = new EmployeeModel();
            errepo = new ERRepo();
            transaction = new EmployeeTransaction();
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
        [TestCategory("ER Diagram")]
        public void GivenSelectQueryReturnCount()
        {
            //we already know how many data we have
            int expected = 5;
            int actual=errepo.RetriveAllData();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [TestCategory("ER Diagram")]
        public void GiveingValuesToUpdateReturnSuccessCount()
        {
            int expected = 1;
            model.EmployeName = "vijaya";
            model.Base_pay = 80000;
            int actual = errepo.ChangeSalaryBasedOnNameAndID(model);
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        [TestCategory("ER Diagram")]
        public void CallingMethodGetingCountOfResult()
        {
            int expected = 3;
            int actual = errepo.GetEmployeAccordingToDate();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [TestCategory("ER Diagram")]
        public void CallingMethodGettingResultInTheFormOfString()
        {
            string expected = "9168400 80000 9088400 4584200 2";
            string actual = errepo.AggregateFunctionBasedOnGender();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [TestCategory("Transaction for insert")]
        public void InsertNewRecordThroughTransactionIfRecordIsAddedItGiveSuccess()
        {
            string expected = "Success";
            string actual = transaction.InserIntoTableUsingTransaction();
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        [TestCategory("Transaction for delete")]
        public void UseingCascadeDeleteToDeleteRecordIFSuccessReturnSucess()
        {
            string expected = "success";
            string actual = transaction.DeleteUsingCascade();
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        [TestCategory("IsActive")]
        public void TryingToCreateNewColoumIfColoumCreatesReturnSuccess()
        {
            string expected = "Success";
            string actual = transaction.AddingIsActiveColoumToTable();
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        [TestCategory("IsActive")]
        public void TryingToDisplayTheDateIfIsActiveIsoneIfItSuccessReturnSuccess()
        {
            int expected = 4;
            int actual = transaction.RetriveDataIfIsActiveIsOne();
            Assert.AreEqual(actual, expected);
        }
    }
}
