# API Gateway
## Info
This API Gateway is a reverse proxy that forwards requests to the appropriate microservice. Routes are mapped 1:1. 
The gateway also handles authentication using JWT tokens.
Also, the gateway modifies the request and response when working with the identity service:
- For the ```/api/auth/login``` and ```/api/auth/refresh``` routes: The gateway removes the refresh token from the response body and appends the 'Set-Cookie' header with the refresh token. \
    Modified response body from those routes:
    ```json
    {
        "accessToken": "token"
    }
    ```
- For the ```/api/auth/refresh``` route: The gateway retrieves the refresh token from the 'Cookie' header and the expired access token from the 'Authorization' header and appends them to the request body. Therefore, ```/api/auth/refresh``` route does not require a body, but expects the 'Cookie' and 'Authorization' headers to be set.
## Configuration
The following options need to be set for the gateway to work properly:
- JwtOptions:PublicKey - the public key used to verify the access tokens
- CorsOptions:AllowedOrigins - the list of allowed origins
- ReverseProxy - YARP configuration

For convenience, those options are already set in the appsettings.Development.json file.
