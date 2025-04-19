namespace extractor_c.Models;


[System.Serializable]
public class OpenAIUsageData 
{
    public int promptTokens { get; set; }
    public int completionTokens { get; set; }
    public int totalTokens { get; set; }
}