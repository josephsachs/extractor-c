namespace extractor_c.Models;


[System.Serializable]
public class OpenAIResponse
{
    public Choice[] choices { get; set; }
    //public OpenAIUsageData[] usage { get; set; }
}