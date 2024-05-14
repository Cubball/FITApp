# Employees Service
Employees Service is responsible for storing and managing employees' personal info, including thier name, profile picture, educations, etc.

## Configuration
The service expects for the following options to be set:
- JwtOptions:PublicKey - the public RSA key used to verify the access tokens
- CloudinarySettings:CloudName - Cloudinary cloud name
- CloudinarySettings:ApiSecret - Cloudinary API secret
- CloudinarySettings:ApiKey - Cloudinary API key
- MongoDBSettings:DatabaseName - MongoDB database name
- MongoDBSettings:ConnectionString - MongoDB connection string

CloudinarySettings are not set in appsettings.\
JwtOptions, MongoDBSettings:ConnectionString are already set for development only.\
MongoDBSettings:DatabaseName is already set for both development and production.\
If needed, you can change any of the already set options.
You can set options either in appsettings files, environment variables or using dotnet user-secrets.

## Running locally
### Prerequisites
- .NET 8 SDK
- MongoDB database
- All of the required options

### How to run
1. Clone the repository
    ```
    git clone https://github.com/Cubball/FITApp
    ```
1. cd into this folder
    ```
    cd FITApp/FITApp.EmployeesService
    ```
1. Run the app
    ```
    dotnet run
    ```

After successfully launching the service will listen on http://localhost:5002

## Controllers Overview
### Users Controller
The Users Controller manages user-related operations such as adding, modifying, and deleting users.

### Employees Controller
The Employees Controller handles operations related to employee management, including pagination, retrieving employee details, and managing positions, educations, academic degrees, and academic ranks.

### Profile Controller
The Profile Controller is responsible for managing user profiles, including retrieving profile information, updating profile details, managing profile photos, and handling positions, educations, academic degrees, and academic ranks associated with the user's profile.
## Endpoints
### Users
#### Adding a user
```
POST /api/users
```
Request body:
```json
{
    "userId": "userId",
    "email": "email",
    "role": "role",
    "roleId": "roleId"
}
```
Responses:
- 200 OK: User added successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission.

#### Modifying a user
```
PUT /api/users/{id}
```
Accepts:
```json
{
    "email": "email",
    "role": "role",
    "roleId": "roleId"
}
```
Responses:
- 200 OK: User updated successfully.
- 400 Bad Request: Invalid request body.
- 401 Unauthorized: User credentials are incorrect.
- 403 Forbidden: User does not have permission to modify the user.
- 404 Not Found: User with the specified ID not found.

#### Deleting a user
```
DELETE /api/users/{id}
```
Responses:
- 200 OK: User deleted successfully.
- 401 Unauthorized: User credentials are incorrect.
- 403 Forbidden: User does not have permission to delete the user.
- 404 Not Found: User with the specified ID not found.


### Employees

#### Pagination
```
GET /api/employees?page=1&pageSize=10
```
Responses:
- 200 OK: Returns a list of employees with pagination details.
```json
{
    "page": 1,
    "pageSize": 10,
    "total": 100,
    "employees": [
        {
            "id": "id",
            "firstName": "firstName",
            "lastName": "lastName",
            "patronymic": "patronymic",
            "email": "email",
            "role": "role"
        }
    ]
}
```
- 400 Bad Request: Invalid request.
- 403 Forbidden: User does not have permission to access employee data.

#### Retrieve an employee by ID
```
GET /api/employees/{id}
```
Responses:
- 200 OK: Returns the details of the specified employee.
```json
{
    "id": "id",
    "user": {
        "userId": "userId",
        "email": "email",
        "role": "role",
        "roleId": "roleId"
    },
    "firstName": "firstName",
    "lastName": "lastName",
    "patronymic": "patronymic",
    "birthDate": "birthDate",
    "photoUrl": "photoUrl",
    "positions": [
        {
            "name": "name",
            "startDate": "startDate",
            "endDate": "endDate"
        }
    ],
    "educations": [
        {
            "university": "university",
            "specialty": "specialty",
            "diplomaDateOfIssue": "diplomaDateOfIssue"
        }
    ],
    "academicDegrees": [
        {
            "fullName": "name",
            "shortName": "name",
            "diplomaNumber": "diplomaNumber",
            "dateOfIssue": "dateOfIssue"
        }
    ],
    "academicRanks": [
        {
            "name": "name",
            "certificateNumber": "certificateNumber",
            "dateOfIssue": "dateOfIssue"
        }
    ]
}
```
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to access employee data.
- 404 Not Found: Employee with the specified ID not found.

#### Update employee's profile photo
```
PUT /api/employees/{id}/photo
```
Responses:
- 200 OK: Employee's profile photo updated successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to update employee's profile photo.
- 404 Not Found: Employee with the specified ID not found.

#### Add a position to the specified user
```
POST /api/employees/{id}/positions
```
Request body:
```
{
    "name": "name",
    "startDate": "startDate",
    "endDate": "endDate"
}
```
Responses:
- 200 OK: Position added successfully.
- 400 Bad Request: Invalid request body.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to add a position to the employee.
- 404 Not Found: Employee with the specified ID not found.

#### Delete a position from the specified user
```
DELETE /api/employees/{id}/positions/{index}
```
Responses:
- 200 OK: Position deleted successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to delete the position.
- 404 Not Found: Position not found for the employee.

#### Add an education to the specified employee
```
POST /api/employees/{id}/educations
```
Request body:
```
{
    "university": "university",
    "specialty": "specialty",
    "diplomaDateOfIssue": "diplomaDateOfIssue"
}
```
Responses:
- 400 Bad Request: Invalid request body.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to add education information to the employee.
- 404 Not Found: Employee with the specified ID not found.

#### Delete an education from the specified employee
```
DELETE /api/employees/{id}/educations/{index}
```
Responses:
- 200 OK: Education information deleted successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to delete education information.
- 404 Not Found: Education information not found for the employee.

#### Add an academic degree to the specified employee
```
POST /api/employees/{id}/academic-degrees
```
Request body:
```
{
    "fullName": "name",
    "shortName": "name",
    "diplomaNumber": "diplomaNumber",
    "dateOfIssue": "dateOfIssue"
}
```
Responses:
- 400 Bad Request: Invalid request body.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to add academic degree information to the employee.
- 404 Not Found: Employee with the specified ID not found.

#### Delete an academic degree from the specified employee
```
DELETE /api/employees/{id}/academic-degrees/{index}
```
Responses:
- 200 OK: Academic degree information deleted successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to delete academic degree information.
- 404 Not Found: Academic degree information not found for the employee.

#### Add an academic rank to the specified employee
```
POST /api/employees/{id}/academic-ranks
```
Request body:
```
{
    "name": "name",
    "certificateNumber": "certificateNumber",
    "dateOfIssue": "dateOfIssue"
}
```
Responses:
- 400 Bad Request: Invalid request body.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to add academic rank information to the employee.
- 404 Not Found: Employee with the specified ID not found.

#### Delete an academic rank from the specified employee
```
DELETE /api/employees/{id}/academic-ranks/{index}
```
Responses:
- 200 OK: Academic rank information deleted successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to delete academic rank information.
- 404 Not Found: Academic rank information not found for the employee.
#### Retrieve an image by id
```
GET /api/images/{id}
```
Responses:
- 200 OK: The image with the specified ID is successfully retrieved and returned in the response body.
- 404 Not Found: The image with the specified ID does not exist in the system.
### Profile

#### Retrieve the profile information of the current user
```
GET /api/profile
```

Responses:
- 200 OK: Successfully retrieved the user profile.
```
{
    "id": "id",
    "user": {
        "userId": "userId",
        "email": "email",
        "role": "role",
        "roleId": "roleId"
    },
    "firstName": "firstName",
    "lastName": "lastName",
    "patronymic": "patronymic",
    "birthDate": "birthDate",
    "photoUrl": "photoUrl",
    "positions": [
        {
            "name": "name",
            "startDate": "startDate",
            "endDate": "endDate"
        }
    ],
    "educations": [
        {
            "university": "university",
            "specialty": "specialty",
            "diplomaDateOfIssue": "diplomaDateOfIssue"
        }
    ],
    "academicDegrees": [
        {
            "fullName": "name",
            "shortName": "name",
            "diplomaNumber": "diplomaNumber",
            "dateOfIssue": "dateOfIssue"
        }
    ],
    "academicRanks": [
        {
            "name": "name",
            "certificateNumber": "certificateNumber",
            "dateOfIssue": "dateOfIssue"
        }
    ]
}
```
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to access the profile.

#### Upload a photo for the user profile
```
PUT /api/profile/photo
```
Responses:
- 200 OK: Photo uploaded successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to upload a photo.


#### Delete the user's profile photo
```
DELETE /api/profile/photo
```
Responses:
- 200 OK: Photo deleted successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to delete the photo.
- 404 Not Found: Photo not found.

#### Update the profile information of the current user
```
PUT /api/profile
```
Request Body:
```
{
    "firstName": "firstName",
    "lastName": "lastName",
    "patronymic": "patronymic",
    "birthDate": "birthDate",
}
```
Responses:
- 200 OK: Profile information updated successfully.
- 400 Bad Request: Invalid request body.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to update the profile.
- 404 Not Found: User profile not found.

#### Add a new position to the user's profile.
```
POST /api/profile/positions
```
Request Body:
```
{
    "name": "name",
    "startDate": "startDate",
    "endDate": "endDate"
}
```
Responses:
- 200 OK: Position added successfully.
- 400 Bad Request: Invalid request body.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to add a position.

#### Delete a position from the user's profile.
```
DELETE /api/profile/positions/{index}
```
Responses:
- 200 OK: Position deleted successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to delete the position.
- 404 Not Found: Position not found.

#### Add education information to the user's profile.
```
POST /api/profile/educations
```
Request body:
```
{
    "university": "university",
    "specialty": "specialty",
    "diplomaDateOfIssue": "diplomaDateOfIssue"
}
```
Responses:
- 200 OK: Education information added successfully.
- 400 Bad Request: Invalid request body.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to add education information.

#### Delete education information from the user's profile.
```
DELETE /api/profile/educations/{index}
```
Responses:
- 200 OK: Education deleted successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to delete education information.
- 404 Not Found: Education information not found.

#### Add academic degree information to the user's profile.
```
POST /api/profile/academic-degrees
```
Request body:
```
{
    "fullName": "name",
    "shortName": "name",
    "diplomaNumber": "diplomaNumber",
    "dateOfIssue": "dateOfIssue"
}
```
Responses:
- 200 OK: Academic degree information added successfully.
- 400 Bad Request: Invalid request body.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to add academic degree information.

#### Delete academic degree information from the user's profile.
```
DELETE /api/profile/academic-degrees/{index}
```
Responses:
- 200 OK: Academic degree information deleted successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to delete academic degree information.
- 404 Not Found: Academic degree information not found.

#### Add academic rank information to the user's profile.
```
POST /api/profile/academic-ranks
```
Request body:
```
{
    "name": "name",
    "certificateNumber": "certificateNumber",
    "dateOfIssue": "dateOfIssue"
}
```
Responses:
- 200 OK: Academic rank information added successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to add academic rank information.
- 404 Not Found: Academic rank information not found.

#### Delete academic rank information from the user's profile.
```
DELETE /api/profile/academic-ranks/{index}
```
Responses:
- 200 OK: Academic rank information deleted successfully.
- 401 Unauthorized: User is not authenticated.
- 403 Forbidden: User does not have permission to delete academic rank information.
- 404 Not Found: Academic rank information not found.
