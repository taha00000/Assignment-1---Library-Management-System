using Bogus;
using LibraryManagementSystem.Domain;

namespace LibraryManagementSystem.Data.Fakers;

public sealed class MemberFaker : Faker<Member>
{
    public MemberFaker()
    {
        RuleFor(m => m.Name, f => f.Person.FullName);
    }
}