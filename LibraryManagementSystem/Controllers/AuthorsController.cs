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
        // TODO: 11.1 Fetch all authors and return list, include Books for each author and return the view with authors
        // Refer to similar listing for Members
        throw new NotImplementedException("AuthorsController.Authors is not implemented");
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
        // TODO: 11.2 Check if model is valid then add author to context and save changes, then redirect to Authors action
        
        // TODO: 11.3 Return the view with author if model is not valid, errors will be auto populated by the framework
        throw new NotImplementedException("AuthorsController.Add is not implemented");
        // DO NOT MODIFY BELOW THIS LINE
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        // DO NOT MODIFY ABOVE THIS LINE
        // TODO: 11.4 Check if author exists, remove author from context and save changes, then redirect to Authors action
        
        // TODO: 11.5 Return NotFound() if author does not exist
        throw new NotImplementedException("AuthorsController.Delete is not implemented");
        // DO NOT MODIFY BELOW THIS LINE
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        // DO NOT MODIFY ABOVE THIS LINE
        // TODO: 11.6 Find author by id, return NotFound() if author does not exist, otherwise return the view with author
        throw new NotImplementedException("AuthorsController.Update is not implemented");
        // DO NOT MODIFY BELOW THIS LINE
    }

    [HttpPost]
    public IActionResult Update(Author author)
    {
        // DO NOT MODIFY ABOVE THIS LINE
        // TODO: 11.7 Check if model is valid then update author in context and save changes, then redirect to Authors action
        throw new NotImplementedException("AuthorsController.Update is not implemented");
        // DO NOT MODIFY BELOW THIS LINE
    }
}