using Common.Models;
using DataAccessLayer.Errors;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IRepository<Contact> repository;

        public ContactController(IRepository<Contact> repository)
        {
            this.repository = repository;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<ContactModel>>> GetAll()
        {
            try
            {
                var contacts = await repository.Include(nameof(Contact.TelephoneNumbers)).GetAllAsync();
                var mapped = contacts.Select(c => new ContactModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    BirthDate = c.BirthDate,
                    TelephoneNumbers = c.TelephoneNumbers.Select(t => t.Number).AsEnumerable()
                }).ToList();
                return Ok(mapped);

            }
            catch (EntityNotFoundException e)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<ContactModel>> Get(int id)
        {
            try
            {
                var contact = await repository.Include(nameof(Contact.TelephoneNumbers)).GetByIdAsync(id);
                var contactModel = new ContactModel
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Address = contact.Address,
                    BirthDate = contact.BirthDate,
                    TelephoneNumbers = contact.TelephoneNumbers.Select(t => t.Number).AsEnumerable()
                };
                return Ok(contactModel);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
