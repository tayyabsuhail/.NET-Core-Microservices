using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class BasketCart
    {
        public string userName { get; set; }
        public List<BasketCartItems> Items { get; set; } = new List<BasketCartItems>();

        public BasketCart()
        {
        }

        public BasketCart(string userName)
        {
            this.userName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }

                return totalprice;
            }
        }
    }
}
