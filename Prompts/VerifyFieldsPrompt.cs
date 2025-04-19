namespace extractor_c.Prompts;

using System.Text;
using Models;

public class VerifyFieldsPrompt {
    private StringBuilder stringBuilder;

    private string instructions = @"You are verifying your analysis of a property legal document. 
    Please confirm that the information in the Json object's values accurately reflect the information in the document document.
    Return the Json object, with any required corrections to the values. Return only the Json object in plaintext (no formatting, no embed).
    Notes:
    - when multiple lots occur, list the identifiers with commas
    - book refers to the place of the recording
    - maximum_allowable_rent is a boolean that should be true if a maximum allowable rent is defined in the document.
    - transferrable is a boolean that should be true if there exists any case in which transfer is allowed.
    - annual_report is a boolean that should be true if the developer is required to make an annual report.
    ";

    public VerifyFieldsPrompt() {
        stringBuilder = new StringBuilder();
    }

    public OpenAIRequest Get(string context, string documentText) {
        OpenAIRequest request = new OpenAIRequest
            {
                messages = new OpenAICommand[] { 
                    new OpenAICommand
                    {
                        role = "developer",
                        content = "You are verifying a Json against that of the original document and returning the same Json."
                    },
                    new OpenAICommand
                    {
                        role = "user",
                        content = instructions
                    },
                    new OpenAICommand
                    {
                        role = "user",
                        content = $"- Json object: {context}"
                    },
                    new OpenAICommand
                    {
                        role = "user",
                        content = $"- Original document: {documentText}"
                    }
                }
            };

        stringBuilder.Clear();

        return request;
    }
}