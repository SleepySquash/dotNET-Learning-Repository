using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebRazor.Models;

namespace WebRazor.Pages.Users
{
    public class Edit : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        
        public async Task OnGet(int? id)
        {
            if (id == null) User = new User();
            else
            {
                using var client = new HttpClient {BaseAddress = new Uri("https://localhost:5003/api/")};
                var response = await client.GetAsync("users/" + id);
                if (response.IsSuccessStatusCode)
                    User = JsonConvert.DeserializeObject<UserJson>(await response.Content.ReadAsStringAsync())?.User;
            }
        }
        
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return RedirectToPage();
            using var client = new HttpClient {BaseAddress = new Uri("https://localhost:5003/api/")};
            var response = (User.Id == 0)
                ? await client.PostAsJsonAsync("users", User)
                : await client.PutAsJsonAsync("users/" + User.Id, User);
            return RedirectToPage("Index");
        }
    }
}