namespace extractor_c.Models;

[System.Serializable]
public class GPT4Request
{
    public string model { get; set; }
    public OpenAICommand[] messages { get; set; }
    public int max_completion_tokens { get; set; }

    public GPT4Request() {
      model = "gpt-4-turbo-2024-04-09";
      max_completion_tokens = 1000;
    }
}