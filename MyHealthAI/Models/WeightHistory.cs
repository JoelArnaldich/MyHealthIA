using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealthAI.Models
{
    public class WeightHistory
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int ID { get; set; }
        public double Weight { get; set; }
        public DateOnly Date { get; set; }
        public required int UserID { get; set; }

        //propiedad navegacion
        public User User { get; set; }
    }
}