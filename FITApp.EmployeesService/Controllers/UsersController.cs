using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.EmployeesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            await _usersService.CreateUser(userDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            long deletedCount = await _usersService.DeleteUser(id);

            if (deletedCount == 0)
            {
                return NotFound(); // Якщо елемент не знайдено
            }

            return NoContent(); // Успішний видалення, не потрібно повертати тіло відповіді
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateDto userUpdateDto)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid employee ID.");
            }

            long updatedCount = await _usersService.UpdateUserDetails(id, userUpdateDto);
            return updatedCount == 0 ? NotFound() : Ok();

        }
    }
}


