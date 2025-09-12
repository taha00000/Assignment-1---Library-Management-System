using Bogus;
using LibraryManagementSystem.Domain;

namespace LibraryManagementSystem.Data.Fakers;

public sealed class BookFaker : Faker<Book>
{
    public BookFaker()
    {
        RuleFor(b => b.Title, f => f.Commerce.ProductName());
        RuleFor(b => b.ISBN, f => f.Commerce.Ean13());
    }
}