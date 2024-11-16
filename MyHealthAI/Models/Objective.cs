using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealthAI.Models
{
    public class Objective
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int ID { get; set; }

        public bool LoseWeight { get; set; } = false;

        public bool LoseWeightWinMuscle { get; set; } = false;

        public bool WinMuscleWinWeight { get; set; } = false;


    }
}
