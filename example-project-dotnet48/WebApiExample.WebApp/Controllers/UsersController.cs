using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.DataStore.Models;
using WebApiExample.WebApp.Models;
using WebApiExample.WebApp.Services;

namespace WebApiExample.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var user = await _userService.GetUserAsync(id);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NewUser newUser)
        {
            // Model validation is handled automatically by [ApiController]
            try
            {
                var user = await _userService.AddUserAsync(newUser.Name, newUser.Age);
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _userService.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}