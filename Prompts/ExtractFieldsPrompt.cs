namespace extractor_c.Prompts;

using System.Text;
using Models;


public class ExtractFieldsPrompt {
    private StringBuilder stringBuilder;

    private string instructions = @"You are extracting data from a property legal document. Please analyze the text and return a JSON (plaintext, do not format) with the format: 
    {
        covenant: string, 
        covenant_effective_date: string,
        developer: string,
        maximum_allowable_rent: boolean,
        transferrable: boolean,
        annual_report: boolean,
        legal_description: {
            lot: string,
            plat: string,
            subdivision: string,
            book: string,
            boundary: [
                {
                    description: string
                },
                {
                    description: string
                }
                // ... and so on
            ]
        }
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
