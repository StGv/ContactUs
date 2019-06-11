using ContactUsService.Controllers;
using ContactUsService.Controllers.DTOs;
using ContactUsService.Services;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http;

namespace ContactUsService.Tests
{
    [TestClass]
    public class ContactUsControllerUnitTests : BaseEFTest
    {

        ContactUsController _target;

        [TestInitialize]
        public void Before()
        {
            _target = new ContactUsController(new CustomerMessageRepo(base.DbContext));
        }

        [TestMethod]
        public void testGetMessageWhenRecordIsPresent()
        {

            var fromData = new ContactUsFormDTO()
            {
                fullName = "Joseph Smith",
                email = "smith.joseph@gmail.com",
                message = "this is a very long message!"
            };


            var taskPost = _target.SubmitMessage(fromData);
            taskPost.Wait();

            HttpConfiguration config = new HttpConfiguration();
            //config.Routes.MapHttpRoute("Default", "api/{controller}/{id}");
            ContactUsService.WebApiConfig.Register(config);

            using (HttpServer server = new HttpServer(config))
            using (HttpMessageInvoker client = new HttpMessageInvoker(server))
            {
                HttpClient request = new HttpClient();
                var response = request.GetAsync("http://localhost/api/contactus/getmessage/1").Result;
                
                //using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/contactus/getmessage/1"))
                //using (HttpResponseMessage response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    //var stringPayload = JsonConvert.SerializeObject(fromData);
                    //Assert.AreEqual(stringPayload, result);
                    //Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                }
            };
        }

        [TestMethod]
        public void PostMessage_ReturnsBadRequest_WhenEmailIsInvalid()
        {
            var fromData = new ContactUsFormDTO()
            {
                fullName = "Joseph Smith",
                email = "smith.joseph@gmail.com",
                message = "this is a very long message!"
            };

            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                            name: "DefaultApi",
                            routeTemplate: "api/{controller}/{id}",
                            defaults: new { id = RouteParameter.Optional }
                        );
            HttpServer server = new HttpServer(config);
            using (HttpMessageInvoker client = new HttpMessageInvoker(server))
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/contactus"))
                {
                    var stringPayload = JsonConvert.SerializeObject(fromData);
                    request.Content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = client.SendAsync(request, CancellationToken.None).Result)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        Assert.AreEqual(stringPayload, result);
                        //var content = Assert.IsTrue(typeof(response.Content) is StringContent);
                        //Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                    }
                }
                
            };
        }

        [TestMethod]
        public void test_test()
        {
            var fromData = new ContactUsFormDTO()
            {
                fullName = "Joseph Smith",
                email = "smith.joseph@gmail.com",
                message = "this is a very long message!"
            };


            var taskPost = _target.SubmitMessage(fromData);
            taskPost.Wait();

            string url = "http://localhost:9000";
            using (WebApp.Start<OwinStartupConfiguration>(url))
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = client.GetAsync(url + "/api/contactus/getmessage/1").Result;
                    
                    var stringPayload = JsonConvert.SerializeObject(fromData);
                    Assert.AreEqual(stringPayload, response);
                }

            }
        }
    }
}
