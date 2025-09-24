using LibraryManagementSystem.Data;
using LibraryManagementSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;

public class AuthorsController(LibraryContext context) : Controller
{
    public IActionResult Authors()
    {
        // DO NOT MODIFY ABOVE THIS LINE
        // Fetch all authors and include Books, then return the view
        var authors = context.Authors
            .Include(a => a.Books)
            .ToList();
        return View(authors);
        // DO NOT MODIFY BELOW THIS LINE
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Author author)
    {
        // DO NOT MODIFY ABOVE THIS LINE
        if (ModelState.IsValid)
        {
            if (context.Authors.Any(a => a.Name == author.Name))
            {
                ModelState.AddModelError("Name", "An author with this name already exists.");
                return View(author);
            }
            context.Authors.Add(author);
            context.SaveChanges();
            return RedirectToAction("Authors");
        }
        return View(author);
        // DO NOT MODIFY BELOW THIS LINE
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        // DO NOT MODIFY ABOVE THIS LINE
        var author = context.Authors.Find(id);
        if (author != null)
        {
            context.Authors.Remove(author);
            context.SaveChanges();
            return RedirectToAction("Authors");
        }
        return NotFound();
        // DO NOT MODIFY BELOW THIS LINE
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        // DO NOT MODIFY ABOVE THIS LINE
        var author = context.Authors.Find(id);
        if (author == null)
        {
            return NotFound();
        }
        return View(author);
        // DO NOT MODIFY BELOW THIS LINE
    }

    [HttpPost]
    public IActionResult Update(Author author)
    {
        // DO NOT MODIFY ABOVE THIS LINE
        if (ModelState.IsValid)
        {
            if (context.Authors.Any(a => a.Name == author.Name && a.Id != author.Id))
            {
                ModelState.AddModelError("Name", "An author with this name already exists.");
                return View(author);
            }
            context.Authors.Update(author);
            context.SaveChanges();
            return RedirectToAction("Authors");
        }
        return View(author);
        // DO NOT MODIFY BELOW THIS LINE
    }
}