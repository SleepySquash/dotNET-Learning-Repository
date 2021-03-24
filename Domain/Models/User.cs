using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        
        [Required]
        [StringLength(15, MinimumLength = 10)]
        public string Phone { get; set; }
        
        [StringLength(32)]
        public string FirstName { get; set; }
        
        [StringLength(32)]
        public string MiddleName { get; set; }
        
        [StringLength(32)]
        public string LastName { get; set; }
        
        [StringLength(255)]
        public string Password { get; set; }
    }
}