using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Controllers;
using LibraryManagementSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace LibraryManagementSystem.Tests.Controllers;

public class AuthorsControllerTest : BaseControllerTest
{
    private readonly AuthorsController _controller;

    public AuthorsControllerTest()
    {
        _controller = new AuthorsController(Context);
    }

    [Fact]
    public void Authors_ShouldReturnViewWithAuthors()
    {
        var authors = new List<Author>
        {
            new Author { Id = 1, Name = "Author 1" },
            new Author { Id = 2, Name = "Author 2" }
        };
        Context.Authors.AddRange(authors);
        Context.SaveChanges();

        var result = _controller.Authors();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Author>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public void Add_ShouldRedirectToAuthors_WhenModelStateIsValid()
    {
        var author = new Author { Id = 1, Name = "New Author" };

        var result = _controller.Add(author);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Authors", redirectToActionResult.ActionName);
        Assert.Contains(author, Context.Authors);
    }

    [Fact]
    public void Add_ShouldReturnView_WhenModelStateIsInvalid()
    {
        _controller.ModelState.AddModelError("Name", "Required");
        var author = new Author { Id = 1, Name = "" };

        var result = _controller.Add(author);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(author, viewResult.Model);
    }

    [Fact]
    public void Delete_ShouldRedirectToAuthors_WhenAuthorExists()
    {
        var author = new Author { Id = 1, Name = "Author to Delete" };
        Context.Authors.Add(author);
        Context.SaveChanges();

        var result = _controller.Delete(1);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Authors", redirectToActionResult.ActionName);
        Assert.DoesNotContain(author, Context.Authors);
    }

    [Fact]
    public void Delete_ShouldReturnNotFound_WhenAuthorDoesNotExist()
    {
        var result = _controller.Delete(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Update_ShouldRedirectToAuthors_WhenModelStateIsValid()
    {
        var author = new Author { Id = 1, Name = "Updated Author" };
        Context.Authors.Add(author);
        Context.SaveChanges();

        var result = _controller.Update(author);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Authors", redirectToActionResult.ActionName);
        Assert.Contains(author, Context.Authors);
    }

    [Fact]
    public void Update_ShouldReturnView_WhenModelStateIsInvalid()
    {
        _controller.ModelState.AddModelError("Name", "Required");
        var author = new Author { Id = 1, Name = "" };

        var result = _controller.Update(author);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(author, viewResult.Model);
    }
}