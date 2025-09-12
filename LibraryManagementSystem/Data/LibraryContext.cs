using Bogus;
using LibraryManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data;

public class LibraryContext(DbContextOptions<LibraryContext> options) : DbContext(options)
{
    // Creating a DbSet in the context maps the domain model to the database table
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Member> Members { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entity framework figures out most of the details based on conventions, but we can override them here
        // we can also define DB specific settings which are not obvious from the domain model
        // As an example, domain model does not care about indexes, they are a data layer concern so they are defined
        // separately.
        modelBuilder.Entity<Loan>().HasIndex(m => m.ReturnDate);
        // DO NOT MODIFY ABOVE THIS LINE
        // TODO: 7. Add indexes for Book.Title, Book.ISBN, Author.Name
        
        // DO NOT MODIFY BELOW THIS LINE
    }
}