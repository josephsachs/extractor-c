namespace extractor_c.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using extractor_c.Services;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly OpenAIService client;
    private readonly PdfService pdfService;

    public UploadController(OpenAIService api, PdfService pdfService)
    {
        this.client = api;
        this.pdfService = pdfService;
    }

    [HttpPost]
    [RequestSizeLimit(10 * 1024 * 1024)]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        using var stream = file.OpenReadStream();

        var text = await pdfService.ExtractTextFromPDF(stream);
        var request = new ExtractFieldsPrompt().Get(text);

        foreach (OpenAICommand command in request.messages) {
            Console.WriteLine(command.content);
        }

        var result = await client.makeRequest(request);
        
        Console.Write(result);

        return Ok(JsonSerializer.Deserialize<JsonElement>(result));
    }
}