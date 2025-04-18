using UglyToad.PdfPig;

namespace extractor_c.Services;

public class PdfService {
  public string ExtractTextFromPDF(Stream pdfStream) {
    using var doc = PdfDocument.Open(pdfStream);
    var text = string.Join("\n", doc.GetPages().Select(p => p.Text));
 
    return text;
  }
}