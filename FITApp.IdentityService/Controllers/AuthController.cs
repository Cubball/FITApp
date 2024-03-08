using FITApp.IdentityService.Contracts.Requests;
using FITApp.IdentityService.Contracts.Responses;
using FITApp.IdentityService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FITApp.IdentityService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public AuthController(
        ITokenService tokenService,
        IUserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokensResponse>> Login([FromBody] LoginRequest request)
    {
        var credentialsValid = await _userService.IsUserValidAsync(request.Email, request.Password);
        if (!credentialsValid)
        {
            return Unauthorized();
        }

        var jwt = _tokenService.GenerateJwt(await _userService.GetClaimsAsync(request.Email));
        var refreshToken = _tokenService.GenerateRefreshToken();
        var success = await _userService.UpdateRefreshTokenAsync(request.Email, refreshToken);
        if (!success)
        {
            return StatusCode(500);
        }

        return new TokensResponse
        {
            AccessToken = jwt,
            RefreshToken = refreshToken,
        };
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<TokensResponse>> Refresh([FromBody] RefreshRequest request)
    {
        var (claims, isValid) = await _tokenService.GetClaimsFromExpiredTokenAsync(request.AccessToken);
        if (!isValid)
        {
            return Unauthorized();
        }

        var email = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
        if (email is null)
        {
            return Unauthorized();
        }

        var refreshTokenValid = await _userService.IsRefreshTokenValidAsync(email, request.RefreshToken);
        if (!refreshTokenValid)
        {
            return Unauthorized();
        }

        var jwt = _tokenService.GenerateJwt(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var success = await _userService.UpdateRefreshTokenAsync(email, refreshToken);
        if (!success)
        {
            return StatusCode(500);
        }

        return new TokensResponse
        {
            AccessToken = jwt,
            RefreshToken = refreshToken,
        };
    }

    [HttpPost("change-password")]
    public ActionResult ChangePassword([FromBody] ChangePasswordRequest request)
    {
        // TODO: this endpoint will require authentication
        throw new NotImplementedException();
    }
}