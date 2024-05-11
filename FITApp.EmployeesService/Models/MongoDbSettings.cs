using FITApp.EmployeesService.Interfaces;

namespace FITApp.EmployeesService.Models
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}