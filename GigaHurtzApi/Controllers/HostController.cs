using GigaHurtz.Common.Models;
using GigaHurtzApi.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<HostModel>> GetHostById(string id)
    {
        var host = await _dbService.GetHost(id);
        if (host is null) return NotFound();
        return Ok(host);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHost(HostModel host)
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HostModel>>> GetAllHosts()
    {
        return Ok(await _dbService.GetAllHosts());
    }

    [HttpPost("{hostId}/{requestId}")]
    public async Task<IActionResult> MakeRequest(string hostId, string requestId)
    {
        var host = await _dbService.GetHost(hostId);
        if (host is null) return NotFound();
        var requests = host.Requests.ToList();
        requests.Add(requestId);
        var newHost = new HostModel(host.Id,
            host.Name,
            host.AvailableRooms,
            host.MaxTenants,
            host.Address,
            host.Languages,
            host.Kids,
            host.Cooks,
            host.GenderPref,
            host.Phone,
            host.Email,
            host.ImageUrl,
            requests);
        await _dbService.AddHost(newHost);
        return Ok();
    }
}
