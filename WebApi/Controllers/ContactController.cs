﻿using Common.Models;
using DataAccessLayer.Errors;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Mapper;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IRepository<Contact> repository;
        private readonly IRepository<TelephoneNumber> telephoneNumberRepository;
        private readonly IOptions<PaginationSettings> paginationSettings;

        public ContactController(IRepository<Contact> repository, IRepository<TelephoneNumber> telephoneNumberRepository, IOptions<PaginationSettings> paginationSettings)
        {
            this.repository = repository;
            this.telephoneNumberRepository = telephoneNumberRepository;
            this.paginationSettings = paginationSettings;
        }

        [HttpGet("get/{pageNumber}")]
        public async Task<ActionResult<IEnumerable<ContactModel>>> GetAll(int pageNumber = 1)
        {
            try
            {
                if (pageNumber < 1)
                {
                    return BadRequest("page number not valid");
                }
                var pageSize = paginationSettings.Value.PageSize;
                var contacts = await repository.Include(nameof(Contact.TelephoneNumbers)).GetAllAsync(pageNumber, pageSize);
                var mapped = contacts.Select(c => c.MapToDto()).ToList();
                // pagination
                return await mapToPaginationModel(pageNumber, pageSize, mapped);

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
                var contactModel = contact.MapToDto();
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
                var mappedContact = contact.MapToModel();
                mappedContact.Id = 0; // DB autogenerated

                var newId = await repository.InsertAsync(mappedContact);
                contact.Id = newId;
                return CreatedAtAction("PostContact", new { id = contact.Id }, contact);
            }
            catch (Exception e)
            {
                return handleError(e);
            }

        }

        [HttpPut]
        public async Task<IActionResult> PutContact(ContactModel contact)
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

                var mappedContact = contact.MapToModel();
                mappedContact.CreatedAt = originalContact.CreatedAt;
                mappedContact.Deleted = false;

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
                        await telephoneNumberRepository.DeleteAsync(orig.Id, true);
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

        private async Task<ActionResult<IEnumerable<ContactModel>>> mapToPaginationModel(int pageNumber, int pageSize, List<ContactModel> mapped)
        {
            var totalRecords = await repository.GetCount();
            var totalPages = totalRecords % pageSize == 0 ? Convert.ToInt32((double)totalRecords / pageSize) : Convert.ToInt32((double)totalRecords / pageSize + 0.5);

            if (pageNumber > totalPages)
            {
                return BadRequest("page number not valid");
            }

            var result = new PaginatedResult<List<ContactModel>>
            {
                Previous = pageNumber > 1 ? this.Url.ActionLink("GetAll", "Contact", new { pageNumber = pageNumber - 1 }) : "",
                Next = pageNumber < totalPages ? this.Url.ActionLink("GetAll", "Contact", new { pageNumber = pageNumber + 1 }) : "",
                Current = pageNumber,
                TotalPages = totalPages,
                TotalRecords = totalRecords,
                Data = mapped
            };
            return Ok(result);
        }
    }
}
