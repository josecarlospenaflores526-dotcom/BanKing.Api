
using Banking.Application.Exceptions;

namespace Banking.Application.Services;

public static class BankingRules
{
    public static decimal Deposit(decimal balance, decimal amount)
    {
        if (amount <= 0)
            throw new BusinessException(
                "El depósito debe ser mayor que cero");

        return balance + amount;
    }

    public static decimal Withdraw(decimal balance, decimal amount)
    {
        if (amount <= 0)
            throw new BusinessException(
                "El retiro debe ser mayor que cero");

        if (balance < amount)
            throw new BusinessException("Fondos insuficientes");

        return balance - amount;
    }
}
