using System;
using System.Collections.Generic;
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

    public async Task<string> GetFirstQuestionAsync(string userGoal)
    {
        string prompt = $"A user wants to achieve this goal: '{userGoal}'.\n\n" +
                        "I want to uncover the masks I am currently wearing, the roles I am playing, and the illusions I am believing. " +
                        "Please guide me through this by asking me 10 reflective questions, one at a time, to help me recognize the stories I am telling myself. " +
                        "Only respond with the first question to start.";

        return await GetOpenAiResponse(prompt);
    }

    public async Task<string> GetNextQuestionAsync(List<string> answers)
    {
        string formattedAnswers = string.Join("\n", answers);
        string prompt = $"The user has answered the following questions:\n{formattedAnswers}\n\n" +
                        "Now, please provide the next question in the series. Do not include previous questions, only the next one.";

        return await GetOpenAiResponse(prompt);
    }

    public async Task<string> GetFinalAnalysisAsync(List<string> answers)
    {
        string formattedAnswers = string.Join("\n", answers);
        string prompt = $"The user has answered all 10 questions:\n{formattedAnswers}\n\n" +
                        "Now, step into the role of their higher self and analyze their responses. " +
                        "Identify the top negative patterns in their life, the top positive pattern they can embrace to grow, and provide daily affirmations/routines. " +
                        "Be direct and truthful, tough love is welcomed. Provide actionable steps to change their behavior and embody their authentic self. " +
                        "End with a message of encouragement.";

        return await GetOpenAiResponse(prompt);
    }

    private async Task<string> GetOpenAiResponse(string prompt)
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
