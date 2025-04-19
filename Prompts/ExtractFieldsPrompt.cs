namespace extractor_c.Prompts;

using System.Text;
using Models;


public class ExtractFieldsPrompt {
    private StringBuilder stringBuilder;

    private string instructions = @"You are extracting data from a document. Please analyze the text and return a JSON (plaintext, do not format) with the format: 
    {
        name: string,
        address: string?,
        phone_numbers: [string]?,
        email: string?,
        experience: [
            {
                title: string?,
                company_name: string,
                start_date: string?,
                end_date: string?,
                roles: [string]
            },
            // ... and so on
        ],
        skills: [string]?, // these will be scattered around
        education: [
            {
                degree: string?,
                certification: string?,
                date: string,
                institution: string
            },
            // ... and so on
        ]
    }
    ";

    public ExtractFieldsPrompt() {
        stringBuilder = new StringBuilder();
    }

    public OpenAIRequest Get(string context) {
        OpenAIRequest request = new OpenAIRequest
            {
                messages = new OpenAICommand[] { 
                    new OpenAICommand
                    {
                        role = "developer",
                        content = "You are extracting data and structuring the extracted data as JSON"
                    },
                    new OpenAICommand
                    {
                        role = "user",
                        content = instructions
                    },
                    new OpenAICommand
                    {
                        role = "user",
                        content = context
                    }
                }
            };

        stringBuilder.Clear();

        return request;
    }
}
