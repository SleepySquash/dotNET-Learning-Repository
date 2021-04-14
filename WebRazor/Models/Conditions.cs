using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WebRazor.Models
{
    public class Conditions
    {
        [JsonProperty("data")] public IEnumerable<Condition> List { get; set; }
        [JsonProperty("user")] public User User { get; set; }

        public Conditions()
        {
            List = Enumerable.Empty<Condition>();
            User = null;
        }
    }
}