using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class UserUpdateDTO
    {
        [Required]
        [StringLength(15, MinimumLength = 10)]
        public string Phone { get; set; }
        
        [StringLength(32)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        
        [StringLength(32)]
        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }
        
        [StringLength(32)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        
        [StringLength(255)]
        public string Password { get; set; }
    }
}