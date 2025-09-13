# Etapa base para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar y restaurar dependencias específicas del proyecto
COPY ["TaskManagement.API/TaskManagement.API.csproj", "TaskManagement.API/"]
COPY ["TaskManagement.Infrastructure/TaskManagement.Infrastructure.csproj", "TaskManagement.Infrastructure/"]
COPY ["TaskManagement.Domain/TaskManagement.Domain.csproj", "TaskManagement.Domain/"]
COPY ["TaskManagement.Application/TaskManagement.Application.csproj", "TaskManagement.Application/"]

# Restaurar dependencias
RUN dotnet restore "TaskManagement.API/TaskManagement.API.csproj"

# Copiar el resto de los archivos del proyecto
COPY . .
WORKDIR "/src/TaskManagement.API"

# Construir la aplicación
RUN dotnet build "TaskManagement.API.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "TaskManagement.API.csproj" -c Release -o /app/publish

# Configuración final para la imagen
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Configurar el punto de entrada de la aplicación
ENTRYPOINT ["dotnet", "TaskManagement.API.dll"]
