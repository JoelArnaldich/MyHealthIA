using Microsoft.EntityFrameworkCore;

namespace MyHealthAI.Models
{
    public class AppDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-F6LUMJ4,49170\\SQLEXPRESS\\SQLEXPRESS;Database=MyHealthAiDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealType> MealType { get; set; }
        public DbSet<DialyExercise> DialyExercises { get; set; }
        public DbSet<DialyWater> DialyWater { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<AnswerIA> AnswerIA { get; set; }

    }
}




