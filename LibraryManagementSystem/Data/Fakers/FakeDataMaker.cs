namespace LibraryManagementSystem.Data.Fakers;

public class FakeDataMaker(LibraryContext context)
{
    public void MakeFakeData()
    {
        if (IsDataSeeded())
        {
            return;
        }
        var bookFaker = new BookFaker();
        var memberFaker = new MemberFaker();

        var books = bookFaker.Generate(10);
        var members = memberFaker.Generate(10);

        context.Books.AddRange(books);
        context.Members.AddRange(members);

        context.SaveChanges();
    }

    private bool IsDataSeeded()
    {
        return context.Books.Any() || context.Members.Any();
    }
}