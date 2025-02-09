namespace GoalsToRealityAPI.Models
{
    public class Task
    {
        public int ID { get; set; }
        public int SubGoalID { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public int Priority { get; set; }
        public decimal Weight { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public SubGoal SubGoal { get; set; }
    }
}

