namespace extractor_c.Controllers;

using System.Threading.Tasks;
using System.Text;
using Services;


public class ExtractFieldsPrompt {
    private StringBuilder stringBuilder;

    public ExtractFieldsPrompt() {
        stringBuilder = new StringBuilder();
    }

    public OpenAIRequest Get(string context) {
        OpenAIRequest request = new OpenAIRequest
            {
                temperature = 0.7,
                frequency_penalty = 1.5,
                logit_bias = {
                    { "Character1", -100 },
                    { "Character2", -100 }
                },
                messages = new OpenAICommand[] { 
                    new OpenAICommand
                    {
                        role = "system",
                        content = "You are extracting data from text and structuring the extracted data as JSON."
                    },
                    new OpenAICommand
                    {
                        role = "user",
                        content = $@"Document text here" // Document text here
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
