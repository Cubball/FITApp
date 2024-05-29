FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /

COPY FITApp.PublicationsService/FITApp.PublicationsService.csproj ./FITApp.PublicationsService/
COPY FITApp.Auth/FITApp.Auth.csproj ./FITApp.Auth/
RUN dotnet restore ./FITApp.PublicationsService/

COPY FITApp.Auth/ ./FITApp.Auth/
COPY FITApp.PublicationsService/ ./FITApp.PublicationsService/
RUN dotnet publish -c Release -o /app ./FITApp.PublicationsService/ --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_HTTP_PORTS=5003
ENTRYPOINT ["dotnet", "FITApp.PublicationsService.dll"]
