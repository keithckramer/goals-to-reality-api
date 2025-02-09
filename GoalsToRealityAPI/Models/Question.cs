namespace GoalsToRealityAPI.Models
{
    public class Question
    {
        public int ID { get; set; }
        public int QuestionnaireID { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Questionnaire Questionnaire { get; set; }
        public ICollection<AnswerChoice> AnswerChoices { get; set; }
    }
}

