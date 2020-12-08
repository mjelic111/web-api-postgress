using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TelephoneNumber : BaseEntity
    {
        public string Number { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
