using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealthAI.Models
{
    [Index(nameof(Username), nameof(Email), IsUnique = true)]
    public class User
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int ID { get; set; }

        public required int  Height { get; set; }
        public required double Weight { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required int Age { get; set; }
        public int? ObjectiveID { get; set; }
        public int GenderID { get; set; }
        public int ActivityID {  get; set; }
        public int? GoalWeight { get; set; }

        //propiedad navegacion

        public List<AnswerIA> answersIA { get; set; }
        public List<Meal> Meals { get; set; }
        public List<DialyWater> DialyWater { get; set; }
        public List<DialyExercise> dialy_Exercises { get; set; }
        public Objective Objective { get; set; }
        public Gender gender { get; set; }
        public Activity activity { get; set; }  

    }
}
