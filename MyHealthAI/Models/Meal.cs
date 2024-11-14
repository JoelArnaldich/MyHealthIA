using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealthAI.Models
{
    public class Meal
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int ID { get; set; }
        public required string Name { get; set; }
        public required int Kcal { get; set; }
        public int? Weight { get; set; }
        public int Protein { get; set; }
        public int Carbohydrate { get; set; }
        public int Fat { get; set; }
        public DateOnly Date { get; set; }
        public int MealTypeID { get; set; }
        public required int UserID { get; set; }

        //propiedad navegacion

        public User User { get; set; }
        public MealType MealType { get; set; }
    }
}
