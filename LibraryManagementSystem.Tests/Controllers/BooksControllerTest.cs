using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Controllers;
using LibraryManagementSystem.Domain;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace LibraryManagementSystem.Tests.Controllers;

public class BooksControllerTest : BaseControllerTest
{
    private readonly BooksController _controller;

    public BooksControllerTest()
    {
        _controller = new BooksController(Context);
    }

    [Fact]
    public void Books_ShouldReturnViewWithBooks()
    {
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", ISBN = "123" },
            new Book { Id = 2, Title = "Book 2", ISBN = "123" }
        };
        Context.Books.AddRange(books);
        Context.SaveChanges();

        var result = _controller.Books();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Book>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public void Add_ShouldRedirectToBooks_WhenModelStateIsValid()
    {
        var viewModel = new BookViewModel
        {
            Title = "New Book",
            ISBN = "123",
            SelectedAuthorIds = new List<int> { 1 }
        };
        var author = new Author { Id = 1, Name = "Author 1" };
        Context.Authors.Add(author);
        Context.SaveChanges();

        var result = _controller.Add(viewModel);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Books", redirectToActionResult.ActionName);
        Assert.Contains(Context.Books, b => b is { Title: "New Book", ISBN: "123" } && b.Authors.Contains(author));
    }

    [Fact]
    public void Add_ShouldReturnView_WhenModelStateIsInvalid()
    {
        _controller.ModelState.AddModelError("Title", "Required");
        var viewModel = new BookViewModel { Title = "", ISBN = "123" };

        var result = _controller.Add(viewModel);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(viewModel, viewResult.Model);
    }

    [Fact]
    public void Update_ShouldRedirectToBooks_WhenModelStateIsValid()
    {
        var book = new Book { Id = 1, Title = "Old Book", ISBN = "123" };
        var author = new Author { Id = 1, Name = "Author 1" };
        Context.Books.Add(book);
        Context.Authors.Add(author);
        Context.SaveChanges();

        var viewModel = new BookViewModel
        {
            Id = 1,
            Title = "Updated Book",
            ISBN = "123",
            SelectedAuthorIds = new List<int> { 1 }
        };

        var result = _controller.Update(viewModel);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Books", redirectToActionResult.ActionName);
        Assert.Contains(Context.Books, b => b is { Title: "Updated Book", ISBN: "123" } && b.Authors.Contains(author));
    }

    [Fact]
    public void Update_ShouldReturnView_WhenModelStateIsInvalid()
    {
        _controller.ModelState.AddModelError("Title", "Required");
        var viewModel = new BookViewModel { Id = 1, Title = "", ISBN = "123" };

        var result = _controller.Update(viewModel);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(viewModel, viewResult.Model);
    }

    [Fact]
    public void Delete_ShouldRedirectToBooks_WhenBookExists()
    {
        var book = new Book { Id = 1, Title = "Book to Delete", ISBN = "123" };
        Context.Books.Add(book);
        Context.SaveChanges();

        var result = _controller.Delete(1);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Books", redirectToActionResult.ActionName);
        Assert.DoesNotContain(book, Context.Books);
    }

    [Fact]
    public void Delete_ShouldReturnNotFound_WhenBookDoesNotExist()
    {
        var result = _controller.Delete(1);

        Assert.IsType<NotFoundResult>(result);
    }
}