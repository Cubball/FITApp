FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /

COPY FITApp.EmployeesService/FITApp.EmployeesService.csproj ./FITApp.EmployeesService/
COPY FITApp.Auth/FITApp.Auth.csproj ./FITApp.Auth/
RUN dotnet restore ./FITApp.EmployeesService/

COPY FITApp.EmployeesService/ ./FITApp.EmployeesService/
COPY FITApp.Auth/ ./FITApp.Auth/
RUN dotnet publish -c Release -o /app ./FITApp.EmployeesService/

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
RUN apt-get update && apt-get install -y curl
COPY --from=build /app .
ENV ASPNETCORE_HTTP_PORTS=5002
ENTRYPOINT ["dotnet", "FITApp.EmployeesService.dll"]
