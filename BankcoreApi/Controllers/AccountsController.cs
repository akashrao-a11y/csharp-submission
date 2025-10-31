using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankCoreApi.Data;
using BankCoreApi.Models;

namespace BankCoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AccountsController(AppDbContext db) => _db = db;

        // GET: api/accounts
        [HttpGet]
        public IActionResult GetAll()
        {
            var accounts = _db.Accounts
                .Include(a => a.User)
                .Include(a => a.Branch)
                .Where(a => !a.IsClosed)
                .ToList();
            return Ok(accounts);
        }

        // GET: api/accounts/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var account = _db.Accounts
                .Include(a => a.User)
                .Include(a => a.Branch)
                .FirstOrDefault(a => a.Id == id);

            if (account == null)
                return NotFound("Account not found");

            return Ok(account);
        }

        // POST: api/accounts
        [HttpPost]
        public IActionResult Create([FromBody] Account account)
        {
            if (account == null)
                return BadRequest("Invalid account data");

            account.AccountNumber = account.AccountNumber ?? Guid.NewGuid().ToString("N")[..10];
            account.Balance = account.Balance < 0 ? 0 : account.Balance;
            account.IsClosed = false;

            _db.Accounts.Add(account);
            _db.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = account.Id }, account);
        }

        // POST: api/accounts/{id}/deposit
        [HttpPost("{id}/deposit")]
        public IActionResult Deposit(int id, [FromBody] Account operation)
        {
            var acc = _db.Accounts.Find(id);
            if (acc == null) return NotFound("Account not found");
            if (acc.IsClosed) return BadRequest("Account is closed");

            if (operation.Balance <= 0)
                return BadRequest("Deposit amount must be greater than zero");

            acc.Balance += operation.Balance;
            _db.SaveChanges();

            return Ok(new { Message = "Deposit successful", Balance = acc.Balance });
        }

        // POST: api/accounts/{id}/withdraw
        [HttpPost("{id}/withdraw")]
        public IActionResult Withdraw(int id, [FromBody] Account operation)
        {
            var acc = _db.Accounts.Find(id);
            if (acc == null) return NotFound("Account not found");
            if (acc.IsClosed) return BadRequest("Account is closed");

            if (operation.Balance <= 0)
                return BadRequest("Withdraw amount must be greater than zero");

            if (acc.AccountType == "Current" && acc.OverdraftLimit.HasValue)
            {
                if (acc.Balance + acc.OverdraftLimit < operation.Balance)
                    return BadRequest("Insufficient funds/overdraft limit exceeded");
            }
            else if (acc.Balance < operation.Balance)
            {
                return BadRequest("Insufficient funds");
            }

            acc.Balance -= operation.Balance;
            _db.SaveChanges();

            return Ok(new { Message = "Withdraw successful", Balance = acc.Balance });
        }

        // DELETE: api/accounts/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var acc = _db.Accounts.Find(id);
            if (acc == null) return NotFound("Account not found");

            if (acc.Balance != 0)
                return BadRequest("Account must have zero balance to close.");

            acc.IsClosed = true;
            _db.SaveChanges();

            return NoContent();
        }
    }
}
