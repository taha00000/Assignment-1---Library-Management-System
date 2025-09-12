using System;
using System.Linq;
using JetBrains.Annotations;
using LibraryManagementSystem.Domain;
using LibraryManagementSystem.Domain.Services;
using Moq;
using Xunit;

namespace LibraryManagementSystem.Tests.Domain.Services;

[TestSubject(typeof(LoanService))]
public class LoanServiceTest
{
    [Fact]
    public void LoanBook_ShouldCreateLoan_WhenBookIsAvailableAndMemberCanBorrow()
    {
        var book = new Book { Id = 1, Title = "Test Book" };
        var member = new Member { Id = 1, Name = "Test Member" };
        var loanDate = DateTime.Now;
        var loanService = new LoanService();

        var loan = loanService.LoanBook(book, member, loanDate);

        Assert.NotNull(loan);
        Assert.Equal(book, loan.Book);
        Assert.Equal(member, loan.Member);
        Assert.Equal(loanDate, loan.LoanDate);
        Assert.Null(loan.ReturnDate);
        Assert.Contains(loan, member.Loans);
    }

    [Fact]
    public void LoanBook_ShouldThrowException_WhenBookIsNotAvailable()
    {
        var book = new Mock<Book>();
        book.Setup(b => b.IsAvailable()).Returns(false);
        var member = new Member { Id = 1, Name = "Test Member" };
        var loanDate = DateTime.Now;
        var loanService = new LoanService();

        Assert.Throws<InvalidOperationException>(() => loanService.LoanBook(book.Object, member, loanDate));
    }

    [Fact]
    public void LoanBook_ShouldThrowException_WhenMemberCannotBorrow()
    {
        var book = new Book { Id = 1, Title = "Test Book" };
        var member = new Mock<Member>();
        member.Setup(m => m.CanBorrow()).Returns(false);
        var loanDate = DateTime.Now;
        var loanService = new LoanService();

        Assert.Throws<InvalidOperationException>(() => loanService.LoanBook(book, member.Object, loanDate));
    }

    [Fact]
    public void ReturnBook_ShouldSetReturnDateAndRemoveLoanFromMember()
    {
        var member = new Member { Id = 1, Name = "Test Member" };
        var loan = new Loan { Id = 1, Member = member, LoanDate = DateTime.Now };
        member.RecordLoan(loan);
        var loanService = new LoanService();

        loanService.ReturnBook(loan);

        Assert.NotNull(loan.ReturnDate);
        Assert.NotNull(member.Loans.First(l => l.Id == loan.Id).ReturnDate);
    }

    [Fact]
    public void ReturnBook_ShouldThrowException_WhenBookIsAlreadyReturned()
    {
        var member = new Member { Id = 1, Name = "Test Member" };
        var loan = new Loan { Id = 1, Member = member, LoanDate = DateTime.Now, ReturnDate = DateTime.Now };
        var loanService = new LoanService();

        Assert.Throws<InvalidOperationException>(() => loanService.ReturnBook(loan));
    }
}