using AutoMapper;
using ContactUsService.Controllers.DTOs;
using ContactUsService.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ContactUsService.Controllers
{
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
        [Route("{id}")]
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

        [HttpGet]
        public  IHttpActionResult GetCustomepage()
        {
            return Ok($"you have reached ContactUsController");
        }

        [HttpPost]
        public async Task<IHttpActionResult> SubmitMessage([FromBody] ContactUsFormDTO fromData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = Mapper.Map<Models.CustomerMessage>(fromData);
            var newId = await _repository.CreateNewMessageAsync(entity);

            return CreatedAtRoute("DefaultApi", newId, new { Id = newId});
        }
    }
}
