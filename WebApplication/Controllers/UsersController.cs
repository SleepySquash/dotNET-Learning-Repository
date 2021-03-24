using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public UsersController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public async Task<ActionResult> Index()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri("https://localhost:5003/api/");
                        var response = await client.GetAsync("users");
                        if (response.IsSuccessStatusCode)
                        {
                            UsersViewModel users =
                                JsonConvert.DeserializeObject<UsersViewModel>(
                                    await response.Content.ReadAsStringAsync());
                            return View(users);
                        }
                        else ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                    catch (SystemException e)
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }

                return View(new UsersViewModel());
            }
            catch (HttpRequestException e) { return Problem("Server is probably offline or unreachable"); }
        }

        [BindProperty] public User User { get; set; }
        
        public async Task<ActionResult> Edit(long? id)
        {
            User = new User();
            if (id == null) return View(User);
            
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:5003/api/");
                    var response = await client.GetAsync("users/" + id);
                    if (response.IsSuccessStatusCode)
                        return View(JsonConvert.DeserializeObject<UserJson>(await response.Content.ReadAsStringAsync())?.User);
                    else ModelState.AddModelError(string.Empty, "Client error. Response: " + response.Content);
                }
                catch (SystemException e) {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator."); }
            }

            return View(User);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit()
        {
            if (!ModelState.IsValid) return View(User);
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:5003/api/");
                    var response = (User.Id == 0)
                        ? await client.PostAsJsonAsync("users", User)
                        : await client.PutAsJsonAsync("users/" + User.Id, User);
                    if (response.IsSuccessStatusCode) return RedirectToAction("Index");
                    else
                    {
                        var readAsStringAsync = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", "Client error. Response " + response.StatusCode + ": " + readAsStringAsync);
                    }
                }
                catch (SystemException e) {
                    ModelState.AddModelError("", "Server error. Cannot connect to API."); }
            }
            return View(User);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(long id)
        {
            _logger.LogInformation("hi!");
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:5003/api/");
                    var response = await client.DeleteAsync("users/" + id);
                    if (response.IsSuccessStatusCode) return RedirectToAction("Index");
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