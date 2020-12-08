using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class DatabasebContext: DbContext
    {
        public DatabasebContext(DbContextOptions<DatabasebContext> options): base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }

    }
}
