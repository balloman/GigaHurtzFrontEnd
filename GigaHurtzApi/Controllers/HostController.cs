using GigaHurtzApi.Services;
using Microsoft.AspNetCore.Mvc;
using Host = GigaHurtz.Common.Models.Host;

namespace GigaHurtzApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HostController : ControllerBase
{
    private readonly IDbService _dbService;

    public HostController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Host>> GetHostById(string id)
    {
        var host = await _dbService.GetHost(id);
        if (host is null) return NotFound();
        return Ok(host);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHost(Host host)
    {
        try
        {
            await _dbService.AddHost(host);
            return Ok();
        } catch (IDbService.DbException e)
        {
            return NotFound(e.Message);
        }
    }
}
