using LibraryManagementSystem.Data;
using LibraryManagementSystem.Domain;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;

public class BooksController(LibraryContext context) : Controller
{
    public IActionResult Books()
    {
        var books = context.Books
            .Include(b => b.Loans.Where(l => l.ReturnDate == null))
            // DO NOT MODIFY ABOVE THIS LINE
            // Include Authors in the query
            .Include(b => b.Authors)
            // Notice: We will have to use SQL Joins if we were not using ORM like Entity Framework
            
            // DO NOT MODIFY BELOW THIS LINE
            .ToList();
        return View(books);
    }

    public IActionResult Add()
    {
        var model = new BookViewModel
        {
            AvailableAuthors = context.Authors.ToList()
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Add(BookViewModel model)
    {
        if (context.Books.Any(b => b.ISBN == model.ISBN))
        {
            ModelState.AddModelError("ISBN", "A book with this ISBN already exists.");
        }

        if (ModelState.IsValid)
        {
            var book = new Book
            {
                Title = model.Title,
                ISBN = model.ISBN,
                Authors = context.Authors.Where(a => model.SelectedAuthorIds != null && model.SelectedAuthorIds.Contains(a.Id)).ToList()
            };
            context.Books.Add(book);
            context.SaveChanges();
            return RedirectToAction("Books");
        }
        model.AvailableAuthors = context.Authors.ToList();
        return View(model);
    }

    public IActionResult Update(int id)
    {
        var book = context.Books.Include(b => b.Authors).FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return NotFound();
        }
        var model = new BookViewModel
        {
            Id = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            SelectedAuthorIds = book.Authors.Select(a => a.Id).ToList(),
            AvailableAuthors = context.Authors.ToList()
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Update(BookViewModel model)
    {
        if (context.Books.Any(b => b.ISBN == model.ISBN && b.Id != model.Id))
        {
            ModelState.AddModelError("ISBN", "A book with this ISBN already exists.");
        }

        if (ModelState.IsValid)
        {
            var book = context.Books.Include(b => b.Authors).FirstOrDefault(b => b.Id == model.Id);
            if (book == null)
            {
                return NotFound();
            }
            book.Title = model.Title;
            book.ISBN = model.ISBN;
            book.Authors = context.Authors.Where(a => model.SelectedAuthorIds != null && model.SelectedAuthorIds.Contains(a.Id)).ToList();
            context.SaveChanges();
            return RedirectToAction("Books");
        }
        model.AvailableAuthors = context.Authors.ToList();
        return View(model);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var book = context.Books.Find(id);
        if (book != null)
        {
            context.Books.Remove(book);
            context.SaveChanges();
            return RedirectToAction("Books");
        }
        return NotFound();
    }

}