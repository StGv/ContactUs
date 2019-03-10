using ContactUsService.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ContactUsService.Tests
{
    public class TestDBInstanceBaseTest
    {
        protected ContactUsDbContext DbContext;

        protected DapperBase queryEngine;

        public static string TESTDBNAME = "TestDatabase";

        [TestInitialize]
        public void TestSetup()
        {
            DbContext = new ContactUsDbContext(TESTDBNAME);
            DbContext.Database.CreateIfNotExists();
            queryEngine = new DapperBase(DbContext.Database.Connection);
            
        }

        [TestCleanup]
        public void TestCleanup()
        {
            IEnumerable<DbEntityEntry> changedEntriesCopy = DbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted
                );
            foreach (DbEntityEntry entity in changedEntriesCopy)
            {
                DbContext.Entry(entity.Entity).State = EntityState.Detached;
            }

            Database.Delete(TESTDBNAME);
        }
    }
}
