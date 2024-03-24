# Identity Service
## Configuration
The following options need to be set for the service to work properly:
- JwtOptions:PublicKey - the public key used to verify the access tokens
- JwtOptions:PrivateKey - the private key used to sign the access tokens
- AdminOptions:Email - the email of the admin user that will be created on the first run
- AdminOptions:Password - the password of the admin user that will be created on the first run
- AdminOptions:RoleName - the role of the admin user that will be created on the first run
- EmailSettings:Email - Google Account email address
- EmailSettings:Password - Google Account App Password that is generated in the Google Account settings

For development purposes, JwtOptions and AdminOptions options are already set in the appsettings.Development.json file.
However, in a production environment, they should be set securely elsewhere.

EmailSettings can be set using dotnet user-secrets.

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
GET /api/auth/confirm-reset-password?id=string&token=string
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
  "roleName": "string"
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
  "roleName": "string"
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
