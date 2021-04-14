using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebRazor.Models;

namespace WebRazor.Pages.Users
{
    public class Index : PageModel
    {
        public Models.Users Users { get; set; }

        public async Task OnGet()
        {
            using var client = new HttpClient {BaseAddress = new Uri("https://localhost:5003/api/")};
            var response = await client.GetAsync("users");
            if (response.IsSuccessStatusCode)
                Users = JsonConvert.DeserializeObject<Models.Users>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            using var client = new HttpClient {BaseAddress = new Uri("https://localhost:5003/api/")};
            var response = await client.DeleteAsync("users/" + id);
            return RedirectToPage("Index");
        }
    }
}