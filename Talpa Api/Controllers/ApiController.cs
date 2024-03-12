using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talpa_Api.Contexts;
using Talpa_Api.Models;

namespace Talpa_Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class ApiController(Context context) : ControllerBase
{
    // GET: api/GetUser
    [HttpGet("{id:int}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = context.Users.Find(id);

        return user == null ? NotFound() : user;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser()
    {
        context.Users.Add(new User {Id = 0, IsManager = true, Name = "test", Team = new Team {Id = 0, Name = "test"}});
        await context.SaveChangesAsync();

        return NoContent();
    }
}