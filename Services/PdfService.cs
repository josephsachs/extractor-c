using UglyToad.PdfPig;

namespace extractor_c.Services;

public class PdfService {
  private readonly ILogger<PdfService> _logger;

  public PdfService(ILogger<PdfService> logger) {
    this._logger = logger;
  }

  public async Task<string> ExtractTextFromPDF(Stream pdfStream) {
      try {
          using var doc = PdfDocument.Open(pdfStream);
          return string.Join("\n", doc.GetPages().Select(p => p.Text));
      }
      catch (Exception ex) {
          _logger.LogError(ex, "PDF extraction failed");
          throw new ApplicationException("Could not process PDF", ex);
      }
  }
}