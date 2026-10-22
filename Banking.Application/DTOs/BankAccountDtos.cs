
namespace Banking.Application.DTOs;

public record CreateAccountDto(
    int CustomerId,
    decimal InitialBalance
);
public record AccountDto(
    string AccountNumber,
    int CustomerId,
    decimal Balance,
    DateTime CreatedAt
);
public record BalanceDto(
    string AccountNumber,
    decimal Balance
);
public record MoneyDto(decimal Amount);
public record TransactionDto(
    Guid Id,
    string Type,
    decimal Amount,
    DateTime Timestamp,
    decimal ResultingBalance
);
