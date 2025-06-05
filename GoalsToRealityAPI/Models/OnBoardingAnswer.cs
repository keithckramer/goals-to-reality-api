using System.ComponentModel.DataAnnotations;

namespace GoalsToRealityAPI.Models
{
    public class OnboardingAnswer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Age { get; set; }
        public string Occupation { get; set; }
        public string BiggestChallenge { get; set; }

        public string Goal { get; set; }
        public string Timeline { get; set; }
        public string Why { get; set; }

        public string WorkTime { get; set; }
        public string PlanningStyle { get; set; }
        public bool WantsAccountability { get; set; }
    }
}

