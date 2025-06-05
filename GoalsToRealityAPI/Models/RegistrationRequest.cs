namespace GoalsToRealityAPI.Models
{
    public class RegistrationRequest
    {
        public string? Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public int? StateID { get; set; }
        public string? Zip { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
