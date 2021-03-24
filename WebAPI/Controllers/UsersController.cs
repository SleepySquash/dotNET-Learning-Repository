using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(new { data = await _usersService.AllAsync() });
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var userFromDb = await _usersService.GetAsync(id);
            if (userFromDb == null) return NotFound();
            return Json(new
            {
                data = userFromDb
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserCreateDTO user)
        {
            try
            {
                User userCreated = _mapper.Map<User>(user);
                await _usersService.CreateAsync(userCreated);
                return CreatedAtAction(nameof(Get), new {id = userCreated.Id}, userCreated);
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, UserUpdateDTO user)
        {
            try
            {
                User userModified = _mapper.Map<User>(user);
                userModified.Id = id;
                await _usersService.UpdateAsync(userModified);
            }
            catch (DbUpdateConcurrencyException) { return NotFound(); }
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try { await _usersService.RemoveAsync(id); }
            catch (Exception e) { return NotFound(e.Message); }
            return NoContent();
        }
    }
}