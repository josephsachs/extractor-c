namespace extractor_c.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;


public class OpenAIService
{
    private string OrganizationID;
    private string APIKey;
    private StringBuilder stringBuilder;
    private static readonly HttpClient client = new HttpClient();


    public OpenAIService() {
        this.stringBuilder = new StringBuilder();
        this.getAPIConfig();
    }

    public async Task<string> PostRequest(OpenAIRequest openAIRequest)
    {
        string json = JsonSerializer.Serialize(openAIRequest);

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");

        httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);
        httpRequest.Headers.Add("OpenAI-Organization", OrganizationID);

      try {
            var response = await client.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        } catch (HttpRequestException e) {
            Console.WriteLine($"Request error: {e.Message}");
            throw;
        }
    }

    public void getAPIConfig() {
        string configDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Configs");
        string filePath = Path.Combine(configDirectory, "openai-api.json");

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            OpenAIConfigData configData = JsonSerializer.Deserialize<OpenAIConfigData>(dataAsJson);
            this.OrganizationID = configData.OrganizationID;
            this.APIKey = configData.APIKey;
        }
        else
        {
            Console.WriteLine("Cannot find config file");
        }
    }
}

public class OpenAIConfigData {
    public string OrganizationID;
    public string APIKey;
}

[System.Serializable]
public class OpenAIRequest
{
    public string model = "gpt-3.5-turbo";
    public OpenAICommand[] messages;

    public int max_tokens = 150;
    public double temperature = 0;
    public double top_p = 0.5;
    public double frequency_penalty = 0;
    public double presence_penalty = 0;

    public Dictionary<string, int> logit_bias;

    public OpenAIRequest() {
        logit_bias = new Dictionary<string, int>();
    }
}

[System.Serializable]
public class OpenAIRequestResponseType {
    public string type = "";
}

[System.Serializable]
public class OpenAICommand 
{
    public string role;
    public string content;
}

[System.Serializable]
public class OpenAIResponse
{
    public Choice[] choices;
    public OpenAIUsageData[] usage;
}

[System.Serializable]
public class Choice
{
    public int index;
    public OpenAICommand message;
}

[System.Serializable]
public class OpenAIUsageData 
{
    public int promptTokens;
    public int completionTokens;
    public int totalTokens;
}