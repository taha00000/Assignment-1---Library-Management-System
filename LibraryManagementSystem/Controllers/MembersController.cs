using LibraryManagementSystem.Data;
using LibraryManagementSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;

public class MembersController(LibraryContext context) : Controller
{
    public IActionResult Members()
    {
        var members = context.Members
            .Include(m => m.Loans.Where(l => l.ReturnDate == null))
            .ToList();
        return View(members);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Member member)
    {
        if (ModelState.IsValid)
        {
            context.Members.Add(member);
            context.SaveChanges();
            return RedirectToAction("Members");
        }
        return View(member);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var member = context.Members.Find(id);
        if (member != null)
        {
            context.Members.Remove(member);
            context.SaveChanges();
            return RedirectToAction("Members");
        }
        return NotFound();
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var member = context.Members.Find(id);
        if (member == null)
        {
            return NotFound();
        }
        return View(member);
    }

    [HttpPost]
    public IActionResult Update(Member member)
    {
        if (ModelState.IsValid)
        {
            context.Members.Update(member);
            context.SaveChanges();
            return RedirectToAction("Members");
        }
        return View(member);
    }
}