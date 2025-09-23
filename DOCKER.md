# Docker Setup - Million Project Backend

## Prerrequisitos

- Docker Desktop instalado
- Docker Compose v2.0+

## Configuración Rápida

### 1. Variables de Entorno

Crea un archivo `.env` en la raíz del proyecto con las siguientes variables:

```bash
# Database Configuration
MONGO_CONNECTION_STRING=mongodb://localhost:27017
DATABASE_NAME=PropertiesBD
MONGO_ROOT_USERNAME=admin
MONGO_ROOT_PASSWORD=password

# Auth0 Configuration
AUTH0_DOMAIN=your-auth0-domain.us.auth0.com
AUTH0_CLIENT_ID=your-client-id
AUTH0_CLIENT_SECRET=your-client-secret
AUTH0_AUDIENCE=https://your-auth0-domain.us.auth0.com/api/v2/

# Application Configuration
ASPNETCORE_ENVIRONMENT=Production
```

### 2. Ejecutar con Docker Compose

```bash
# Construir y ejecutar todos los servicios
docker-compose up --build

# Ejecutar en segundo plano
docker-compose up -d --build

# Ver logs
docker-compose logs -f

# Detener servicios
docker-compose down
```

### 3. Servicios Disponibles

- **API Backend**: http://localhost:5120
- **MongoDB**: localhost:27017
- **Health Check**: http://localhost:5120/health

## Comandos Útiles

### Desarrollo

```bash
# Reconstruir solo la API
docker-compose up --build taskmanagementapi

# Ver logs de un servicio específico
docker-compose logs -f taskmanagementapi

# Ejecutar comandos en el contenedor
docker-compose exec taskmanagementapi bash
```

### Producción

```bash
# Ejecutar en modo producción
ASPNETCORE_ENVIRONMENT=Production docker-compose up -d

# Verificar estado de los servicios
docker-compose ps

# Verificar health checks
docker-compose exec taskmanagementapi curl http://localhost:8080/health
```

## Estructura del Proyecto

```
MillionProjectBackend/
├── TaskManagement.API/
│   └── Dockerfile          # Dockerfile para la API
├── docker-compose.yml      # Configuración de servicios
├── .dockerignore          # Archivos a ignorar en el build
├── init-mongo.js          # Script de inicialización de MongoDB
└── .env.example           # Plantilla de variables de entorno
```

## Características de Seguridad

- ✅ Usuario no-root en el contenedor
- ✅ Variables de entorno para configuración sensible
- ✅ Health checks para monitoreo
- ✅ Restart policies configuradas
- ✅ Volúmenes persistentes para datos

## Troubleshooting

### Puerto ya en uso

```bash
# Cambiar puerto en docker-compose.yml
ports:
  - "5121:8080"  # Cambiar 5120 por 5121
```

### Problemas de conexión a MongoDB

```bash
# Verificar que MongoDB esté ejecutándose
docker-compose ps mongodb

# Ver logs de MongoDB
docker-compose logs mongodb
```

### Limpiar volúmenes

```bash
# Detener y eliminar volúmenes
docker-compose down -v

# Eliminar imágenes no utilizadas
docker system prune -a
```
