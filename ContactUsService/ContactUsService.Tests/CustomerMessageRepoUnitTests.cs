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
            var msg = new Models.CustomerMessage { Text = "This is a test message" };
            var invokerTask = _target.CreateNewMessageAsync(msg);
            invokerTask.Wait();

            Assert.IsTrue(Context.Messages.Count() == 1);
            Assert.AreEqual(Context.Messages.ElementAt(1).Text, msg.Text);
        }
    }
}
