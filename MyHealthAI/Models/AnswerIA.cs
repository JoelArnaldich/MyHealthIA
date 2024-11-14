using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealthAI.Models
{
    public class AnswerIA
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int ID { get; set; }
        public required string Answers { get; set; }
        public required int UserID { get; set; }
        //propiedad navegacion
        public User User { get; set; }
    }
}
