using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

using BankCoreApi.Data;
using BankCoreApi.Models;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;
    public UsersController(AppDbContext db) => _db = db;

    [HttpGet]
    public IActionResult GetAll() => Ok(_db.Users.Where(u => !EF.Property<bool>(u, "IsDeleted")).ToList());

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var u = _db.Users.Find(id);
        if (u == null) return NotFound();
        return Ok(u);
    }

    [HttpPost]
    public IActionResult Create([FromBody] User model)
    {
        _db.Users.Add(model);
        _db.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] User model)
    {
        var u = _db.Users.Find(id);
        if (u == null) return NotFound();
        u.FirstName = model.FirstName;
        u.LastName = model.LastName;
        u.Email = model.Email;
        _db.SaveChanges();
        return NoContent();
    }

    // Limited delete: soft delete and check role (simplified)
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var u = _db.Users.Find(id);
        if (u == null) return NotFound();
        // TODO: check current caller role/permissions; for now soft-delete
        _db.Remove(u); // or set u.IsDeleted = true
        _db.SaveChanges();
        return NoContent();
    }
}
