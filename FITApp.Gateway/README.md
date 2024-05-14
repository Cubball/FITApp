# API Gateway
This API Gateway is a reverse proxy that forwards requests to the appropriate microservice. Routes are mapped 1:1.
The gateway also handles authentication using JWT tokens.

## Configuration
The service expects for the following options to be set:
- JwtOptions:PublicKey - the public RSA key used to verify the access tokens
- CorsOptions:AllowedOrigins - the list of allowed origins
- ReverseProxy - YARP configuration

JwtOptions:PublicKey is already set for development only.\
CorsOptions and ReverseProxy configuration are already set for both development and production.\
If needed, you can change any of the already set options.
You can set options either in appsettings files, environment variables or using dotnet user-secrets.

## Running locally
### Prerequisites
- .NET 8 SDK
- All of the required options

### How to run
1. Clone the repository
    ```
    git clone https://github.com/Cubball/FITApp
    ```
1. cd into this folder
    ```
    cd FITApp/FITApp.Gateway
    ```
1. Run the app
    ```
    dotnet run
    ```

After successfully launching the gateway will listen on https://localhost:7000 and http://localhost:5000

## Routes
The gateway forwards requests to [Identity Service](../FITApp.IdentityService) and [Employees Service](../FITApp.EmployeesService).
Routes are mapped one-to-one, and for two of those routes the gateway modifies request/response:
- [```/api/auth/login```](../FITApp.IdentityService/README.md#logging-in):
    - Request - unchanged:
        ```json
        {
          "email": "user@example.com",
          "password": "password"
        }
        ```
    - Response - refresh token is removed from the body and appended in the 'Set-Cookie' header. Modified response body:
        ```json
        {
            "accessToken": "token"
        }
        ```
- [```/api/auth/refresh```](../FITApp.IdentityService/README.md#refreshing-the-access-token):
    - Request - expired access token is retrieved from the 'Authorization' header and refresh token is retrieved from the 'Cookie' header to be appended to the body before forwarding the request.
    Therefore they are not sent in the body. Modified request body:
        ```json
        {
        }
        ```
    - Response - refresh token is removed from the body and appended in the 'Set-Cookie' header. Modified response body:
        ```json
        {
            "accessToken": "token"
        }
        ```
