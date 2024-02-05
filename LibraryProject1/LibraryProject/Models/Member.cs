using System;
using System.Collections.Generic;

namespace LibraryData.Models
{
    public partial class Member
    {
        public Member()
        {
            Carts = new HashSet<Cart>();
        }

        public int MemberId { get; set; }
        public string? FulltName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}
