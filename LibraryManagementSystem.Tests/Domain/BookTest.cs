using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using LibraryManagementSystem.Domain;
using Xunit;

namespace LibraryManagementSystem.Tests.Domain;

[TestSubject(typeof(Book))]
public class BookTest
{

    [Fact]
    public void IsAvailable_ShouldReturnTrue_WhenNoLoans()
    {
        var book = new Book();
        var result = book.IsAvailable();
        Assert.True(result);
    }

    [Fact]
    public void IsAvailable_ShouldReturnTrue_WhenAllLoansReturned()
    {
        var book = new Book
        {
            Loans = new List<Loan>
            {
                new Loan { ReturnDate = DateTime.Now.AddDays(-1) },
                new Loan { ReturnDate = DateTime.Now.AddDays(-2) }
            }
        };
        var result = book.IsAvailable();
        Assert.True(result);
    }

    [Fact]
    public void IsAvailable_ShouldReturnFalse_WhenAnyLoanNotReturned()
    {
        var book = new Book
        {
            Loans = new List<Loan>
            {
                new Loan { ReturnDate = DateTime.Now.AddDays(-1) },
                new Loan { ReturnDate = null }
            }
        };
        var result = book.IsAvailable();
        Assert.False(result);
    }

    [Fact]
    public void AuthorsToString_ShouldReturnEmptyString_WhenNoAuthors()
    {
        var book = new Book();
        var result = book.AuthorsToString();
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void AuthorsToString_ShouldReturnSingleAuthor_WhenOneAuthor()
    {
        var book = new Book
        {
            Authors = new List<Author>
            {
                new Author { Name = "Author1" }
            }
        };
        var result = book.AuthorsToString();
        Assert.Equal("Author1", result);
    }

    [Fact]
    public void AuthorsToString_ShouldReturnAuthorsSeparatedByAnd_WhenMultipleAuthors()
    {
        var book = new Book
        {
            Authors = new List<Author>
            {
                new Author { Name = "Author1" },
                new Author { Name = "Author2" },
                new Author { Name = "Author3" }
            }
        };
        var result = book.AuthorsToString();
        Assert.Equal("Author1, Author2 and Author3", result);
    }
}