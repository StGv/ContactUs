﻿using AutoMapper;
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
        [Route("getmessage/{id}")]
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
            var entityModel = Mapper.Map<Models.CustomerMessage>(fromData);
            var created = await _repository.CreateNewMessageAsync(entityModel);

            //return CreatedAtRoute("api/contactus/getmessage/{id}", newId, new { Id = newId});
            return Ok(created > 0);
        }
    }
}
