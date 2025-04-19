namespace extractor_c.Services;

using Prompts;
using extractor_c.Models;
using System.Text.Json;

public class FileHandlerService {

  private readonly OpenAIService Client;
  private readonly PdfService PdfService;

  private readonly ILogger<FileHandlerService> _logger;

  public FileHandlerService(OpenAIService client, PdfService pdfService, ILogger<FileHandlerService> logger) {
    this.Client = client;
    this.PdfService = pdfService;    
    this._logger = logger;
  }

  public async Task<OpenAIResponse> Handle(IFormFile file) {
    try {
      using var stream = file.OpenReadStream();
      var document = await PdfService.ExtractTextFromPDF(stream);
            
      var request = new ExtractFieldsPrompt().Get(document);
              
      var result = await Client.MakeRequest(request);
      var messageContent = result.choices[0].message.content;

      request = new VerifyFieldsPrompt().Get(messageContent, document);

      result = await Client.MakeRequest(request);
      
      _logger.LogInformation(result.ToString());

      messageContent = result.choices[0].message.content;
      Resume resume = JsonSerializer.Deserialize<Resume>(messageContent);

      return result;

    } catch (Exception ex) {
      _logger.LogError(ex.Message);

      return new OpenAIResponse();
    }
    
  }
}