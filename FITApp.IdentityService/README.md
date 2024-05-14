# Identity Service
Identity Service is responsible for storing and managing users, their roles and roles' permissions.
It is also responsible for giving out JWT to use when making requests to other services.
This service also communicates synchronously [Employees Service](../FITApp.EmployeesService) to make sure their data is in sync.

## Configuration
The service expects for the following options to be set:
- JwtOptions:PublicKey - the public RSA key used to verify the access tokens
- JwtOptions:PrivateKey - the private RSA key used to sign the access tokens
- AdminOptions:Email - the email of the admin user that will be created on the first run
- AdminOptions:Password - the password of the admin user that will be created on the first run
- AdminOptions:RoleName - the role of the admin user that will be created on the first run
- EmailSettings:Email - Google Account email address for sending the emails
- EmailSettings:Password - Google Account App Password that is generated in the Google Account settings
- ConnectionStrings:IdentityDefaultConnection - the connection string for the PostgreSQL database
- FITAppOptions:BaseUrl - the base URL of the app
- FITAppOptions:EmployeeServiceBaseUrl - the base URL of the Employees Service
- FITAppOptions:EmployeeServiceUsersEndpoint - users endpoint in the Employees Service

EmailSettings are not set in appsettings.\
JwtOptions, AdminOptions and ConnectionStrings are already set for development only.\
FITAppOptions are already set for both development and production.\
If needed, you can change any of the already set options.
You can set options either in appsettings files, environment variables or using dotnet user-secrets.

## Running locally
### Prerequisites
- .NET 8 SDK
- PostgreSQL database
- All of the required options
- [Employees Service](../FITApp.EmployeesService) running

### How to run
1. Clone the repository
    ```
    git clone https://github.com/Cubball/FITApp
    ```
1. cd into this folder
    ```
    cd FITApp/FITApp.IdentityService
    ```
1. Run the app
    ```
    dotnet run
    ```

Upon first launch, the service will apply migrations to the database and seed it with the admin user.
It will also make a request to the [Employees Service](../FITApp.EmployeesService) to add the admin user there as well, so make sure it is running before launching this service.
After successfully launching the service will listen on http://localhost:5001

## Endpoints
### Auth
#### Logging in
```
POST /api/auth/login
```
Accepts:
```json
{
  "email": "user@example.com",
  "password": "password"
}
```
Returns:
- 200:
  ```json
  {
    "accessToken": "string",
    "refreshToken": "string"
  }
  ```
- 401: if provided credentials are not correct

#### Refreshing the access token
```
POST /api/auth/refresh
```
Accepts:
```json
{
  "accessToken": "string",
  "refreshToken": "string"
}
```
Returns:
- 200:
  ```json
  {
    "accessToken": "string",
    "refreshToken": "string"
  }
  ```
- 401: if provided credentials are not correct

#### Resetting the password
```
POST /api/auth/reset-password
```
Accepts:
```json
{
  "email": "user@example.com"
}
```
Returns:
- 200
- 400: if provided data is not correct

#### Confirming the password reset
```
GET /api/auth/reset-password-confirm?id=string&token=string
```
Returns:
- 200
- 400: if provided data is not correct

#### Changing the password (requires authorization)
```
POST /api/auth/change-password
```
Accepts:
```json
{
  "oldPassword": "string",
  "newPassword": "string"
}
```
Returns:
- 200
- 400: if provided data is not correct
- 401: if user is not authorized

### Users
#### Creating a user (requires authorization and permissions)
```
POST /api/users
```
Accepts:
```json
{
  "email": "string",
  "roleId": "string"
}
```
Returns:
- 201
- 400: if provided data is not correct
- 401: if user is not authorized
- 403: if user does not have the required permissions

#### Getting a user (requires authorization and permissions)
```
GET /api/users/{id}
```
Returns:
- 200:
  ```json
  {
    "id": "string",
    "email": "string",
    "roleId": "string",
    "roleName": "string"
  }
  ```
- 401: if user is not authorized
- 403: if user does not have the required permissions
- 404: if user with the provided id was not found

#### Getting all users (requires authorization and permissions)
```
GET /api/users
```
Returns:
- 200:
  ```json
  [
    {
      "id": "string",
      "email": "string",
      "roleId": "string",
      "roleName": "string"
    }
  ]
  ```
- 401: if user is not authorized
- 403: if user does not have the required permissions

#### Updating the user's role (requires authorization and permissions)
```
PUT /api/users/{id}/role
```
Accepts:
```json
{
  "roleId": "string"
}
```
Returns:
- 200
- 400: if provided data is not correct
- 401: if user is not authorized
- 403: if user does not have the required permissions
- 404: if user with the provided id was not found

#### Resetting the user's password (requires authorization and permissions)
```
POST /api/users/{id}/reset-password
```
Returns:
- 200
- 401: if user is not authorized
- 403: if user does not have the required permissions
- 404: if user with the provided id was not found

#### Deleting a user (requires authorization and permissions)
```
DELETE /api/users/{id}
```
Returns:
- 200
- 401: if user is not authorized
- 403: if user does not have the required permissions
- 404: if user with the provided id was not found

### Roles
#### Creating a role (requires authorization and permissions)
```
POST /api/roles
```
Accepts:
```json
{
  "name": "string"
  "permissions": ["string"]
}
```
Returns:
- 201
- 400: if provided data is not correct
- 401: if user is not authorized
- 403: if user does not have the required permissions

#### Getting a role (requires authorization and permissions)
```
GET /api/roles/{id}
```
Returns:
- 200:
  ```json
  {
    "id": "string",
    "name": "string",
    "permissions": ["string"]
  }
  ```
- 401: if user is not authorized
- 403: if user does not have the required permissions
- 404: if role with the provided id was not found

#### Getting all roles (requires authorization and permissions)
```
GET /api/roles
```
Returns:
- 200:
  ```json
  [
    {
      "id": "string",
      "name": "string"
    }
  ]
  ```
- 401: if user is not authorized
- 403: if user does not have the required permissions

#### Updating a role (requires authorization and permissions)
```
PUT /api/roles/{id}
```
Accepts:
```json
{
  "name": "string"
  "permissions": ["string"]
}
```
Returns:
- 200
- 400: if provided data is not correct
- 401: if user is not authorized
- 403: if user does not have the required permissions
- 404: if role with the provided id was not found

#### Deleting a role (requires authorization and permissions)
```
DELETE /api/roles/{id}
```
Returns:
- 200
- 400: if there are users with the role
- 401: if user is not authorized
- 403: if user does not have the required permissions
- 404: if role with the provided id was not found

#### Getting all permissions (requires authorization and permissions)
```
GET /api/roles/permissions
```
Returns:
- 200:
  ```json
  [
    {
      "id": 0,
      "name": "string",
      "description": "string"
    }
  ]
  ```
- 401: if user is not authorized
- 403: if user does not have the required permissions
