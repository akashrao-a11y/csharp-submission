using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankCoreApi.Dtos;
using BankCoreApi.Models.Dto;
using BankCoreApi.Models;
using Microsoft.AspNetCore.Mvc;


using BankCoreApi.Data;    // ✅ Required



[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly AppDbContext _db;
    public AccountsController(AppDbContext db) => _db = db;

    [HttpGet]
    public IActionResult GetAll() => Ok(_db.Accounts.Where(a => !a.IsDeleted).ToList());

    [HttpGet("{id}")]
    public IActionResult Get(Guid id) => Ok(_db.Accounts.Find(id));

    [HttpPost]
    public IActionResult Create([FromBody] CreateAccountDto dto)
    {
        Account account = dto.Type switch
        {
            "Savings" => new SavingsAccount { AccountNumber = dto.AccountNumber, BranchId = dto.BranchId, CurrencyId = dto.CurrencyId, Balance = dto.InitialDeposit, MinimumBalance = dto.MinimumBalance },
            "Current" => new CurrentAccount { AccountNumber = dto.AccountNumber, BranchId = dto.BranchId, CurrencyId = dto.CurrencyId, Balance = dto.InitialDeposit, OverdraftLimit = dto.OverdraftLimit },
            _ => throw new Exception("Unknown type")
        };
        _db.Accounts.Add(account);
        _db.SaveChanges();
        // add owner
        _db.AccountOwners.Add(new AccountOwner { AccountId = account.Id, UserId = dto.PrimaryOwnerId, OwnerType = OwnerType.Primary });
        _db.Transactions.Add(new Transaction { AccountId = account.Id, Amount = dto.InitialDeposit, Type = "InitialDeposit", Notes = "Seed" });
        _db.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = account.Id }, account);
    }

    [HttpPost("{id}/deposit")]
    public IActionResult Deposit(Guid id, [FromBody] OperationDto op)
    {
        var acc = _db.Accounts.Find(id);
        if (acc == null || acc.IsDeleted) return NotFound();
        // check deposit rules (e.g., limits)
        acc.Balance += op.Amount;
        _db.Transactions.Add(new Transaction { AccountId = id, Amount = op.Amount, Type = "Deposit", Notes = op.Notes });
        _db.SaveChanges();
        return Ok(new { acc.Balance });
    }

    [HttpPost("{id}/withdraw")]
    public IActionResult Withdraw(Guid id, [FromBody] OperationDto op)
    {
        var acc = _db.Accounts.OfType<Account>().SingleOrDefault(a => a.Id == id);
        if (acc == null) return NotFound();
        // check ownership/permissions here (omitted)
        // withdrawal limit example:
        decimal dailyLimit = 50000m; // sample
        // implement per-user/per-day limits via Transaction queries

        if (acc is CurrentAccount ca)
        {
            if (acc.Balance + ca.OverdraftLimit < op.Amount) return BadRequest("Insufficient funds/overdraft");
        }
        else
        {
            if (acc.Balance < op.Amount) return BadRequest("Insufficient funds");
            // ensure minimum balance not violated for savings
            if (acc is SavingsAccount sa)
            {
                if (acc.Balance - op.Amount < sa.MinimumBalance) return BadRequest("Min balance violation");
            }
        }

        acc.Balance -= op.Amount;
        _db.Transactions.Add(new Transaction { AccountId = id, Amount = op.Amount, Type = "Withdraw", Notes = op.Notes });
        _db.SaveChanges();
        return Ok(new { acc.Balance });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var acc = _db.Accounts.Find(id);
        if (acc == null) return NotFound();
        // limited deletion: only admins or when balance is zero
        if (acc.Balance != 0) return BadRequest("Account must have zero balance to close.");
        acc.IsDeleted = true;
        _db.SaveChanges();
        return NoContent();
    }
}
