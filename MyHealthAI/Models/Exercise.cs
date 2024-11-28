using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealthAI.Models
{
    public class Exercise
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int ID { get; set; }
        public int Walk { get; set; }
        public int Run { get; set; }
        public int LiftWeights { get; set; }
        public int ExerciseHighPerformance { get; set; }
        public int ExerciseMediumPerformance { get; set; }
        public int ExerciseLowPerformance { get; set; }
        public DateOnly Date { get; set; }
        public required int UserID { get; set; }

        //propiedad navegacion
        public User User { get; set; }
    }
}
