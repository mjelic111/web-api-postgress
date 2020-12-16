using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi;
using Xunit;
using DataAccessLayer.Context;
using System.Linq;
using Common.Models;
using System.Net.Http.Json;
using System.Collections.Generic;

namespace IntegrationTests
{
    public class ContactTests
    {
        private HttpClient client;

        public ContactTests()
        {
            var factory = new WebApplicationFactory<Startup>().WithInMemoryDatabase<Startup, DatabaseContext>();
            client = factory.CreateClient();
        }



        [Fact]
        public async Task CreateContact()
        {
            // arrange
            var contactResponse = await client.PostAsJsonAsync("/api/contact",
            new ContactModel { Name = "Test", Address = "Address", BirthDate = DateTime.Now, TelephoneNumbers = new List<string>() });
            contactResponse.EnsureSuccessStatusCode();
            
            // act
            var response = await client.GetAsync("api/contact/get/1");

            // assert
            response.EnsureSuccessStatusCode();
            // Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
