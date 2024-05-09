using FITApp.EmployeesService.Models;
using MongoDB.Bson.Serialization;
namespace FITApp.EmployeesService
{
    public static class MongoDbClassMapInitializer
    {
        public static void RegisterClassMaps()
        {
            BsonClassMap.RegisterClassMap<Employee>(em =>
            {
                em.AutoMap();
                em.MapMember(em => em.Positions).SetDefaultValue(new List<Position>());
                em.MapMember(em => em.Educations).SetDefaultValue(new List<Education>());
                em.MapMember(em => em.AcademicDegrees).SetDefaultValue(new List<AcademicDegree>());
                em.MapMember(em => em.AcademicRanks).SetDefaultValue(new List<AcademicRank>());

            });
        }
    }
}