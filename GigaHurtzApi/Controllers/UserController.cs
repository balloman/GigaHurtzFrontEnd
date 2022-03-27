using GigaHurtzApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GigaHurtzApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IDbService _dbService;

    public UserController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<int>> GetUserRole(string id)
    {
        var user = await _dbService.GetRole(id);
        if (user is null) return NotFound();
        return Ok(user);
    }
}