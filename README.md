# 🏠 API de Gestión de Propiedades

API RESTful desarrollada en **.NET 8** para gestión de propiedades inmobiliarias con **MongoDB**, **Swagger** y **Docker**.

## 🚀 Inicio Rápido

### Prerrequisitos

- .NET 8.0
- MongoDB (local o en la nube)
- Docker (opcional)

### Instalación

1. **Clonar y configurar**

   ```bash
   git clone https://github.com/HeartKush/ProjectMillion.git
   cd ProjectMillion
   dotnet restore
   ```

2. **Variables de entorno**
   Crear archivo `.env`:

   ```env
   MONGO_CONNECTION_STRING=mongodb://localhost:27017
   DATABASE_NAME=PropertiesBD
   AUTH0_DOMAIN=tu-dominio.auth0.com
   AUTH0_CLIENT_ID=tu-client-id
   AUTH0_CLIENT_SECRET=tu-client-secret
   AUTH0_AUDIENCE=tu-audience
   ```

3. **Ejecutar**

   ```bash
   # Local
   dotnet run

   # Docker
   docker-compose up --build
   ```

4. **Swagger UI**
   ```
   http://localhost:5120/swagger
   ```

## 📋 Endpoints

### 🏠 Propiedades

| Método | Endpoint             | Descripción                                                     |
| ------ | -------------------- | --------------------------------------------------------------- |
| `GET`  | `/api/property`      | Buscar propiedades (filtros: name, address, minPrice, maxPrice) |
| `GET`  | `/api/property/{id}` | Detalle de propiedad                                            |
| `POST` | `/api/property`      | Crear propiedad                                                 |

### 👤 Propietarios

| Método | Endpoint          | Descripción                        |
| ------ | ----------------- | ---------------------------------- |
| `GET`  | `/api/owner`      | Buscar propietarios (filtro: name) |
| `GET`  | `/api/owner/{id}` | Detalle de propietario             |
| `POST` | `/api/owner`      | Crear propietario                  |

### 📊 Rastros de Propiedades

| Método | Endpoint                                      | Descripción                          |
| ------ | --------------------------------------------- | ------------------------------------ |
| `GET`  | `/api/propertytrace/by-property/{propertyId}` | Historial de ventas de una propiedad |
| `POST` | `/api/propertytrace`                          | Registrar venta                      |

### 🔐 Autenticación

| Método | Endpoint          | Descripción         |
| ------ | ----------------- | ------------------- |
| `GET`  | `/api/auth/token` | Obtener token Auth0 |

## 📁 Arquitectura

```
TaskManagement/
├── TaskManagement.API/           # Controladores y configuración
├── TaskManagement.Application/   # Servicios y DTOs
├── TaskManagement.Domain/        # Entidades y repositorios
├── TaskManagement.Infrastructure/ # Persistencia MongoDB
└── TaskManagement.Tests/        # Pruebas unitarias
```

## 🛠️ Tecnologías

- **.NET 8** - Framework principal
- **MongoDB** - Base de datos NoSQL
- **Swagger** - Documentación de API
- **Docker** - Containerización
- **Auth0** - Autenticación
- **Clean Architecture** - Principios SOLID

## 📝 Ejemplos de Uso

### Buscar propiedades

```bash
GET /api/property?name=casa&minPrice=100000&maxPrice=500000
```


### Crear propiedad

```bash
POST /api/property
{
  "name": "Casa Centro",
  "address": "Av. Principal 123",
  "price": 250000,
  "codeInternal": "C001",
  "year": 2020,
  "idOwner": "64f1a2b3c4d5e6f7g8h9i0j1"
}
```

### Crear propietario

```bash
POST /api/owner
{
  "name": "Juan Pérez",
  "address": "Calle 123",
  "photo": "https://example.com/photo.jpg",
  "birthday": "1990-01-01"
}
```

## 👨‍💻 Desarrollado por

**Ismael Parra** - [LinkedIn](https://www.linkedin.com/in/ismaelparra)
