
namespace Banking.Application.Models;

public class BankTransaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int BankAccountId { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal ResultingBalance { get; set; }
}
