using System.Text;
using System.Text.Json;
using FITApp.Gateway.Serialization;
using Microsoft.Net.Http.Headers;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace FITApp.Gateway.Transforms;

public class ResponseTransformProvider : ITransformProvider
{
    private const string RefreshRouteId = "refresh-route";
    private const string LoginRouteId = "login-route";
    private const string RefreshTokenCookieName = "refreshToken";
    private const string CookiePath = "/api/auth/refresh";

    public void Apply(TransformBuilderContext context)
    {
        var routeId = context.Route.RouteId;
        if (routeId is RefreshRouteId or LoginRouteId)
        {
            context.AddResponseTransform(async transformContext =>
            {
                if (!(transformContext.ProxyResponse?.IsSuccessStatusCode ?? false))
                {
                    return;
                }

                var proxyResponse = transformContext.ProxyResponse;
                var responseStream = await proxyResponse.Content.ReadAsStreamAsync();
                using var sr = new StreamReader(responseStream);
                var body = await sr.ReadToEndAsync();
                var apiResponse = JsonSerializer.Deserialize<FullAuthResponse>(body);
                if (apiResponse is null)
                {
                    return;
                }

                var newBody = JsonSerializer.Serialize(new { accessToken = apiResponse.AccessToken, });
                var response = transformContext.HttpContext.Response;
                // HACK: boo, not strongly typed
                response.Headers.Append(HeaderNames.SetCookie, $"{RefreshTokenCookieName}={apiResponse.RefreshToken};HttpOnly;Path={CookiePath};SameSite=None;Secure");
                await response.Body.WriteAsync(Encoding.UTF8.GetBytes(newBody));
            });
        }
    }

    public void ValidateCluster(TransformClusterValidationContext context) { }

    public void ValidateRoute(TransformRouteValidationContext context) { }
}