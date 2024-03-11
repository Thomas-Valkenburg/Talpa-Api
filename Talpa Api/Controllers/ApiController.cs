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
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);

        return user == null ? NotFound() : user;
    }
}