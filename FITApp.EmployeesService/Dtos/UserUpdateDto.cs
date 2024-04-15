
using System.ComponentModel.DataAnnotations;

namespace FITApp.EmployeesService.Dtos
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }
        [Required(ErrorMessage = "RoleId is required.")]
        public string RoleId { get; set; }
    }
}