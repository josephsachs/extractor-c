namespace extractor_c.Services;

using Prompts;

public class FileHandlerService {

  private readonly OpenAIService Client;
  private readonly PdfService PdfService;

  private readonly ILogger<FileHandlerService> _logger;

  public FileHandlerService(OpenAIService client, PdfService pdfService, ILogger<FileHandlerService> logger) {
    this.Client = client;
    this.PdfService = pdfService;    
    this._logger = logger;
  }

  public async Task<string> Handle(IFormFile file) {
    try {
      using var stream = file.OpenReadStream();
      var document = await PdfService.ExtractTextFromPDF(stream);
            
      var request = new ExtractFieldsPrompt().Get(document);
              
      var result = await Client.MakeRequest(request);
      var messageContent = result.choices[0].message.content;

      request = new VerifyFieldsPrompt().Get(messageContent, document);

      result = await Client.MakeRequest(request);
      messageContent = result.choices[0].message.content;

      return messageContent;

    } catch (Exception ex) {
      _logger.LogError($"Exception in FileHandlerService: {ex.Message}, {ex.StackTrace}");

      throw;
    }
  }
}