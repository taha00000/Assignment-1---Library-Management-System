using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using LibraryManagementSystem.Domain;
using Xunit;

namespace LibraryManagementSystem.Tests.Domain;

[TestSubject(typeof(Member))]
public class MemberTest
{
    [Fact]
    public void CanBorrow_ShouldReturnTrue_WhenLoanCountIsLessThan3()
    {
        var member = new Member();
        member.Loans.Add(new Loan { ReturnDate = DateTime.Now });
        member.Loans.Add(new Loan { ReturnDate = DateTime.Now });

        var result = member.CanBorrow();

        Assert.True(result);
    }

    [Fact]
    public void CanBorrow_ShouldReturnFalse_WhenLoanCountIs3OrMore()
    {
        var member = new Member();
        member.Loans.Add(new Loan());
        member.Loans.Add(new Loan());
        member.Loans.Add(new Loan());

        var result = member.CanBorrow();

        Assert.False(result);
    }

    [Fact]
    public void RecordLoan_ShouldAddLoanToLoansCollection()
    {
        var member = new Member();
        var loan = new Loan();

        member.RecordLoan(loan);

        Assert.Contains(loan, member.Loans);
    }

    [Fact]
    public void RemoveLoan_ShouldUpdateLoanInLoansCollection()
    {
        var member = new Member();
        var loan = new Loan { Id = 1 };
        member.Loans = new List<Loan> { loan };
        member.ReturnLoan(loan);

        Assert.NotNull(member.Loans.First(l => l.Id == loan.Id).ReturnDate);
    }
}