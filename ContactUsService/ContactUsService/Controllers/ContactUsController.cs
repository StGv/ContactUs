using AutoMapper;
using ContactUsService.Controllers.DTOs;
using ContactUsService.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace ContactUsService.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [RoutePrefix("api/contactus")]
    public class ContactUsController : ApiController
    {
        private readonly ICustomerMessageRepository _repository;

        public ContactUsController(ICustomerMessageRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [ResponseType(typeof(ContactUsFormDTO))]
        [HttpGet]
        [Route("getmessage/{id}", Name = "GetMessagesById")]
        public async Task<IHttpActionResult> GetCustomerMessage(int id)
        {
            var message = await _repository.GetCustomerMessageAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<ContactUsFormDTO>(message);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IHttpActionResult> SubmitMessage([FromBody] ContactUsFormDTO fromData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entityModel = Mapper.Map<Models.CustomerMessage>(fromData);
            var newMessage = await _repository.CreateNewMessageAsync(entityModel);

            if(newMessage == null)
            {
                return InternalServerError();
            }

            return CreatedAtRoute("GetMessagesById", 
                new { id = newMessage.Id }, 
                Mapper.Map<ContactUsFormDTO>(newMessage));
        }
    }
}
