using GoalsToRealityAPI.Data;
using GoalsToRealityAPI.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/goals")]
[ApiController]
public class GoalsController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly OpenAiService _openAiService;

    public GoalsController(AppDbContext dbContext, OpenAiService openAiService)
    {
        _dbContext = dbContext;
        _openAiService = openAiService;
    }

    public class GoalRequest
    {
        public string Title { get; set; }
        public string GoalDescription { get; set; }
        public int Priority { get; set; }
        public decimal Weight { get; set; }
        public DateTime? DueDate { get; set; }
    }


    // Step 1: Save Goal & Start AI Conversation
    [HttpPost("start-reflection")]
    public async Task<IActionResult> StartReflection([FromBody] GoalRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest(new { message = "Goal title is required" });

        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if (string.IsNullOrEmpty(userIdString))
            return Unauthorized(new { message = "User not found" });

        // Convert userIdString (string) to an int
        if (!int.TryParse(userIdString, out int userId))
            return BadRequest(new { message = "Invalid user ID format" });

        var newGoal = new Goal
        {
            UserID = userId,  // ✅ Now userId is correctly converted to int
            GoalTitle = request.Title,
            GoalDescription = request.GoalDescription,
            Priority = request.Priority,
            Weight = request.Weight,
            DueDate = request.DueDate
        };



        _dbContext.Goals.Add(newGoal);
        await _dbContext.SaveChangesAsync();

        // Get first AI question
        string firstQuestion = await _openAiService.GetFirstQuestionAsync(request.Title);

        return Ok(new { message = "Goal saved successfully", question = firstQuestion });
    }

    // Step 2: Handle Step-by-Step AI Questions
    [HttpPost("next-question")]
    public async Task<IActionResult> GetNextQuestion([FromBody] ReflectionRequest request)
    {
        if (request.Answers.Count >= 10)
        {
            // Step 3: Get Final Analysis
            string analysis = await _openAiService.GetFinalAnalysisAsync(request.Answers);
            return Ok(new { finalAnalysis = analysis });
        }

        // Get the next question
        string nextQuestion = await _openAiService.GetNextQuestionAsync(request.Answers);
        return Ok(new { question = nextQuestion });
    }
}

// DTOs for Requests
public class GoalRequest
{
    public string Title { get; set; }
    public string Category { get; set; }
    public string Deadline { get; set; }
    public string Motivation { get; set; }
}

public class ReflectionRequest
{
    public List<string> Answers { get; set; }
}
