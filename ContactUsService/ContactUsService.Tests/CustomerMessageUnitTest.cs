using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactUsService.Tests
{
    [TestClass]
    public class CustomerMessageUnitTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testCreateMessageWithEmptyEmail()
        {
            var msg = Models.CustomerMessage.Create("Test message", "", "John Smith");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testCreateMessageWithEmptyName()
        {
            var msg = Models.CustomerMessage.Create("Test message", "john.smith@yahoo.com", "");
        }

        [TestMethod]
        public void testCreateMessage()
        {
            var msg = Models.CustomerMessage.Create("Test message", "john.smith@yahoo.com", "John Smith");

            Assert.AreEqual(msg.Text, "Test message");
            Assert.IsNotNull(msg.Customer);
            Assert.IsNotNull(msg.CustomerEmail);
        }

        [TestMethod]
        public void testUpdateMessage()
        {
            var msg = Models.CustomerMessage.Create("Test message", "john.smith@yahoo.com", "John Smith");

            var customer = Models.Customer.Create("john.smith@yahoo.com", "John Smith");
            msg = Models.CustomerMessage.Update("Test message updated", customer);

            Assert.AreEqual(msg.Text, "Test message updated");
            Assert.AreSame(customer, msg.Customer);
        }
    }
}
