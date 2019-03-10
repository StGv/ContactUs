using ContactUsService.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;

namespace ContactUsService.Tests
{
    public class InMemoryDBBaseTest
    {
        public ContactUsDbContext Context { get; private set; }

        [TestInitialize]
        public void SetUp()
        {
            DbConnection effortConnection = Effort.DbConnectionFactory.CreateTransient();
            Context = new ContactUsDbContext(effortConnection);
        }

        [TestCleanup]
        public void TearDown()
        {
            Context.Database.Connection.Close();
            Context.Database.Delete();
            Context.Dispose();
        }
    }
}
