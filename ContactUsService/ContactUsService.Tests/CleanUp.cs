using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

namespace ContactUsService.Tests
{
    [TestClass]
    public static class CleanUp
    {
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Database.Delete(BaseEFTest.TESTDBNAME);
        }
    }
}
