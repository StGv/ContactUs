using ContactUsService.Controllers;
using ContactUsService.Controllers.DTOs;
using ContactUsService.Services;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ContactUsService.Tests
{
    [TestClass]
    public class IntegrationTests : TestDBInstanceBaseTest
    {
        ContactUsController _target;

        [TestInitialize]
        public void Before()
        {
            _target = new ContactUsController(new CustomerMessageRepo(base.DbContext));
        }


        [TestMethod]
        public void testIfDataWillBeInsertedWhenPostedToController()
        {
            var fromData = new ContactUsFormDTO()
            {
                fullName = "Joseph Smith",
                email = "smith.joseph@gmail.com",
                message = "this is a very long message!"
            };

            var taskPost = _target.SubmitMessage(fromData);
            taskPost.Wait();
            var dbMessages= GetMessageFromDB("smith.joseph@gmail.com") as List<Models.CustomerMessage>;
            var dbCustomers = GetCustomersFromDB("smith.joseph@gmail.com") as List<Models.Customer>;

            Assert.IsNotNull(dbMessages);
            Assert.IsTrue(dbMessages.Count == 1);
            Assert.IsNotNull(dbCustomers);
            Assert.IsTrue(dbCustomers.Count == 1);

            Assert.AreEqual(dbMessages[0].Text, fromData.message);
            Assert.AreEqual(dbCustomers[0].FirstName, Models.Customer.getFirstName(fromData.fullName));
            Assert.AreEqual(dbCustomers[0].LastName, Models.Customer.getLastName(fromData.fullName));
        }

        [TestMethod]
        public void testMultipleMessagesSentfromSameUserShouldNotCreateMultipleCustomers()
        {
            var fromData = new ContactUsFormDTO()
            {
                fullName = "Joseph Smith",
                email = "smith.joseph@gmail.com",
                message = "this is a very long message!"
            };

            for (int i = 0; i < 10; i++)
            {
                var taskPost = _target.SubmitMessage(fromData);
                taskPost.Wait();
            }

            var dbMessages = GetMessageFromDB("smith.joseph@gmail.com") as List<Models.CustomerMessage>;
            var dbCustomers = GetCustomersFromDB("smith.joseph@gmail.com") as List<Models.Customer>;

            Assert.IsNotNull(dbMessages);
            Assert.IsTrue(dbMessages.Count == 10);
            Assert.IsNotNull(dbCustomers);
            Assert.IsTrue(dbCustomers.Count == 1);

        }

        [TestMethod]
        public void testWhenMessageFromExistingCustomerWithNewNameShouldUpdateCustomerName()
        {
            var fromData = new ContactUsFormDTO()
            {
                fullName = "Joseph Smith",
                email = "smith.joseph@gmail.com",
                message = "this is a very long message!"
            };
            var taskPost = _target.SubmitMessage(fromData);
            taskPost.Wait();

            var fromData2 = new ContactUsFormDTO()
            {
                fullName = "Josephine Smith-Jones",
                email = "smith.joseph@gmail.com",
                message = "this is a another even longr message!"
            };
            var taskPost2 = _target.SubmitMessage(fromData2);
            taskPost2.Wait();

            var dbMessages = GetMessageFromDB("smith.joseph@gmail.com") as List<Models.CustomerMessage>;
            var dbCustomers = GetCustomersFromDB("smith.joseph@gmail.com") as List<Models.Customer>;

            Assert.IsNotNull(dbMessages);
            Assert.IsTrue(dbMessages.Count == 2);
            Assert.IsNotNull(dbCustomers);
            Assert.IsTrue(dbCustomers.Count == 1);

            Assert.AreEqual(dbCustomers[0].FirstName, Models.Customer.getFirstName(fromData2.fullName));
            Assert.AreEqual(dbCustomers[0].LastName, Models.Customer.getLastName(fromData2.fullName));
        }

        private IEnumerable<Models.CustomerMessage> GetMessageFromDB(string email)
        {
            var sql = $"SELECT * FROM CustomerMessages WHERE Customer_Email = '{email}'";

            var taskQueryDB = queryEngine.Select<Models.CustomerMessage>(sql) ;
            taskQueryDB.Wait();

            return taskQueryDB.Result.AsList();
        }

        private IEnumerable<Models.Customer> GetCustomersFromDB(string email)
        {
            var sql = $"SELECT * FROM Customers WHERE Email = '{email}'";

            var taskQueryDB = queryEngine.Select<Models.Customer>(sql);
            taskQueryDB.Wait();

            return taskQueryDB.Result.AsList();
        }

    }
}
