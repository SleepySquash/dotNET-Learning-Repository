using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConditionsController : Controller
    {
        private readonly IConditionsService _conditionsService;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        public ConditionsController(IConditionsService conditionsService, IUsersService usersService, IMapper mapper)
        {
            _conditionsService = conditionsService;
            _usersService = usersService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(new { data = await _conditionsService.AllAsync() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var condition = await _conditionsService.GetAsync(id);
            return Json(new { data = condition });
        }
        
        [HttpGet("user/{id}")]
        public async Task<IActionResult> User(long id)
        {
            var conditions = await _conditionsService.AllOfUserAsync(id);
            var user = await _usersService.GetAsync(id);
            return Json(new { data = conditions, user = user });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ConditionCreateDTO condition)
        {
            try
            {
                Condition conditionCreated = _mapper.Map<Condition>(condition);
                await _conditionsService.CreateAsync(conditionCreated);
                return CreatedAtAction(nameof(Get), new {id = conditionCreated.Id}, conditionCreated);
            }
            catch (Exception e)
            {
                if (e.Message == "Not found") return NotFound("User with such ID was not found");
                return BadRequest("Error adding value: might be duplicate.");
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, ConditionUpdateDTO condition)
        {
            try
            {
                Condition conditionModified = _mapper.Map<Condition>(condition);
                conditionModified.Id = id;
                await _conditionsService.UpdateAsync(conditionModified);
                return NoContent();
            }
            catch (Exception e)
            {
                if (e.Message == "Not found") return NotFound("User with such ID was not found");
                return BadRequest("Error updating value.");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _conditionsService.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception e) { return BadRequest("Error deleting value: id might be invalid."); }
        }
    }
}