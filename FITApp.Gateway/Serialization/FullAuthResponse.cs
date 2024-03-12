using System.Text.Json.Serialization;

namespace FITApp.Gateway.Serialization;

public class FullAuthResponse
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = null!;

    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; } = null!;
}