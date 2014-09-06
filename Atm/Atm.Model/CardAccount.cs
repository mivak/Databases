using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Atm.Model
{
    public class CardAccount
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [Column(TypeName = "char")]
        public string CardNumber { get; set; }

        [Required]
        [MaxLength(4)]
        [Column(TypeName = "char")]
        public string CardPin { get; set; }

        [Column(TypeName = "Money")]
        public decimal? CardCash { get; set; }
    }
}