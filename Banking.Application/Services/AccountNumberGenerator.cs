using System.Security.Cryptography;
namespace Banking.Application.Services;
public class AccountNumberGenerator
{
    public string Generate()
    {
        int number = RandomNumberGenerator.GetInt32(0, 10000);

        return $"ACC-{DateTime.Today:yyyyMMdd}-{number:D4}";
    }
}
