namespace GoalsToRealityAPI.Models
{
    public class Progress
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int GoalID { get; set; }
        public int? SubGoalID { get; set; }
        public int? TaskID { get; set; }
        public int? SubTaskID { get; set; }
        public DateTime ProgressDate { get; set; }
        public decimal ProgressValue { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public Goal Goal { get; set; }
        public SubGoal SubGoal { get; set; }
        public Task Task { get; set; }
        public SubTask SubTask { get; set; }
    }
}
