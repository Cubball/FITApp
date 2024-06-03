using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Options;
using Microsoft.Extensions.Options;

namespace FITApp.EmployeesService.Services;

public class PublicationsService(HttpClient httpClient, IOptions<FITAppOptions> appOptions) : IPublicationsService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly FITAppOptions _appOptions = appOptions.Value;

    public async Task<bool> UpdateAuthorDetailsAsync(string userId, AuthorDto author)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{_appOptions.PublicationsServiceUsersEndpoint}/{userId}", author);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
    
    public async Task<bool> UpdateProfileDetailsAsync(AuthorDto author)
    {
        try
        {
            Console.WriteLine(_appOptions.PublicationsServiceUsersEndpoint);
            var response = await _httpClient.PutAsJsonAsync($"{_appOptions.PublicationsServiceUsersEndpoint}", author);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}