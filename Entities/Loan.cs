using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geotik.Entities
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public int AccountId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int Ammount { get; set; }

        [Required]
        public bool Borrow { get; set; }

        [Required]
        public bool Payed { get; set; }
    }
}
