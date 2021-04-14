using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebRazor.Models
{
    public class ConditionJson
    {
        [JsonProperty("data")] public Condition Condition { get; set; }
    }
    
    public class Condition
    {
        [ForeignKey("User")]
        public long UserId { get; set; }

        [Key]
        public long Id { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        
        [Required]
        [StringLength(16)]
        public string State { get; set; }
    }
}