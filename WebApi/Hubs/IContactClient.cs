using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Hubs
{
    public interface IContactClient
    {
        Task ReceiveContact(int id, ContactModel contact);
    }
}
