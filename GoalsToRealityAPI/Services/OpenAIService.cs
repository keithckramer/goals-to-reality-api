using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class OpenAiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAiService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _apiKey = configuration["OpenAI:ApiKey"];
    }

    public async Task<string> GetAiResponseAsync(string prompt)
    {
        var requestBody = new
        {
            model = "gpt-4",
            messages = new[] { new { role = "system", content = prompt } },
            max_tokens = 150
        };

        var jsonRequest = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

        var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error from OpenAI: {response.StatusCode}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        using JsonDocument doc = JsonDocument.Parse(jsonResponse);
        var result = doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

        return result ?? "No response";
    }
}

