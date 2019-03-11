using ContactUsService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactUsService.Tests
{
    [TestClass]
    public class CustomerMessageRepoUnitTests : InMemoryDBBaseTest
    {
        ICustomerMessageRepository _target;

        [TestInitialize]
        public void Init()
        {
            _target = new CustomerMessageRepo(base.Context);
        }

        [TestMethod]
        public void insertMessage()
        {
            var fromData = new 
            {
                fullName = "Joseph Smith",
                email = "smith.joseph@gmail.com",
                message = "this is a very long message!"
            };

            var msg =  Models.CustomerMessage.Create(fromData.message, fromData.email, fromData.fullName );
            var invokerTask = _target.CreateNewMessageAsync(msg);
            invokerTask.Wait();

            var dbMessages = Context.Messages.ToList();
            var dbCustomers = Context.Customers.ToList();

            Assert.IsNotNull(dbMessages);
            Assert.IsTrue(dbMessages.Count == 1);
            Assert.IsNotNull(dbCustomers);
            Assert.IsTrue(dbCustomers.Count == 1);

            Assert.AreEqual(dbMessages[0].Text, fromData.message);
            Assert.AreEqual(dbCustomers[0].FirstName, Models.Customer.getFirstName(fromData.fullName));
            Assert.AreEqual(dbCustomers[0].LastName, Models.Customer.getLastName(fromData.fullName));
        }
    }
}
