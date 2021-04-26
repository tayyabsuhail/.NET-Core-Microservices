using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public static class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext)
        {
            orderContext.Database.Migrate();
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>()
            {
                new Order() { UserName = "tayyabsuhail", FirstName = "Tayyab", LastName = "Suhail", EmailAddress = "tayyab@testemail.com", AddressLine = "PK", TotalPrice = 5239 },
                new Order() { UserName = "mike", FirstName = "Michael", LastName = "John", EmailAddress ="mj@testemail.com", AddressLine = "PK", TotalPrice = 3486 }
            };
        }
    }
}
