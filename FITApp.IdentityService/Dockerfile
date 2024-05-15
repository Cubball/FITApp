FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /

COPY FITApp.IdentityService/FITApp.IdentityService.csproj ./FITApp.IdentityService/
COPY FITApp.Auth/FITApp.Auth.csproj ./FITApp.Auth/
RUN dotnet restore ./FITApp.IdentityService/

COPY FITApp.Auth/ ./FITApp.Auth/
COPY FITApp.IdentityService/ ./FITApp.IdentityService/
RUN dotnet publish -c Release -o /app ./FITApp.IdentityService/ --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_HTTP_PORTS=5001
ENTRYPOINT ["dotnet", "FITApp.IdentityService.dll"]
