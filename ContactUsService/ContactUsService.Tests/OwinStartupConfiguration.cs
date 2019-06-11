using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ContactUsService.Tests.OwinStartupConfiguration))]

namespace ContactUsService.Tests
{
    public class OwinStartupConfiguration
    {
        public void Configuration(IAppBuilder app)
        {
            //HttpConfiguration config = new HttpConfiguration();
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //app.UseWebApi(config);

            app.Use((context, next) =>
            {
                System.IO.TextWriter output = context.Get<System.IO.TextWriter>("host.TraceOutput");
                return next().ContinueWith(result =>
                {
                    output.WriteLine("Scheme {0} : Method {1} : Path {2} : MS {3}",
                    context.Request.Scheme, context.Request.Method, context.Request.Path, getTime());
                });
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync(getTime() + " My First OWIN App");
            });
        }

        string getTime()
        {
            return System.DateTime.Now.Millisecond.ToString();
        }
  
    }
}
