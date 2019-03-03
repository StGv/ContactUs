using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactUsService.Tests
{
    [TestClass]
    public static class GlobalSetUp
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            ContactUsService.AutoMapperConfig.Initialize();
        }
    }
}
