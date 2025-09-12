using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Domain;
using LibraryManagementSystem.Domain.Services;

namespace LibraryManagementSystem.Controllers
{
    public class LoansController(LibraryContext context) : Controller
    {
        private readonly LoanService _loanService = new();

        [HttpGet]
        public IActionResult LoanBookForm()
        {
            var books = context.Books.ToList();
            var members = context.Members.ToList();
            ViewBag.Books = books;
            ViewBag.Members = members;
            return View();
        }

        [HttpPost]
        public IActionResult LoanBook(int bookId, int memberId)
        {
            var book = context.Books.
                Include(b => b.Loans).FirstOrDefault(b => b.Id == bookId);
            var member = context.Members.
                Include(m => m.Loans).FirstOrDefault(m => m.Id == memberId);

            if (book == null || member == null)
            {
                return NotFound();
            }

            try
            {
                var loan = _loanService.LoanBook(book, member, DateTime.Now);
                context.Loans.Add(loan);
                context.SaveChanges();
                return RedirectToAction("Loans");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                var books = context.Books.ToList();
                var members = context.Members.ToList();
                ViewBag.Books = books;
                ViewBag.Members = members;
                return View("LoanBookForm");
            }
        }

        [HttpGet]
        public IActionResult Loans()
        {
            var loans = ActiveLoans();
            return View(loans);
        }

        private List<Loan> ActiveLoans()
        {
            var loans = context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.ReturnDate == null)
                .ToList();
            return loans;
        }

        [HttpPost]
        public IActionResult ReturnBook(int loanId)
        {
            var loan = context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefault(l => l.Id == loanId);

            if (loan == null)
            {
                return NotFound();
            }

            try
            {
                _loanService.ReturnBook(loan);
                context.SaveChanges();
                return RedirectToAction("Loans");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                var loans = ActiveLoans();
                return View("Loans", loans);
            }
        }
    }
}
