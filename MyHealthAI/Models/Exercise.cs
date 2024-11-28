using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealthAI.Models
{
    public class Exercise
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int ID { get; set; }
        public string ExerciseType { get; set; } 
        public double DurationInMinutes { get; set; } 
        public double CaloriesBurned { get; set; } 
        public DateOnly Date { get; set; }
        public required int UserID { get; set; }

        //propiedad navegacion
        public User User { get; set; }
    }
}
