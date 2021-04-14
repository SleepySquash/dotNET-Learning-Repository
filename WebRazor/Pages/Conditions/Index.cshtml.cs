using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebRazor.Pages.Conditions
{
    public class Index : PageModel
    {
        public Models.Conditions Conditions { get; set; }
        
        public async Task OnGet(int userId)
        {
            using var client = new HttpClient {BaseAddress = new Uri("https://localhost:5003/api/")};
            var response = await client.GetAsync("conditions/user/" + userId);
            if (response.IsSuccessStatusCode)
                Conditions = JsonConvert.DeserializeObject<Models.Conditions>(await response.Content.ReadAsStringAsync());
        }
        
        public async Task<IActionResult> OnPostDelete(int id, int userId)
        {
            using var client = new HttpClient {BaseAddress = new Uri("https://localhost:5003/api/")};
            var response = await client.DeleteAsync("conditions/" + id);
            return RedirectToPage("Index", new{userId = userId});
        }
    }
}