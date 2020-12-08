using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Context
{
    public static class Seed
    {
        public static List<Contact> Contacts = new List<Contact>()
        {
            new Contact{Id = 1, Name="Test contact 1", Address="Address 1", BirthDate=DateTime.Now.AddYears(-1)}
        };
        public static List<TelephoneNumber> Numbers = new List<TelephoneNumber>()
        {
            new TelephoneNumber{Id= 1, ContactId=1, Number="098444777"},
            new TelephoneNumber{Id= 2, ContactId=1, Number="098555666"}
        };
    }
}
