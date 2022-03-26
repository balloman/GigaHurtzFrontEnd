using GigaHurtz.Common.Models;
using GigaHurtzApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GigaHurtzApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RefugeeController : ControllerBase
{
    private readonly IDbService _dbService;

    public RefugeeController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Refugee>> GetRefugeeById(string id)
    {
        var refugee = await _dbService.GetRefugee(id);
        if (refugee is null) return NotFound();
        return Ok(refugee);
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateRefugee(Refugee refugee)
    {
        try
        {
            await _dbService.AddRefugee(refugee);
            return Ok();
        } catch (IDbService.DbException e)
        {
            return BadRequest(e.Message);
        }
    }
}
