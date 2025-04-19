namespace extractor_c.Controllers;

using Microsoft.AspNetCore.Mvc;
using extractor_c.Services;
using extractor_c.Prompts;
using extractor_c.Models;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly FileHandlerService FileHandlerService;
    private readonly ILogger<UploadController> _logger;

    public UploadController(FileHandlerService fileHandlerService, ILogger<UploadController> logger)
    {
        this.FileHandlerService = fileHandlerService;
        this._logger = logger;
    }

    [HttpPost]
    [RequestSizeLimit(10 * 1024 * 1024)]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        try {
            using var stream = file.OpenReadStream();
            OpenAIResponse result = await FileHandlerService.Handle(stream);
            
            return Ok(result);
        } catch (Exception ex) {
            _logger.LogError(ex.Message);

            return StatusCode(500, ex);
        }
    }
}