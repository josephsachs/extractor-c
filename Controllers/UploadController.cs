namespace extractor_c.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using extractor_c.Services;
using extractor_c.Models;
using extractor_c.Prompts;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly OpenAIService client;
    private readonly PdfService pdfService;

    private readonly ILogger log = new LoggerFactory().CreateLogger<UploadController>();

    public UploadController(OpenAIService client, PdfService pdfService)
    {
        this.client = client;
        this.pdfService = pdfService;
    }

    [HttpPost]
    [RequestSizeLimit(10 * 1024 * 1024)]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        try {
            using var stream = file.OpenReadStream();

            var document = await pdfService.ExtractTextFromPDF(stream);
            var request = new ExtractFieldsPrompt().Get(document);
            
            var result = await client.makeRequest(request);

            var messageContent = result.choices[0].message.content;

            request = new VerifyFieldsPrompt().Get(messageContent, document);
            result = await client.makeRequest(request);
            
            log.LogInformation(result.ToString());
            
            return Ok(result);
        } catch (Exception ex) {
            log.LogError(ex.Message);

            return StatusCode(500, ex);
        }
    }
}