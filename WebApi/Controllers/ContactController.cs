﻿using Common.Models;
using DataAccessLayer.Errors;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Http;
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
        private readonly IRepository<TelephoneNumber> telephoneNumberRepository;

        public ContactController(IRepository<Contact> repository, IRepository<TelephoneNumber> telephoneNumberRepository)
        {
            this.repository = repository;
            this.telephoneNumberRepository = telephoneNumberRepository;
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
            catch (Exception e)
            {
                return handleError(e);
            }
        }

        [HttpGet("{id}")]
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
                    TelephoneNumbers = contact.TelephoneNumbers.Where(t => t.Deleted == false).Select(t => t.Number).AsEnumerable()
                };
                return Ok(contactModel);
            }
            catch (Exception e)
            {
                return handleError(e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ContactModel>> PostContact(ContactModel contact)
        {
            try
            {
                var mappedContact = new Contact
                {
                    //Id = Db autogenerated,
                    Name = contact.Name,
                    Address = contact.Address,
                    BirthDate = contact.BirthDate,
                    TelephoneNumbers = new List<TelephoneNumber>()
                };

                // update telephone numbers
                foreach (var t in contact.TelephoneNumbers)
                {
                    mappedContact.TelephoneNumbers.Add(new TelephoneNumber { Number = t });
                }

                var newId = await repository.InsertAsync(mappedContact);
                contact.Id = newId;
                return CreatedAtAction("PostContact", new { id = contact.Id }, contact);
            }
            catch (Exception e)
            {
                return handleError(e);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, ContactModel contact)
        {
            try
            {
                // get original contact
                var originalContact = await repository.Include(nameof(Contact.TelephoneNumbers)).GetByIdAsync(contact.Id);

                // check is deleted
                if (originalContact.Deleted)
                {
                    return NotFound();
                }

                var mappedContact = new Contact
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Address = contact.Address,
                    BirthDate = contact.BirthDate,
                    CreatedAt = originalContact.CreatedAt,
                    Deleted = false,
                    TelephoneNumbers = new List<TelephoneNumber>()
                };

                await repository.UpdateAsync(mappedContact, true);
                // merge telephone numbers
                var updatedTelNumbers = contact.TelephoneNumbers.ToList();
                var originalTelNumbers = originalContact.TelephoneNumbers.Select(tel => tel.Number).ToList();

                foreach (var t in originalTelNumbers)
                {
                    if (updatedTelNumbers.Contains(t))
                    {
                        // no need to update remove it from list
                        updatedTelNumbers.Remove(t);
                    }
                    else
                    {
                        // not found so mark it as deleted
                        var orig = originalContact.TelephoneNumbers.Where(e => e.Number == t).Single();
                        orig.Deleted = true;
                        await telephoneNumberRepository.UpdateAsync(orig, true);
                    }
                }

                // create new
                updatedTelNumbers.ForEach(t => telephoneNumberRepository.InsertAsync(new TelephoneNumber { Number = t, ContactId = contact.Id }, true));

                // save changes
                await telephoneNumberRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return handleError(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                await repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return handleError(e);
            }
        }

        private ActionResult handleError(Exception e)
        {
            if (e is EntityNotFoundException)
            {
                return NotFound(e.Message);
            }

            return BadRequest(e.Message);
        }
    }
}
