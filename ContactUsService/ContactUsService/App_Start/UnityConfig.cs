using ContactUsService.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ContactUsService
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<ICustomerMessageRepository, CustomerMessageRepo>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}