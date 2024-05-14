# FITApp
This application is designed to store and manage professors' publications and their personal info,
including name, educations, academic degrees, ranks and past postitions.

## Architecture
The app is developed using microservices architecture. It includes the following microservices:
- Identity Serivce
- Employees Service
- API Gateway
- Frontend React App

## Running the app
### Running app locally
To run app locally, refer to each services' README to see the instructions on how to run it locally.

### Running using docker compose
#### Prerequisites
- docker compose

#### How to run
1. Clone the repository
    ```
    git clone https://github.com/Cubball/FITApp
    ```
1. cd into this folder
    ```
    cd FITApp
    ```
1. Create a ```.env``` file in this folder containing the following environment variables, replacing the values in with your own
    ```bash
    # Shared
    JwtOptions__PublicKey=your-public-key
    JwtOptions__PrivateKey=your-private-key

    # Identity Service
    AdminOptions__Email=your-admin-email
    AdminOptions__Password=your-admin-password
    EmailSettings__Email=your-gmail-email-address-used-to-send-emails
    EmailSettings__Password=your-gmail-app-password
    ConnectionStrings__IdentityDefaultConnection=your-postgres-connection-string

    # Employees Service
    CloudinarySettings__CloudName=your-cloudinary-cloud-name
    CloudinarySettings__ApiSecret=your-cloudinary-api-secret
    CloudinarySettings__ApiKey=your-cloudinary-api-key
    MongoDBSettings__ConnectionString=your-mongodb-connection-string
    ```
    _For more info about these variables refer to the appropriate service's README. If an option has a colon (:) in it's name, you need to replace it with double underscore (__) in .env. You can also specify other options if needed, but these are the only one that you must have._
1. Run docker compose
    ```
    docker compose up
    ```
After all of the containers have started the app is ready to be used.

Mapped containers' ports (http):
- Frontend Application - 7777
- API Gateway - 7000
- PostgreSQL - 7001
- MongoDB - 7002
