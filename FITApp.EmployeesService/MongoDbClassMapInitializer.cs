using FITApp.EmployeesService.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
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

                em.MapMember(em => em.User).SetElementName("user");
                em.MapMember(em => em.FirstName).SetElementName("firstName");
                em.MapMember(em => em.LastName).SetElementName("lastName");
                em.MapMember(em => em.Patronymic).SetElementName("patronymic");
                em.MapMember(em => em.BirthDate).SetElementName("birthDate");
                em.MapMember(em => em.Photo).SetElementName("photo");
                em.MapMember(em => em.Positions).SetElementName("positions");
                em.MapMember(em => em.Educations).SetElementName("educations");
                em.MapMember(em => em.AcademicDegrees).SetElementName("academicDegrees");
                em.MapMember(em => em.AcademicRanks).SetElementName("academicRanks");


            });
        }
        public static void AddConventionPack()
        {

            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
        }
    }
}