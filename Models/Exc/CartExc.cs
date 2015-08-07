using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public partial class Cart
    {
        public Cart(Customer customer)
        {
            this.customerId = customer.id;
            this.price = 0;
            this.num = 0;
            this.Customer = customer;
            this.CartItems = new HashSet<CartItem>();
        }
    }
}
