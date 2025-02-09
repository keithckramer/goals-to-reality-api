namespace GoalsToRealityAPI.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string PaymentMethod { get; set; }
        public string BillingAddress { get; set; }
        public int? StateID { get; set; }
        public string ZipCode { get; set; }
        public string PaymentToken { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public State State { get; set; }
    }
}
