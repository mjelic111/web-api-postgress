using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public List<TelephoneNumber> TelephoneNumbers { get; set; }
    }
}
