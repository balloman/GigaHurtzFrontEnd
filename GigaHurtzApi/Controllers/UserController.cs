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
    public async Task<ActionResult<string>> GetUserRole(string id)
    {
        var user = await _dbService.GetUserRole(id);
    }
}