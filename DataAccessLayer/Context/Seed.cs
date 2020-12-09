using DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Context
{
    public static class Seed
    {
        public static List<Contact> Contacts = new List<Contact>()
        {
            new Contact{Id = 1, Name="Test contact 1", Address="Address 1", BirthDate=DateTime.Now.AddYears(-1) , CreatedAt= DateTime.Now}
        };
        public static List<TelephoneNumber> Numbers = new List<TelephoneNumber>()
        {
            new TelephoneNumber{Id= 1, ContactId=1, Number="098444777" , CreatedAt= DateTime.Now},
            new TelephoneNumber{Id= 2, ContactId=1, Number="098555666" , CreatedAt= DateTime.Now}
        };
    }
}
