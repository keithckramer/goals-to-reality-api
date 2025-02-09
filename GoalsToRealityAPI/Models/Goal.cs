namespace GoalsToRealityAPI.Models
{
    public class Goal
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDescription { get; set; }
        public int Priority { get; set; }
        public decimal Weight { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
    }
}
