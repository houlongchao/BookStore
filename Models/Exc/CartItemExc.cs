using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public partial class CartItem
    {
        public CartItem()
        {
        }
        public CartItem(Book book)
        {
            this.bookId = book.id;
            this.Book = book;
            this.num = 0;
            this.price = 0;
        }

    }
}
