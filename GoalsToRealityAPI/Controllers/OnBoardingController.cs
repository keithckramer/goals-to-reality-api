using GoalsToRealityAPI.Data;
using GoalsToRealityAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoalsToRealityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnboardingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OnboardingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SaveAnswers([FromBody] OnboardingAnswer dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var answer = new OnboardingAnswer
            {
                Name = dto.Name,
                Age = dto.Age,
                Occupation = dto.Occupation,
                BiggestChallenge = dto.BiggestChallenge,
                Goal = dto.Goal,
                Timeline = dto.Timeline,
                Why = dto.Why,
                WorkTime = dto.WorkTime,
                PlanningStyle = dto.PlanningStyle,
                WantsAccountability = dto.WantsAccountability
            };

            _context.OnboardingAnswers.Add(answer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Answers saved successfully", id = answer.Id });
        }
    }
}
