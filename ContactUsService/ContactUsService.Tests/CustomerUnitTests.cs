using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactUsService.Tests
{
    [TestClass]
    public class CustomerUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testCreateCustomerWithEmptyName()
        {
            var msg = Models.Customer.Create("john.smith@yahoo.com", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testCreateCustomerWithEmptyEmail()
        {
            var msg = Models.Customer.Create("", "John Smith");
        }

        [TestMethod]
        public void testCreateCustomerWithValidName()
        {
            var customer = Models.Customer.Create("john.smith@yahoo.com", "John Smith Jones");

            Assert.AreEqual(customer.Email, "john.smith@yahoo.com");
            Assert.AreEqual(customer.FirstName, "John");
            Assert.AreEqual(customer.LastName, "Smith Jones");
        }

        [TestMethod]
        public void testCreateCustomerWithOneNameOnly()
        {
            var customer = Models.Customer.Create("john.smith@yahoo.com", "John");

            Assert.AreEqual(customer.Email, "john.smith@yahoo.com");
            Assert.AreEqual(customer.FirstName, "John");
            Assert.AreEqual(customer.LastName, "");
        }

        [TestMethod]
        public void testCreateCustomerWithTabsInName()
        {
            var customer = Models.Customer.Create("john.smith@yahoo.com", "           John                       Smith");

            Assert.AreEqual("john.smith@yahoo.com", customer.Email);
            Assert.AreEqual("John", customer.FirstName);
            Assert.AreEqual("Smith", customer.LastName);
        }
    }
}
