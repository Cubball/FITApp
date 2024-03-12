using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace FITApp.Gateway.Transforms;

public class RequestTransformProvider : ITransformProvider
{
    private const string RefreshRouteId = "refresh-route";
    private const string RefreshTokenCookieName = "refreshToken";
    private const string JwtPrefix = "Bearer ";

    public void Apply(TransformBuilderContext context)
    {
        var routeId = context.Route.RouteId;
        if (routeId == RefreshRouteId)
        {
            context.AddRequestTransform(transformContext =>
            {
                var refreshToken = transformContext.HttpContext.Request.Cookies[RefreshTokenCookieName];
                if (string.IsNullOrWhiteSpace(refreshToken))
                {
                    return ValueTask.CompletedTask;
                }

                var accessToken = transformContext.HttpContext.Request.Headers.Authorization.ToString();
                if (string.IsNullOrWhiteSpace(accessToken))
                {
                    return ValueTask.CompletedTask;
                }

                accessToken = accessToken.Replace(JwtPrefix, string.Empty);
                transformContext.ProxyRequest.Content = new StringContent(
                        JsonSerializer.Serialize(new { refreshToken, accessToken }),
                        Encoding.UTF8,
                        MediaTypeNames.Application.Json);
                return ValueTask.CompletedTask;
            });
        }
    }

    public void ValidateCluster(TransformClusterValidationContext context) { }

    public void ValidateRoute(TransformRouteValidationContext context) { }
}