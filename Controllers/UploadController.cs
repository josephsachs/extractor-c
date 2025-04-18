using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using extractor_c.Services;

namespace extractor_c.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly OpenAIService api;

    public UploadController(OpenAIService api)
    {
        this.api = api;
    }

    [HttpPost]
    [RequestSizeLimit(10 * 1024 * 1024)]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        var text = ExtractTextFromPDF(stream); // Replace with real logic

        var result = await api.SendChatCompletionRequestAsync(text);
        return Ok(JsonSerializer.Deserialize<JsonElement>(result));
    }

    private string ExtractTextFromPDF(Stream stream)
    {
        return "Simulated extracted PDF content for now.";
    }
}