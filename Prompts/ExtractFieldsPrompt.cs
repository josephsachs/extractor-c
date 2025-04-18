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
                messages = new OpenAICommand[] { 
                    new OpenAICommand
                    {
                        role = "developer",
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
