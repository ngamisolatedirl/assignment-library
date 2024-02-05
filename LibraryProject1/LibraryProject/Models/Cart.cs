using System;
using System.Collections.Generic;

namespace LibraryData.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int? MemberId { get; set; }
        public int? BookId { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Member? Member { get; set; }
    }
}
