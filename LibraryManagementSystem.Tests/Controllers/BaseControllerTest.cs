using System;
using System.IO;
using LibraryManagementSystem.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Tests.Controllers;

public abstract class BaseControllerTest : IDisposable
{
    protected readonly LibraryContext Context;

    private readonly SqliteConnection _connection;

    protected BaseControllerTest()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite(_connection)
            .Options;

        Context = new LibraryContext(options);
        Context.Database.Migrate();
    }

    public void Dispose()
    {
        Context.Dispose();
        _connection.Close();
        _connection.Dispose();
    }
}