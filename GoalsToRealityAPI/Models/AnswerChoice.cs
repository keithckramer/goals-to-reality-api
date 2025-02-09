namespace GoalsToRealityAPI.Models
{
    public class AnswerChoice
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public string ChoiceValue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Question Question { get; set; }
    }
}

