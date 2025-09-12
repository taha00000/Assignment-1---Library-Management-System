using System.Collections.Generic;
using LibraryManagementSystem.Domain;
using Xunit;

namespace LibraryManagementSystem.Tests.Domain;

public class AuthorTest
{
    [Fact]
    public void BooksToString_ShouldReturnEmptyString_WhenNoBooks()
    {
        var author = new Author();
        var result = author.BooksToString();
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void BooksToString_ShouldReturnBookTitle_WhenOneBook()
    {
        var author = new Author
        {
            Books = new List<Book>
            {
                new Book { Title = "Book 1" }
            }
        };
        var result = author.BooksToString();
        Assert.Equal("Book 1", result);
    }

    [Fact]
    public void BooksToString_ShouldReturnBookTitlesSeparatedByCommaAnd_WhenMultipleBooks()
    {
        var author = new Author
        {
            Books = new List<Book>
            {
                new Book { Title = "Book 1" },
                new Book { Title = "Book 2" },
                new Book { Title = "Book 3" }
            }
        };
        var result = author.BooksToString();
        Assert.Equal("Book 1, Book 2 and Book 3", result);
    }
}