using extractor_c.Models;

[System.Serializable]
public class Choice
{
    public int index { get; set; }
    public OpenAICommand message { get; set; }
}
