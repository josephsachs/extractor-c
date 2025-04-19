namespace extractor_c.Prompts;

using System.Text;
using Models;


public class ExtractFieldsPrompt {
    private string instructions = @"You are extracting data from a document. Please analyze the text and return a JSON (plaintext, do not format) with the format: 
    {
        name: string?,
        address: string?,
        phone_numbers: [string]?,
        email: string?,
        websites: [
            {
                name: string?,
                url: string?
            }
            // ... and so on
        ],
        would_relocate: boolean?,
        experience: [
            {
                title: string?,
                company_name: string,
                start_date: string?,
                end_date: string?,
                responsibilities: [string]?,
                office_type: string?, // should be 'Onsite', 'Remote', 'Hybrid' or null
                is_manager: boolean?,
                industry_classification: string? // null if a good category cannot be inferred
            },
            // ... and so on
        ],
        skills: [string]?, // these will be scattered around
        education: [
            {
                degree: string?,
                certification: string?,
                date: string?,
                institution: string?,
                gpa: string?,
                expiration_date: string?
            },
            // ... and so on
        ],
        portfolio: [
            {
                project_name: string?,
                date: string?,
                url: string?,
                tools_used: [string]?,
                is_professional: boolean?
            }
        ]
    }
    ";

    public GPT4Request Get(string context) {
        GPT4Request request = new GPT4Request
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

        return request;
    }
}
