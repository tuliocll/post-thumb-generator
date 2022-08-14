using Microsoft.AspNetCore.Mvc;

namespace DotNetCover.Controllers;

[ApiController]
[Route("[controller]")]
public class GenerateCoverController : ControllerBase
{

    private readonly ILogger<GenerateCoverController> _logger;

    public GenerateCoverController(ILogger<GenerateCoverController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetGenerateTemplate")]
    public FileContentResult Get(string title, string? author, string? date, [FromQuery] List<string> tags)
    {

        GenerateImage imageGenerator = new GenerateImage(title, author, date, tags);
        var (content, contentType) = imageGenerator.Create();

        return File(content, contentType);
    }
}
