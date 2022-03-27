using GigaHurtz.Common.Models;
using GigaHurtzApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GigaHurtzApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CompatibilityController : ControllerBase
{
    private readonly IDbService _dbService;
    private readonly CompatibilityService _compat;

    public CompatibilityController(IDbService dbService, CompatibilityService compatibilityService)
    {
        _dbService = dbService;
        _compat = compatibilityService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Compatibility>>> GetCompatibility(string id)
    {
        var compats = await _compat.GetCompatForUser(id);
        return Ok(compats);
    }
}
