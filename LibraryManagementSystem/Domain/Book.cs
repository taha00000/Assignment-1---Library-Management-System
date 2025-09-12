using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LibraryManagementSystem.Domain
{
    // Notice that BaseModel does not have a corresponding table in the database.
    // Similarly, Book and Author have many-to-many relationship so that requires a third table in the 
    // database called BookAuthor or AuthorBook, but we do not have a class for that.
    // The class hierarchies do not map one-to-one to the database.
    // The ORM (Entity Framework Core), despite these differences, maps the relationships between the classes and database.
    public class Book : BaseModel
    {
        public string Title { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        // A book may have been written by many authors
        // This will make the relationship between Book and Author many-to-many
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public virtual bool IsAvailable()
        {
            // DO NOT MODIFY ABOVE THIS LINE
            // This method should return true if the book is not currently on loan (No entry in Loans collection)
            // or if it was on loan but has been returned (loan.ReturnDate is not null for all Loans)
            // TODO: 1.1 Implement the IsAvailable method

            throw new NotImplementedException("Book.IsAvailable is not implemented");
            // DO NOT MODIFY BELOW THIS LINE
        }

        
        // This is an example of leaking presentation logic in Domain model. It has nothing to do with domain.
        // Ideally, this method should be part of presentation layer, perhaps in BookViewModel class.
        public string AuthorsToString()
        {
            // DO NOT MODIFY ABOVE THIS LINE
            // This method should return a string with the names of the authors of the book separated by commas
            // If the book has multiple authors, the names should be separated by commas and the last name should be preceded by 'and'
            // If the book has only one author, the name should be returned as is or "unknown" if the author's name is null
            // If the book has no authors, an empty string should be returned
            // TODO: 1.2 Implement the AuthorsToString method
            throw new NotImplementedException("Book.AuthorsToString is not implemented");
            // DO NOT MODIFY BELOW THIS LINE
        }
    }
}
