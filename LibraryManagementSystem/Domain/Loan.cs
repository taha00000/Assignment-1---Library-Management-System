using System;

namespace LibraryManagementSystem.Domain
{
    public class Loan : BaseModel
    {
        public Book Book { get; set; } = default!;
        public Member Member { get; set; } = default!;
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
