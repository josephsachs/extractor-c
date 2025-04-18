using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Text.Json;


namespace extractor_c.Controllers;

public class ExtractFieldsPrompt {
    public OpenAIService api;
    private StringBuilder stringBuilder;
    public DialogGenerator(OpenAIService api) {
        this.api = api;
        stringBuilder = new StringBuilder();
    }

    public async Task<string> Generate(string context) {
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


        foreach (OpenAICommand command in request.messages) {
            Debug.Log(command.content);
        }

        stringBuilder.Clear();

        string res = await api.PostRequest(request);
        
        return res;
    }
}
