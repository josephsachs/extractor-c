namespace extractor_c.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using extractor_c.Services;
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
            OpenAIResponse result = await FileHandlerService.Handle(file);
            
            return Ok(result);
        } catch (JsonException ex) {
            _logger.LogWarning("OpenAI returned non-JSON content: {Content}", ex.Message);
            
            return StatusCode(500, "The AI model did not return valid JSON data");
        } catch (Exception ex) {
            _logger.LogError(ex.Message);

            return StatusCode(500, "An error occurred");
        }
    }
}