using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ConditionsController : Controller
    {
        private readonly ILogger<ConditionsController> _logger;
        public ConditionsController(ILogger<ConditionsController> logger)
        {
            _logger = logger;
        }
        
        public async Task<ActionResult> Index(long userId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:5003/api/");
                    _logger.LogInformation("User id: " + userId);
                    var response = await client.GetAsync("conditions/user/" + userId);
                    if (response.IsSuccessStatusCode)
                    {
                        ConditionsViewModel conditions =
                            JsonConvert.DeserializeObject<ConditionsViewModel>(
                                await response.Content.ReadAsStringAsync());
                        
                        _logger.LogInformation(await response.Content.ReadAsStringAsync());
                        
                        return View(conditions);
                    }
                    else ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                catch (SystemException e) {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator."); }
            }

            return View(new ConditionsViewModel());
        }
        
        [BindProperty] public Condition Condition { get; set; }
        
        public async Task<ActionResult> Edit(long? userId, long? id)
        {
            if (userId == null && id == null) return BadRequest("User ID or Condition ID must not be null");
            
            Condition = new Condition();
            if (id == null)
            {
                Condition.UserId = userId.Value;
                Condition.Date = DateTime.Now;
                return View(Condition);
            }
            
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:5003/api/");
                    var response = await client.GetAsync("conditions/" + id);
                    if (response.IsSuccessStatusCode)
                        return View(JsonConvert.DeserializeObject<ConditionJson>(await response.Content.ReadAsStringAsync())?.Condition);
                    else ModelState.AddModelError(string.Empty, "Client error. Response: " + response.Content);
                }
                catch (SystemException e) {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator."); }
            }

            return View(Condition);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit()
        {
            if (!ModelState.IsValid) return View(Condition);
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:5003/api/");
                    var response = (Condition.Id == 0)
                        ? await client.PostAsJsonAsync("conditions", Condition)
                        : await client.PutAsJsonAsync("conditions/" + Condition.Id, Condition);
                    if (response.IsSuccessStatusCode) return RedirectToAction("Index", new { userId = Condition.UserId });
                    else
                    {
                        var readAsStringAsync = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", "Client error. Response " + response.StatusCode + ": " + readAsStringAsync);
                    }
                }
                catch (SystemException e) {
                    ModelState.AddModelError("", "Server error. Cannot connect to API."); }
            }
            return View(Condition);
        }
        
        [HttpGet]
        public async Task<ActionResult> Delete(long id, long userId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:5003/api/");
                    var response = await client.DeleteAsync("conditions/" + id);
                    if (response.IsSuccessStatusCode) return RedirectToAction("Index", new { userId = userId });
                    else
                    {
                        var readAsStringAsync = await response.Content.ReadAsStringAsync();
                        _logger.LogError(readAsStringAsync);
                        ModelState.AddModelError("", "Client error. Response " + response.StatusCode + ": " + readAsStringAsync);
                    }
                }
                catch (SystemException e) {
                    ModelState.AddModelError("", "Server error. Cannot connect to API."); }
            }
            return NoContent();
        }
    }
}