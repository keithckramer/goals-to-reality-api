using GoalsToRealityAPI.Data;
using GoalsToRealityAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GoalsToRealityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<State>> GetStates()
        {
            var states = _context.States.ToList();
            return Ok(states);
        }
    }
}

