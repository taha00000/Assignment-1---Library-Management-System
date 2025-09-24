using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using LibraryManagementSystem.Controllers;
using LibraryManagementSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace LibraryManagementSystem.Tests.Controllers;

[TestSubject(typeof(MembersController))]
public class MembersControllerTest : BaseControllerTest
{
    private readonly MembersController _controller;

    public MembersControllerTest()
    {
        _controller = new MembersController(Context);
    }

    [Fact]
    public void Members_ShouldReturnViewWithMembers()
    {
        var members = new List<Member>
        {
            new Member { Id = 1, Name = "Member 1" },
            new Member { Id = 2, Name = "Member 2" }
        };
        Context.Members.AddRange(members);
        Context.SaveChanges();

        var result = _controller.Members();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Member>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public void Add_ShouldRedirectToMembers_WhenModelStateIsValid()
    {
        var member = new Member { Id = 1, Name = "New Member" };

        var result = _controller.Add(member);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Members", redirectToActionResult.ActionName);
        Assert.Contains(member, Context.Members);
    }

    [Fact]
    public void Add_ShouldReturnView_WhenModelStateIsInvalid()
    {
        _controller.ModelState.AddModelError("Name", "Required");
        var member = new Member { Id = 1, Name = "" };

        var result = _controller.Add(member);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(member, viewResult.Model);
    }

    [Fact]
    public void Delete_ShouldRedirectToMembers_WhenMemberExists()
    {
        var member = new Member { Id = 1, Name = "Member to Delete" };
        Context.Members.Add(member);
        Context.SaveChanges();

        var result = _controller.Delete(1);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Members", redirectToActionResult.ActionName);
        Assert.DoesNotContain(member, Context.Members);
    }

    [Fact]
    public void Delete_ShouldReturnNotFound_WhenMemberDoesNotExist()
    {
        var result = _controller.Delete(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Update_ShouldRedirectToMembers_WhenModelStateIsValid()
    {
        var member = new Member { Id = 1, Name = "Updated Member" };
        Context.Members.Add(member);
        Context.SaveChanges();

        var result = _controller.Update(member);

        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Members", redirectToActionResult.ActionName);
        Assert.Contains(member, Context.Members);
    }

    [Fact]
    public void Update_ShouldReturnView_WhenModelStateIsInvalid()
    {
        _controller.ModelState.AddModelError("Name", "Required");
        var member = new Member { Id = 1, Name = "" };

        var result = _controller.Update(member);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(member, viewResult.Model);
    }

    [Fact]
    public void Add_ShouldReturnView_WhenMemberNameIsDuplicate()
    {
        var existingMember = new Member { Name = "Duplicate Name" };
        Context.Members.Add(existingMember);
        Context.SaveChanges();

        var newMember = new Member { Name = "Duplicate Name" };
        var result = _controller.Add(newMember);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.False(_controller.ModelState.IsValid);
        Assert.Equal("A member with this name already exists.", _controller.ModelState["Name"].Errors[0].ErrorMessage);
    }

    [Fact]
    public void Update_ShouldReturnView_WhenMemberNameIsDuplicate()
    {
        var member1 = new Member { Id = 1, Name = "Member 1" };
        var member2 = new Member { Id = 2, Name = "Member 2" };
        Context.Members.AddRange(member1, member2);
        Context.SaveChanges();

        member2.Name = "Member 1";
        var result = _controller.Update(member2);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.False(_controller.ModelState.IsValid);
        Assert.Equal("A member with this name already exists.", _controller.ModelState["Name"].Errors[0].ErrorMessage);
    }
}