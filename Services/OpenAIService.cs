namespace extractor_c.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Models;

public class OpenAIService
{
    private string OrganizationID;
    private string APIKey;
    private static readonly HttpClient client = new HttpClient();

    private readonly ILogger log = new LoggerFactory().CreateLogger<OpenAIService>();
    public OpenAIService() {
        this.getAPIConfig();
    }

    public async Task<OpenAIResponse> makeRequest(OpenAIRequest openAIRequest)
    {
      string json = JsonSerializer.Serialize(openAIRequest);

      var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");

      httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
      httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);
      httpRequest.Headers.Add("OpenAI-Organization", OrganizationID);

      try {
          var response = await client.SendAsync(httpRequest);
          var responseBody = await response.Content.ReadAsStringAsync();
            
          if (!response.IsSuccessStatusCode) {
            log.LogError($"Error Status: {(int)response.StatusCode} {response.StatusCode}");
            log.LogError($"Response Body: {responseBody}");
            throw new HttpRequestException($"API Error: {responseBody}");
          }

          return JsonSerializer.Deserialize<OpenAIResponse>(responseBody);
      } catch (HttpRequestException e) {
          log.LogError($"Request error: {e.Message}");
          throw;
      }
    }

    public void getAPIConfig() {
        string configDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Configs");
        string filePath = Path.Combine(configDirectory, "openai-api.json");

        try {
            string dataAsJson = File.ReadAllText(filePath);
            OpenAIConfigData configData = JsonSerializer.Deserialize<OpenAIConfigData>(dataAsJson);
            
            this.APIKey = configData.APIKey;
            this.OrganizationID = configData.OrganizationID;
        }
        catch (Exception ex)
        {
            log.LogError(ex.Message);
        }
    }
}