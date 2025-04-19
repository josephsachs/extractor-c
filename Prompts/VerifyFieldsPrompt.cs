namespace extractor_c.Prompts;

using System.Text;
using Models;

public class VerifyFieldsPrompt {

    private string instructions = @"You are verifying your analysis of a document. 
    Please confirm that the information in the Json object's values accurately reflect the information in the document.
    Return the Json object, with any required corrections to the values. Return only the Json object in plaintext (no formatting, no embed).
    Notes:
    - Phone numbers should be unpunctuated
    - Skills may be scattered in the document, collect them in the array. Skills should be short strings. 
    'Databases like MySQL and Mongo' should result in two skills: 'MySQL' and 'Mongo'. Skills can be 
    abstract categories like 'DevOps' or can be names of specific products, but cannot be concepts like 
    'Raising Productivity', 'Diligently Problem-Solving', etc.. 
    - Company names should be names only, no additional information or parentheticals
    - Unknown or missing values should be null
    ";

    public GPT4Request Get(string context, string documentText) {
        GPT4Request request = new GPT4Request
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

        return request;
    }
}