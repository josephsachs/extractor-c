namespace extractor_c.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Models;

public class OpenAIService
{
    private string OrganizationID;
    private string APIKey;
    private static readonly HttpClient Client = new HttpClient();
    private readonly ILogger _logger;
    public OpenAIService(ILogger<OpenAIService> logger, IOptions<OpenAIConfigData> options)
    {
        var config = options.Value;
        this.APIKey = config.APIKey;
        this.OrganizationID = config.OrganizationID;
        this._logger = logger;
    }

    public async Task<OpenAIResponse> makeRequest(GPT4Request request)
    {
      string json = JsonSerializer.Serialize(request);

      var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");

      httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
      httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);
      httpRequest.Headers.Add("OpenAI-Organization", OrganizationID);

      try {
          var response = await Client.SendAsync(httpRequest);
          var responseBody = await response.Content.ReadAsStringAsync();
            
          if (!response.IsSuccessStatusCode) {
            _logger.LogError($"Error Status: {(int)response.StatusCode} {response.StatusCode}");
            _logger.LogError($"Response Body: {responseBody}");
            throw new HttpRequestException($"API Error: {responseBody}");
          }

          return JsonSerializer.Deserialize<OpenAIResponse>(responseBody);
      } catch (HttpRequestException e) {
          _logger.LogError($"Request error: {e.Message}");
          throw;
      }
    }
}