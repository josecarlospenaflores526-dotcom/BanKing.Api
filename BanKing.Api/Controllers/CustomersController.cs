
using Banking.Application.Interfaces;
using Banking.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanKing.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // Obtener todos los clientes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    // Buscar un cliente por su ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);

        if (customer == null)
        {
            return NotFound("Cliente no encontrado");
        }

        return Ok(customer);
    }

    // Registrar un nuevo cliente
    [HttpPost]
    public async Task<IActionResult> Create(Customer customer)
    {
        var newCustomer = await _customerService.CreateCustomerAsync(customer);

        return CreatedAtAction(
            nameof(GetById),
            new { id = newCustomer.Id },
            newCustomer
        );
    }
}
