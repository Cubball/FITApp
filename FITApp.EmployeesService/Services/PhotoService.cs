using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using FITApp.EmployeesService.Validators;
using FluentValidation;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FITApp.EmployeesService.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IEmployeesRepository employeeRepository;
        private readonly IMapper mapper;
        private readonly IValidator<EmployeePhotoUploadDto> validator;
        private readonly Cloudinary _cloudinary;

        public PhotoService(IEmployeesRepository employeeRepository,IMapper mapper, IOptions<CloudinarySettings> config, IValidator<EmployeePhotoUploadDto> validator)
        {

            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
            this.validator = validator;

            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<long> UpdateEmployeePhoto(string id, EmployeePhotoUploadDto photoUploadDto)
        {
            var validationResult = await validator.ValidateAsync(photoUploadDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException("Photo validation failed.", validationResult.Errors);
            }
            var employee = mapper.Map<EmployeePhotoUpload>(photoUploadDto);
            var file = photoUploadDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face") // зміна розміру фото
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            var urlPhoto = uploadResult.Url.ToString();

            var update = Builders<Employee>.Update.Set(e => e.Photo, urlPhoto);

            var result = await employeeRepository.UpdateEmployee(id, update);
            return result.ModifiedCount;
        }
        public async Task<long> RemoveEmployeePhoto(string id)
        {
            var employee = await employeeRepository.GetEmployee(id);

            if (employee != null && !string.IsNullOrEmpty(employee.Photo))
            {
                var publicId = GetPublicIdFromUrl(employee.Photo);

                var deletionParams = new DeletionParams(publicId);
                var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

                if (deletionResult.Result == "ok")
                {
                    var update = Builders<Employee>.Update.Unset(e => e.Photo);
                    var result = await employeeRepository.UpdateEmployee(id, update);
                    return result.ModifiedCount;
                }
                else
                {
                    throw new Exception("Failed to delete photo from Cloudinary.");
                }
            }
            else
            {
                return 0;
            }
        }

        private string GetPublicIdFromUrl(string url)
        {
            var startIndex = url.LastIndexOf("/") + 1;
            var endIndex = url.LastIndexOf(".") - startIndex;
            return url.Substring(startIndex, endIndex);
        }
    }

}
