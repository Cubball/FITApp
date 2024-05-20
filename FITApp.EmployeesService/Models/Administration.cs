using MongoDB.Bson;

namespace FITApp.EmployeesService.Models
{
    public class Administration
    {
        public ObjectId Id { get; set; }
        public Author HeadOfDepartment { get; set; }

        public Author ScientificSecretary { get; set; }
    }
}