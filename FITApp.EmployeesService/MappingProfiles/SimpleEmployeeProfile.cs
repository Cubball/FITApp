using AutoMapper;
using FITApp.EmployeesService.Dtos;
using MongoDB.Bson;

namespace FITApp.EmployeesService;

public class SimpleEmployeeProfile : Profile
{
    public SimpleEmployeeProfile()
    {
        CreateMap<BsonDocument, SimpleEmployeeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GetValue("_id").AsString))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => GetStringValue(src, "firstName")))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => GetStringValue(src, "lastName")))
            .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => GetStringValue(src, "patronymic")))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => GetNestedValue(src, "user", "email")))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => GetNestedValue(src, "user", "role")));
    }

    private string? GetStringValue(BsonDocument src, string fieldName)
    {
        return src.Contains(fieldName) ? src.GetValue(fieldName).AsString : null;
    }

    private string? GetNestedValue(BsonDocument src, string parentFieldName, string nestedFieldName)
    {
        if (src.Contains(parentFieldName))
        {
            var parentField = src[parentFieldName].AsBsonDocument;
            return parentField.Contains(nestedFieldName) ? parentField.GetValue(nestedFieldName).AsString : null;
        }
        return null;
    }
}
