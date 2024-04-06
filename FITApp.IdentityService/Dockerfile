FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /

COPY FITApp.IdentityService/FITApp.IdentityService.csproj ./FITApp.IdentityService/
COPY FITApp.Auth/FITApp.Auth.csproj ./FITApp.Auth/
RUN dotnet restore ./FITApp.IdentityService/

COPY FITApp.IdentityService/ ./FITApp.IdentityService/
COPY FITApp.Auth/ ./FITApp.Auth/
RUN dotnet publish -c Release -o /app ./FITApp.IdentityService/

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "FITApp.IdentityService.dll"]
