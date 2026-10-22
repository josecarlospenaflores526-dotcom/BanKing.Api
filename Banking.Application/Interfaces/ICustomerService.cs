
using Banking.Application.Models;

namespace Banking.Application.Interfaces;

public interface ICustomerService
{
    Task<Customer> CreateCustomerAsync(Customer customer);
    Task<List<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
}
