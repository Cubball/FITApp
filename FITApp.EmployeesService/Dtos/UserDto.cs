using System.ComponentModel.DataAnnotations;

namespace FITApp.EmployeesService.Dtos
{
    public class UserDto : UserUpdateDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; }
    }
}