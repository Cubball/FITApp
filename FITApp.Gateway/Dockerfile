FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /

COPY FITApp.Gateway/FITApp.Gateway.csproj ./FITApp.Gateway/
COPY FITApp.Auth/FITApp.Auth.csproj ./FITApp.Auth/
RUN dotnet restore ./FITApp.Gateway/

COPY FITApp.Auth/ ./FITApp.Auth/
COPY FITApp.Gateway/ ./FITApp.Gateway/
RUN dotnet publish -c Release -o /app ./FITApp.Gateway/ --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "FITApp.Gateway.dll"]
