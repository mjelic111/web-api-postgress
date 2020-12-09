using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<string> TelephoneNumbers { get; set; }
    }
}
