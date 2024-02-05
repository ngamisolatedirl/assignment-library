using System;
using System.Collections.Generic;

namespace LibraryData.Models
{
    public partial class Book
    {
        public Book()
        {
            Carts = new HashSet<Cart>();
        }

        public int BookId { get; set; }
        public string? BookName { get; set; }
        public string? Photo { get; set; }
        public string? Author { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
