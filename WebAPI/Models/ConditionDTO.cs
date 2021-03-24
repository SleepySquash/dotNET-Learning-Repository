using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class ConditionDTO
    {
        [ForeignKey("User")]
        public long UserId { get; set; }

        [Key]
        public long Id { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        [StringLength(16)]
        public string State { get; set; }
    }
}