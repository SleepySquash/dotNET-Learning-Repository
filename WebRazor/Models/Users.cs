using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WebRazor.Models
{
    public class Users
    {
        [JsonProperty("data")] public IEnumerable<User> List { get; set; }

        public Users()
        {
            List = Enumerable.Empty<User>();
        }
    }
}