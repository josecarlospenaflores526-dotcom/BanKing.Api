using Banking.Application.DTOs;
using Banking.Application.Exceptions;
using Banking.Application.Interfaces;
using Banking.Application.Models;
using Banking.Application.Services;
using Banking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Services;

public class BankAccountService : IBankAccountService
{
    private readonly BankingDbContext _context;
    private readonly AccountNumberGenerator _generator;
    public BankAccountService(
        BankingDbContext context,
        AccountNumberGenerator generator)
    {
        _context = context;
        _generator = generator;
    }
    public async Task<AccountDto> CreateAsync(CreateAccountDto request)
    {
        if (request.InitialBalance < 0)
            throw new BusinessException(
                "El saldo inicial no puede ser negativo");

        bool customerExists = await _context.Customers
            .AnyAsync(c => c.Id == request.CustomerId);

        if (!customerExists)
            throw new BusinessException(
                "Cliente no encontrado", 404);

        string number = "";
        bool available = false;

        for (int attempt = 0; attempt < 10000; attempt++)
        {
            number = _generator.Generate();

            available = !await _context.BankAccounts
                .AnyAsync(a => a.AccountNumber == number);

            if (available)
                break;
        }

        if (!available)
            throw new BusinessException(
                "No hay números de cuenta disponibles", 409);

        var account = new BankAccount
        {
            CustomerId = request.CustomerId,
            AccountNumber = number,
            Balance = request.InitialBalance,
            CreatedAt = DateTime.UtcNow
        };

        _context.BankAccounts.Add(account);
        await _context.SaveChangesAsync();

        return new AccountDto(
            account.AccountNumber,
            account.CustomerId,
            account.Balance,
            account.CreatedAt);
    }
    private async Task<BankAccount> FindAccountAsync(
        string accountNumber)
    {
        var account = await _context.BankAccounts
            .FirstOrDefaultAsync(
                a => a.AccountNumber == accountNumber);

        if (account == null)
            throw new BusinessException(
                "Cuenta no encontrada", 404);

        return account;
    }
    public async Task<BalanceDto> GetBalanceAsync(
        string accountNumber)
    {
        var account = await FindAccountAsync(accountNumber);

        return new BalanceDto(
            account.AccountNumber,
            account.Balance);
    }
    public async Task<BalanceDto> DepositAsync(
        string accountNumber,
        decimal amount)
    {
        if (amount <= 0)
            throw new BusinessException(
                "El depósito debe ser mayor que cero");

        var account = await FindAccountAsync(accountNumber);

        account.Balance = BankingRules.Deposit(account.Balance, amount);

        _context.BankTransactions.Add(new BankTransaction
        {
            BankAccountId = account.Id,
            Type = "Deposit",
            Amount = amount,
            Timestamp = DateTime.UtcNow,
            ResultingBalance = account.Balance
        });

        await _context.SaveChangesAsync();

        return new BalanceDto(
            account.AccountNumber,
            account.Balance);
    }
    public async Task<BalanceDto> WithdrawAsync(
        string accountNumber,
        decimal amount)
    {
        if (amount <= 0)
            throw new BusinessException(
                "El retiro debe ser mayor que cero");

        var account = await FindAccountAsync(accountNumber);

        if (account.Balance < amount)
            throw new BusinessException(
                "Fondos insuficientes");

        account.Balance = BankingRules.Withdraw(account.Balance, amount);

        _context.BankTransactions.Add(new BankTransaction
        {
            BankAccountId = account.Id,
            Type = "Withdrawal",
            Amount = amount,
            Timestamp = DateTime.UtcNow,
            ResultingBalance = account.Balance
        });

        await _context.SaveChangesAsync();

        return new BalanceDto(
            account.AccountNumber,
            account.Balance);
    }
    public async Task<List<TransactionDto>> GetTransactionsAsync(
        string accountNumber)
    {
        var account = await FindAccountAsync(accountNumber);

        return await _context.BankTransactions
            .Where(t => t.BankAccountId == account.Id)
            .OrderBy(t => t.Timestamp)
            .ThenBy(t => t.Id)
            .Select(t => new TransactionDto(
                t.Id,
                t.Type,
                t.Amount,
                t.Timestamp,
                t.ResultingBalance))
            .ToListAsync();
    }
}
