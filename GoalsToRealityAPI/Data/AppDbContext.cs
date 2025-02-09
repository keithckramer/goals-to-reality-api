using GoalsToRealityAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GoalsToRealityAPI.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet for User table with additional fields
        public DbSet<User> Users { get; set; }

        // Other tables in the database
        public DbSet<Goal> Goals { get; set; }
        public DbSet<SubGoal> SubGoals { get; set; }
        public DbSet<GoalsToRealityAPI.Models.Task> Tasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerChoice> AnswerChoices { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Progress> Progress { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between User and State
            modelBuilder.Entity<User>()
                .HasOne(u => u.State) // Each User has one State
                .WithMany() // State does not need a Users collection
                .HasForeignKey(u => u.StateID) // Foreign Key in User
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Additional configurations if needed
        }

    }
}

