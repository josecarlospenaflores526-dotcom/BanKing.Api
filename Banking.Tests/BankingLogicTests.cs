
using Banking.Application.Exceptions;
using Banking.Application.Services;
using Xunit;

namespace Banking.Tests;

public class BankingLogicTests
{
    [Fact]
    public void Deposit_IncreasesBalance()
    {
        decimal result = BankingRules.Deposit(1000, 500);
        Assert.Equal(1500, result);
    }

    [Fact]
    public void Withdraw_DecreasesBalance()
    {
        decimal result = BankingRules.Withdraw(1000, 300);
        Assert.Equal(700, result);
    }

    [Fact]
    public void Withdraw_InsufficientFunds_ThrowsException()
    {
        Assert.Throws<BusinessException>(() =>
            BankingRules.Withdraw(1000, 2000));
    }

    [Fact]
    public void AccountNumber_HasCorrectFormat()
    {
        var generator = new AccountNumberGenerator();
        string number = generator.Generate();

        Assert.Equal(17, number.Length);
        Assert.StartsWith("ACC-", number);
        Assert.Equal(
            DateTime.Today.ToString("yyyyMMdd"),
            number.Substring(4, 8));
        Assert.Equal('-', number[12]);
        Assert.Matches(@"^\d{4}$", number.Substring(13));
    }

    [Fact]
    public void AccountNumber_BasicUniqueness()
    {
        var generator = new AccountNumberGenerator();

        var numbers = Enumerable.Range(0, 20)
            .Select(_ => generator.Generate())
            .ToList();

        Assert.True(numbers.Distinct().Count() > 1);
    }
}
