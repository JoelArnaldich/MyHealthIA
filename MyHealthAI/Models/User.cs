using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealthAI.Models
{
    [Index(nameof(Name), nameof(Email), IsUnique = true)]
    public class User
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int ID { get; set; }

        public required int?  Height { get; set; }
        public required double? Weight { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required int? Age { get; set; }
        public int ObjectiveID { get; set; }
        public int GenderID { get; set; }
        public int? DailyKcal { get; set; }
        public int ActivityID {  get; set; }
        public double? GoalWeight { get; set; }
        public int? DailyPro {  get; set; }
        public int? DailyCar { get; set; }
        public int? DailyFat { get; set; }
        public int? DailyWater { get; set; }

        //propiedad navegacion

        public List<Meal> Meals { get; set; }
        public List<Water> Water { get; set; }
        public List<Exercise> Exercises { get; set; }
        public Objective Objective { get; set; }
        public Gender gender { get; set; }
        public Activity activity { get; set; }

    }
}
