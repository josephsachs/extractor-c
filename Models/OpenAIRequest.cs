[System.Serializable]
public class OpenAIRequest
{
    public string model { get; set; }
    public OpenAICommand[] messages { get; set; }
    public int max_completion_tokens { get; set; }

    public OpenAIRequest() {
      model = "gpt-4-turbo-2024-04-09";
      max_completion_tokens = 1000;
    }
}