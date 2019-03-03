using System;
using ContactUsService.Controllers;
using ContactUsService.Controllers.DTOs;
using ContactUsService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ContactUsService.Tests
{
    [TestClass]
    public class IntegrationTests : BaseEFTest
    {
        ContactUsController _target;

        [TestInitialize]
        public void Before()
        {
            ContactUsService.AutoMapperConfig.Initialize();
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
            Assert.AreEqual(dbCustomers[0].FirstName, fromData.fullName.Split(' ')[0]);
            Assert.AreEqual(dbCustomers[0].LastName, string.Join(" ", fromData.fullName.Trim().Split(' ').Skip(1)));
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
