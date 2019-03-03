using ContactUsService.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Transactions;
using System.Linq;
using System.Data.Entity;

namespace ContactUsService.Tests
{
    public class BaseEFTest
    {
        protected ContactUsDbContext DbContext;

        protected DapperBase queryEngine;

        public static string TESTDBNAME = "TestDatabase";

        [TestInitialize]
        public void TestSetup()
        {
            var connString = ConfigurationManager.ConnectionStrings[TESTDBNAME].ConnectionString;
            DbContext = new ContactUsDbContext(connString);

            //System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlwaysAndSeed<ContactUsDbContext>());
            //DbContext.Database.Initialize(false);

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
        }
    }
}
