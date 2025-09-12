using System;
using System.Linq;
using JetBrains.Annotations;
using LibraryManagementSystem.Controllers;
using LibraryManagementSystem.Domain;
using LibraryManagementSystem.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace LibraryManagementSystem.Tests.Controllers;

[TestSubject(typeof(LoansController))]
public class LoansControllerTest : BaseControllerTest
{
    private readonly LoansController _controller;

    public LoansControllerTest()
    {
        _controller = new LoansController(Context);
    }

    [Fact]
    public void LoanBook_ShouldRedirectToLoans_WhenLoanIsSuccessful()
    {
        var book = new Book { Id = 1, Title = "Test Book", ISBN = "123" };
        var member = new Member { Id = 1, Name = "Test Member" };
        Context.Books.Add(book);
        Context.Members.Add(member);
        Context.SaveChanges();

        var result = _controller.LoanBook(book.Id, member.Id);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Loans", redirectToActionResult.ActionName);
        Assert.Contains(Context.Loans, l => l.Book.Id == book.Id && l.Member.Id == member.Id);
    }

    [Fact]
    public void LoanBook_ShouldReturnNotFound_WhenBookOrMemberDoesNotExist()
    {
        var result = _controller.LoanBook(1, 1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void LoanBook_ShouldReturnViewWithError_WhenLoanFails()
    {
        var book = new Book { Id = 1, Title = "Test Book", ISBN = "123" };
        var member = new Member { Id = 1, Name = "Test Member" };
        Context.Books.Add(book);
        Context.Members.Add(member);

        var loanService = new LoanService();
        var loan = loanService.LoanBook(book, member, DateTime.Now);
        Context.Loans.Add(loan);

        Context.SaveChanges();

        var result = _controller.LoanBook(book.Id, member.Id);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("LoanBookForm", viewResult.ViewName);
        Assert.True(_controller.ModelState.ContainsKey(""));
    }

    [Fact]
    public void ReturnBook_ShouldRedirectToLoans_WhenReturnIsSuccessful()
    {
        var book = new Book { Id = 1, Title = "Test Book", ISBN = "123" };
        var member = new Member { Id = 1, Name = "Test Member" };
        var loan = new Loan { Id = 1, Book = book, Member = member, LoanDate = DateTime.Now };
        Context.Books.Add(book);
        Context.Members.Add(member);
        Context.Loans.Add(loan);
        Context.SaveChanges();

        var result = _controller.ReturnBook(loan.Id);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Loans", redirectToActionResult.ActionName);
        Assert.NotNull(Context.Loans.First(l => l.Id == loan.Id).ReturnDate);
    }

    [Fact]
    public void ReturnBook_ShouldReturnNotFound_WhenLoanDoesNotExist()
    {
        var result = _controller.ReturnBook(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void ReturnBook_ShouldReturnViewWithError_WhenReturnFails()
    {
        var book = new Book { Id = 1, Title = "Test Book", ISBN = "123" };
        var member = new Member { Id = 1, Name = "Test Member" };
        var loan = new Loan { Id = 1, Book = book, Member = member, LoanDate = DateTime.Now, ReturnDate = DateTime.Now };
        Context.Books.Add(book);
        Context.Members.Add(member);
        Context.Loans.Add(loan);
        Context.SaveChanges();

        var result = _controller.ReturnBook(loan.Id);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Loans", viewResult.ViewName);
        Assert.True(_controller.ModelState.ContainsKey(""));
    }
}