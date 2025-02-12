using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoalsToRealityAPI.Controllers
{
    [Route("api/goals")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly OpenAiService _openAiService;

        public GoalsController(OpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        [HttpPost("generate-questions")]
        public async Task<IActionResult> GenerateQuestions([FromBody] string goalTitle)
        {
            if (string.IsNullOrWhiteSpace(goalTitle))
                return BadRequest(new { message = "Goal title is required" });

            string prompt = $"A user wants to achieve the goal: '{goalTitle}'. Generate 4 key questions to refine it.";
            string aiResponse = await _openAiService.GetAiResponseAsync(prompt);

            return Ok(new { aiQuestions = aiResponse.Split("\n") });
        }
    }

}
