using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService.Models.Interfaces
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetTeachers();
        Task<Teacher> GetTeacher(string id);
        Task<Teacher> CreateTeacher(Teacher teacher);
        Task UpdateTeacher(string id, Teacher teacher);
        Task DeleteTeacher(string id);
    }
}