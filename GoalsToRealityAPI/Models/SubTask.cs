namespace GoalsToRealityAPI.Models
{
    public class SubTask
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public string SubTaskTitle { get; set; }
        public string SubTaskDescription { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Task Task { get; set; }
    }
}

