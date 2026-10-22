
using Banking.Application.DTOs;
using Banking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BanKing.Api.Controllers;

[ApiController]
[Route("api/accounts")]
public class BankAccountsController : ControllerBase
{
    private readonly IBankAccountService _service;

    public BankAccountsController(IBankAccountService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAccountDto request)
    {
        var account = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetBalance),
            new { number = account.AccountNumber },
            account);
    }

    [HttpGet("{number}/balance")]
    public async Task<IActionResult> GetBalance(string number)
    {
        return Ok(await _service.GetBalanceAsync(number));
    }

    [HttpPost("{number}/deposits")]
    public async Task<IActionResult> Deposit(
        string number,
        MoneyDto request)
    {
        return Ok(await _service.DepositAsync(
            number, request.Amount));
    }

    [HttpPost("{number}/withdrawals")]
    public async Task<IActionResult> Withdraw(
        string number,
        MoneyDto request)
    {
        return Ok(await _service.WithdrawAsync(
            number, request.Amount));
    }

    [HttpGet("{number}/transactions")]
    public async Task<IActionResult> GetTransactions(string number)
    {
        return Ok(await _service.GetTransactionsAsync(number));
    }
}
