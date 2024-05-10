using FITApp.IdentityService.Contracts.Requests;
using FITApp.IdentityService.Options;
using Microsoft.Extensions.Options;

namespace FITApp.IdentityService.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _httpClient;
    private readonly FITAppOptions _appOptions;

    public EmployeeService(
        HttpClient httpClient,
        IOptions<FITAppOptions> appOptions)
    {
        _httpClient = httpClient;
        _appOptions = appOptions.Value;
    }

    public async Task<bool> CreateAsync(CreateEmployeeRequest user)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_appOptions.EmployeeServiceUsersEndpoint, user);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(string userId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{_appOptions.EmployeeServiceUsersEndpoint}/{userId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(UpdateEmployeeRequest user)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{_appOptions.EmployeeServiceUsersEndpoint}/{user.Id}", user);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}