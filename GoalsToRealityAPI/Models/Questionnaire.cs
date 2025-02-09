namespace GoalsToRealityAPI.Models
{
    public class Questionnaire
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}

