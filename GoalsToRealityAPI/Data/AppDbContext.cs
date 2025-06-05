using GoalsToRealityAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GoalsToRealityAPI.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
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
        public DbSet<OnboardingAnswer> OnboardingAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User → State
            modelBuilder.Entity<User>()
                .HasOne(u => u.State)
                .WithMany()
                .HasForeignKey(u => u.StateID)
                .OnDelete(DeleteBehavior.Restrict);

            // Answer → User
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // Answer → Question
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany()
                .HasForeignKey(a => a.QuestionID)
                .OnDelete(DeleteBehavior.Restrict);

            // Progress → User
            modelBuilder.Entity<Progress>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // Progress → Goal
            modelBuilder.Entity<Progress>()
                .HasOne(p => p.Goal)
                .WithMany()
                .HasForeignKey(p => p.GoalID)
                .OnDelete(DeleteBehavior.Cascade);

            // Progress → SubGoal
            modelBuilder.Entity<Progress>()
                .HasOne(p => p.SubGoal)
                .WithMany()
                .HasForeignKey(p => p.SubGoalID)
                .OnDelete(DeleteBehavior.Restrict);

            // Progress → Task
            modelBuilder.Entity<Progress>()
                .HasOne(p => p.Task)
                .WithMany()
                .HasForeignKey(p => p.TaskID)
                .OnDelete(DeleteBehavior.Restrict);

            // Progress → SubTask
            modelBuilder.Entity<Progress>()
                .HasOne(p => p.SubTask)
                .WithMany()
                .HasForeignKey(p => p.SubTaskID)
                .OnDelete(DeleteBehavior.Restrict);

            // Decimal precision to prevent EF warnings
            modelBuilder.Entity<Goal>()
                .Property(g => g.Weight)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SubGoal>()
                .Property(sg => sg.Weight)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<GoalsToRealityAPI.Models.Task>()
                .Property(t => t.Weight)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Progress>()
                .Property(p => p.ProgressValue)
                .HasColumnType("decimal(18,2)");

            // State table name override
            modelBuilder.Entity<State>().ToTable("States");
        }
    }
}
