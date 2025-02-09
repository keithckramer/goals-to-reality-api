using GoalsToRealityAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GoalsToRealityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly AppDbContext _context;

        //Inject AppDBcontext
        public TestController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint to check database connection
        [HttpGet("check-database")]
        public IActionResult CheckDatabase()
        {
            try
            {
                // Test Query: Count Users in the database
                var usersCount = _context.Users.Count();
                return Ok(new { message = "Database connection succesful", usersCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Database connection failed", error = ex.Message });
            }
        }
    }
}
