namespace GoalsToRealityAPI.Models
{
    public class Answer
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public int UserID { get; set; }
        public string AnswerText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Question Question { get; set; }
        public User User { get; set; }
    }
}

