using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebRazor.Models;

namespace WebRazor.Pages.Conditions
{
    public class Edit : PageModel
    {
        [BindProperty]
        public Condition Condition { get; set; }
        
        public async Task OnGet(int? id, int? userId)
        {
            if (id != null || userId != null)
            {
                if (id == null) Condition = new Condition {UserId = userId.Value, Date = DateTime.Now};
                else
                {
                    using var client = new HttpClient {BaseAddress = new Uri("https://localhost:5003/api/")};
                    var response = await client.GetAsync("conditions/" + id);
                    if (response.IsSuccessStatusCode)
                        Condition = JsonConvert
                            .DeserializeObject<ConditionJson>(await response.Content.ReadAsStringAsync())?.Condition;
                }
            }
        }
        
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return RedirectToPage();
            using var client = new HttpClient {BaseAddress = new Uri("https://localhost:5003/api/")};
            var response = (Condition.Id == 0)
                ? await client.PostAsJsonAsync("conditions", Condition)
                : await client.PutAsJsonAsync("conditions/" + Condition.Id, Condition);
            return RedirectToPage("Index", new { userId = Condition.UserId });
        }
    }
}