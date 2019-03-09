using ContactUsService.Contexts;
using ContactUsService.Services;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.WebApi;

namespace ContactUsService
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<ICustomerMessageRepository, CustomerMessageRepo>(new HierarchicalLifetimeManager());
            container.RegisterType<ContactUsDbContext>(new InjectionConstructor("ContactUsService.Contexts.ContactUsDbContext"));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}