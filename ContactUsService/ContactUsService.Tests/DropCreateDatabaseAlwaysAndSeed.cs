using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactUsService.Tests
{
    internal sealed class DropCreateDatabaseAlwaysAndSeed<TContext> : DropCreateDatabaseAlways<TContext> where TContext : DbContext
    {
        public override void InitializeDatabase(TContext context)
        {
            // This tells the database to close all connections and rolback open transactions.
            // The intent to avoid any open database connections errors during database drop.
            if (context.Database.Exists())
            {
                context.Database.ExecuteSqlCommand(
                    TransactionalBehavior.DoNotEnsureTransaction,
                    $"ALTER DATABASE [{context.Database.Connection.Database}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            }

            base.InitializeDatabase(context);
        }

        protected override void Seed(TContext context)
        {
            // Seed here ...
        }
    }
}
