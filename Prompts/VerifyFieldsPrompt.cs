namespace extractor_c.Prompts;

using Models;

public class VerifyFieldsPrompt {

    private string instructions = @"You are verifying your analysis of a document. 
    Please confirm that the information in the Json object's values accurately reflect the information in the document.
    Return the Json object, with any required corrections to the values. Return only the Json object in plaintext (no formatting, no embed).
    Notes:
    - Phone numbers should be unpunctuated.
    - Responsibilities per job should be summarized with the aim to reduce verbiage. 
        - Remove all marketing language and self-promotion. 
        - Remove impact language like 'improved reliability'; focus on duties. 
        - Remove most adjectives. 
        - Max 15 words.
        - Responsibilities should not end with periods.
    - Skills may be scattered in the document, collect them in the array. 
        - Skills should be short strings. 
        - 'Databases like MySQL and Mongo' should result in two skills: 'MySQL' and 'Mongo'. 
        - A string like 'Key Account Management & Marketing' should result in two skills: 
        'Key Account Management' and 'Marketing'. 
        - 'C# (.NET)' should result in two skills: 'C#' and '.NET'. 
        - Skills can be abstract categories like 'Internet Marketing', 'DevOps' or names of specific products, but cannot be concepts like 
    'Raising Productivity', 'Diligently Problem-Solving', etc.. 
        - Likewise they cannot be vague generalities like 'Revenue Increasing' and 'Sales Growth'. 
        - They should not be accomplishments like 'Consistently meeting quotas' or 'Driving 30% more traffic YoY'.
        - Skills should be corroborated by something in responsibilities, education or portfolio.
    - Company names should be names only, no additional information or parentheticals.
    - Unknown or missing values should be included in the response, but null.
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