using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geotik.Entities
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int UserId { get; set; }

        [Required]
        public DateTime OpenDate { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
