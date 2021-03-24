using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WebApplication.Models
{
    public class ConditionsViewModel
    {
        [JsonProperty("data")] public IEnumerable<Condition> Conditions { get; set; }
        [JsonProperty("user")] public User User { get; set; }

        public ConditionsViewModel()
        {
            Conditions = Enumerable.Empty<Condition>();
            User = null;
        }
    }
}