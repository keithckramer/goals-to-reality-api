namespace GoalsToRealityAPI.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateID { get; set; }
        public string Zip { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property for the State (if you have a States table)
        public State State { get; set; }
    }
}
