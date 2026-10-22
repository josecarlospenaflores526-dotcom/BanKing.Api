using Banking.Application.DTOs;
namespace Banking.Application.Interfaces;
public interface IBankAccountService
{
    Task<AccountDto> CreateAsync(CreateAccountDto request);
    Task<BalanceDto> GetBalanceAsync(string accountNumber);
    Task<BalanceDto> DepositAsync(string accountNumber, decimal amount);
    Task<BalanceDto> WithdrawAsync(string accountNumber, decimal amount);
    Task<List<TransactionDto>> GetTransactionsAsync(string accountNumber);
}
