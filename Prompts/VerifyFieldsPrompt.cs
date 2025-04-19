namespace extractor_c.Prompts;

using System.Text;
using Models;

public class VerifyFieldsPrompt {
    private StringBuilder stringBuilder;

    private string instructions = @"You are verifying your analysis of a document. 
    Please confirm that the information in the Json object's values accurately reflect the information in the document.
    Return the Json object, with any required corrections to the values. Return only the Json object in plaintext (no formatting, no embed).
    Notes:
    - Skills may be scattered in the document, collect them in the array
    - Company names should be names only, no additional information or parentheticals
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