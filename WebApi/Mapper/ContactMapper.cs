using Common.Models;
using DataAccessLayer.Models;
using System.Linq;

namespace WebApi.Mapper
{
    public static class ContactMapper
    {
        public static ContactModel MapToDto(this Contact contact)
        {
            return new ContactModel
            {
                Id = contact.Id,
                Name = contact.Name,
                Address = contact.Address,
                BirthDate = contact.BirthDate,
                TelephoneNumbers = contact.TelephoneNumbers/*.Where(t => t.Deleted == false)*/.Select(t => t.Number).AsEnumerable()
            };
        }

        public static Contact MapToModel(this ContactModel contact)
        {
            return new Contact
            {
                Id = contact.Id,
                Name = contact.Name,
                Address = contact.Address,
                BirthDate = contact.BirthDate,
                TelephoneNumbers = contact.TelephoneNumbers.Select(t => new TelephoneNumber { Number = t }).ToList()
            };
        }
    }
}
