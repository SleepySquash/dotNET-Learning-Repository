using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WebApplication.Models
{
    public class UsersViewModel
    {
        [JsonProperty("data")] public IEnumerable<User> Users { get; set; }

        public UsersViewModel()
        {
            Users = Enumerable.Empty<User>();
        }
    }
}