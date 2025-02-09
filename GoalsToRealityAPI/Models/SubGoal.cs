namespace GoalsToRealityAPI.Models
{
    public class SubGoal
    {
        public int ID { get; set; }
        public int GoalID { get; set; }
        public string SubGoalTitle { get; set; }
        public string SubGoalDescription { get; set; }
        public int Priority { get; set; }
        public decimal Weight { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Goal Goal { get; set; }
    }
}
