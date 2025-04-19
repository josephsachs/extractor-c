namespace extractor_c.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

public class OpenAIService
{
    private string OrganizationID;
    private string APIKey;
    private static readonly HttpClient client = new HttpClient();


    public OpenAIService() {
        this.getAPIConfig();
    }

    public async Task<string> makeRequest(OpenAIRequest openAIRequest)
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
            Console.WriteLine($"Error Status: {(int)response.StatusCode} {response.StatusCode}");
            Console.WriteLine($"Response Body: {responseBody}");

            foreach (var header in response.Headers) {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
            throw new HttpRequestException($"API Error: {responseBody}");
          }

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
            
            this.APIKey = configData.APIKey;
            this.OrganizationID = configData.OrganizationID;
        }
        else
        {
            Console.WriteLine("Cannot find config file");
        }
    }
}