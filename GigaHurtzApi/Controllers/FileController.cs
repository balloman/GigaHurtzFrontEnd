using System.Net.Mime;
using GigaHurtz.Common.Extensions;
using GigaHurtzApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GigaHurtzApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IDbService _dbService;

    public FileController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost("{id}")]
    public async Task<ActionResult<string>> Upload(IFormFile file, string id)
    {
        var contentType = new ContentType(file.ContentType);
        var result = await _dbService.UploadFile(
            $"images/{id}/house{contentType.GetFileExtension()}",
            file.OpenReadStream(),
            contentType);
        return Ok(result);
    }
}
