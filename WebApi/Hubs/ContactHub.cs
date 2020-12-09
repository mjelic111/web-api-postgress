using Common.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Hubs
{
    public class ContactHub : Hub<IContactClient>
    {
        public Task UpdateContact(int id, ContactModel contact)
        {
            return Clients.All.ReceiveContact(id, contact);
        }
    }
}
