# 🏠 API de Gestión de Propiedades

API RESTful desarrollada en **.NET 8** para gestión completa de propiedades inmobiliarias con **MongoDB**, **Swagger**, **Docker** y **Clean Architecture**.

## 🚀 Características

- ✅ **Clean Architecture** - Separación clara de responsabilidades
- ✅ **CRUD Completo** - Operaciones completas para propiedades, propietarios y transacciones
- ✅ **Cálculo de Impuestos** - Sistema automático de cálculo de impuestos colombianos
- ✅ **Estados Dinámicos** - Disponibilidad y destacado basados en datos reales
- ✅ **Filtros Avanzados** - Búsqueda compleja con múltiples criterios
- ✅ **Validación Robusta** - Validación de datos en todas las capas
- ✅ **Timestamps** - Seguimiento de creación y modificación
- ✅ **MongoDB** - Base de datos NoSQL escalable
- ✅ **Swagger** - Documentación interactiva de API

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

| Método   | Endpoint             | Descripción                                                              |
| -------- | -------------------- | ------------------------------------------------------------------------ |
| `GET`    | `/api/property`      | Buscar propiedades (filtros: name, address, minPrice, maxPrice, idOwner) |
| `GET`    | `/api/property/{id}` | Detalle de propiedad con información completa                            |
| `POST`   | `/api/property`      | Crear nueva propiedad                                                    |
| `PUT`    | `/api/property/{id}` | Actualizar propiedad existente                                           |
| `DELETE` | `/api/property/{id}` | Eliminar propiedad                                                       |

### 👤 Propietarios

| Método   | Endpoint          | Descripción                                      |
| -------- | ----------------- | ------------------------------------------------ |
| `GET`    | `/api/owner`      | Buscar propietarios (filtros: name, address)     |
| `GET`    | `/api/owner/{id}` | Detalle de propietario con propiedades asociadas |
| `POST`   | `/api/owner`      | Crear nuevo propietario                          |
| `PUT`    | `/api/owner/{id}` | Actualizar propietario existente                 |
| `DELETE` | `/api/owner/{id}` | Eliminar propietario                             |

### 💰 Transacciones de Propiedades

| Método   | Endpoint                  | Descripción                        |
| -------- | ------------------------- | ---------------------------------- |
| `GET`    | `/api/propertyTrace`      | Listar transacciones por propiedad |
| `GET`    | `/api/propertyTrace/{id}` | Obtener transacción específica     |
| `POST`   | `/api/propertyTrace`      | Crear nueva transacción            |
| `PUT`    | `/api/propertyTrace/{id}` | Actualizar transacción existente   |
| `DELETE` | `/api/propertyTrace/{id}` | Eliminar transacción               |

### 🔐 Autenticación

| Método | Endpoint          | Descripción         |
| ------ | ----------------- | ------------------- |
| `GET`  | `/api/auth/token` | Obtener token Auth0 |

## 📁 Arquitectura

```
TaskManagement/
├── TaskManagement.API/           # Controladores, configuración y middleware
├── TaskManagement.Application/   # Servicios, DTOs y lógica de negocio
├── TaskManagement.Domain/        # Entidades, repositorios e interfaces
├── TaskManagement.Infrastructure/ # Persistencia MongoDB y servicios externos
├── TaskManagement.Tests/        # Pruebas unitarias y de integración
└── SeedData/                    # Scripts de población de datos
```

### 🏗️ Clean Architecture

#### Capa de API (TaskManagement.API)

- **Controladores**: Endpoints REST con validación
- **Middleware**: CORS, autenticación, logging
- **Configuración**: Swagger, servicios, inyección de dependencias

#### Capa de Aplicación (TaskManagement.Application)

- **Servicios**: Lógica de negocio y casos de uso
- **DTOs**: Objetos de transferencia de datos
- **Interfaces**: Contratos para servicios y repositorios
- **Validaciones**: Reglas de negocio y validación de datos

#### Capa de Dominio (TaskManagement.Domain)

- **Entidades**: Modelos de negocio (Property, Owner, PropertyTrace)
- **Repositorios**: Interfaces para acceso a datos
- **Value Objects**: Objetos de valor inmutables

#### Capa de Infraestructura (TaskManagement.Infrastructure)

- **Repositorios**: Implementación de acceso a MongoDB
- **Configuración**: Conexión a base de datos
- **Servicios Externos**: Integraciones con APIs externas

## 🛠️ Tecnologías

- **.NET 8** - Framework principal con C# 12
- **MongoDB** - Base de datos NoSQL con MongoDB.Driver 2.32.0
- **Swagger/OpenAPI** - Documentación interactiva de API
- **Docker** - Containerización y orquestación
- **Auth0** - Autenticación y autorización
- **Clean Architecture** - Principios SOLID y separación de responsabilidades
- **AutoMapper** - Mapeo de objetos (opcional)
- **FluentValidation** - Validación de modelos (opcional)

## 🚀 Funcionalidades Principales

### 🏠 Gestión de Propiedades

- ✅ **CRUD Completo** - Crear, leer, actualizar y eliminar propiedades
- ✅ **Filtros Avanzados** - Búsqueda por nombre, dirección, precio y propietario
- ✅ **Estados Dinámicos** - Disponibilidad basada en transacciones
- ✅ **Propiedades Destacadas** - Sistema de destacado con campo booleano
- ✅ **Imágenes** - Gestión de imágenes principales por propiedad
- ✅ **Timestamps** - Seguimiento de creación y modificación

### 👥 Gestión de Propietarios

- ✅ **CRUD Completo** - Operaciones completas para propietarios
- ✅ **Filtros de Búsqueda** - Por nombre y dirección
- ✅ **Información Personal** - Datos completos con foto de perfil
- ✅ **Propiedades Asociadas** - Relación con propiedades
- ✅ **Timestamps** - Seguimiento de creación y modificación

### 💰 Sistema de Transacciones

- ✅ **CRUD Completo** - Gestión completa de transacciones de venta
- ✅ **Cálculo Automático** - Impuestos según normativa colombiana 2025
- ✅ **Historial Completo** - Seguimiento de todas las transacciones
- ✅ **Validación** - Validación robusta de datos de transacciones
- ✅ **Relaciones** - Vinculación con propiedades específicas

### 🧮 Cálculo de Impuestos

- ✅ **Normativa 2025** - Tabla de impuestos actualizada
- ✅ **UVT** - Cálculo basado en Unidades de Valor Tributario
- ✅ **Progresivo** - Tarifas escalonadas según valor de la propiedad
- ✅ **Transparente** - Desglose completo del cálculo de impuestos

### 🔧 Características Técnicas

- ✅ **Clean Architecture** - Separación clara de responsabilidades
- ✅ **Principios SOLID** - Código mantenible y extensible
- ✅ **Inyección de Dependencias** - Desacoplamiento de componentes
- ✅ **Validación Robusta** - Validación en todas las capas
- ✅ **Manejo de Errores** - Gestión centralizada de errores
- ✅ **Logging** - Sistema de logging estructurado

## 📝 Ejemplos de Uso

### 🏠 Propiedades

#### Buscar propiedades con filtros

```bash
GET /api/property?name=casa&minPrice=100000&maxPrice=500000&idOwner=64f1a2b3c4d5e6f7g8h9i0j1
```

#### Crear propiedad

```bash
POST /api/property
{
  "name": "Casa Centro",
  "address": "Av. Principal 123",
  "price": 250000000,
  "codeInternal": "C001",
  "year": 2020,
  "idOwner": "64f1a2b3c4d5e6f7g8h9i0j1",
  "imageUrl": "https://example.com/casa.jpg",
  "imageEnabled": true,
  "featured": false
}
```

#### Actualizar propiedad

```bash
PUT /api/property/64f1a2b3c4d5e6f7g8h9i0j1
{
  "name": "Casa Centro Actualizada",
  "address": "Av. Principal 123",
  "price": 300000000,
  "codeInternal": "C001",
  "year": 2020,
  "idOwner": "64f1a2b3c4d5e6f7g8h9i0j1",
  "imageUrl": "https://example.com/casa-nueva.jpg",
  "imageEnabled": true,
  "featured": true
}
```

### 👥 Propietarios

#### Buscar propietarios

```bash
GET /api/owner?name=Juan&address=Calle
```

#### Crear propietario

```bash
POST /api/owner
{
  "name": "Juan Pérez",
  "address": "Calle 123 #45-67",
  "photo": "https://example.com/photo.jpg",
  "birthday": "1990-01-01"
}
```

#### Actualizar propietario

```bash
PUT /api/owner/64f1a2b3c4d5e6f7g8h9i0j1
{
  "name": "Juan Pérez Actualizado",
  "address": "Calle 123 #45-67",
  "photo": "https://example.com/photo-nueva.jpg",
  "birthday": "1990-01-01"
}
```

### 💰 Transacciones

#### Crear transacción

```bash
POST /api/propertyTrace
{
  "dateSale": "2025-01-15",
  "name": "Comprador ABC",
  "value": 250000000,
  "tax": 3750000,
  "idProperty": "64f1a2b3c4d5e6f7g8h9i0j1"
}
```

#### Listar transacciones por propiedad

```bash
GET /api/propertyTrace?propertyId=64f1a2b3c4d5e6f7g8h9i0j1
```

#### Actualizar transacción

```bash
PUT /api/propertyTrace/64f1a2b3c4d5e6f7g8h9i0j1
{
  "dateSale": "2025-01-20",
  "name": "Comprador XYZ",
  "value": 300000000,
  "tax": 4500000,
  "idProperty": "64f1a2b3c4d5e6f7g8h9i0j1"
}
```

## 🗄️ Estructura de Datos

### Propiedad

```json
{
  "idProperty": "64f1a2b3c4d5e6f7g8h9i0j1",
  "name": "Casa Centro",
  "address": "Av. Principal 123",
  "price": 250000000,
  "codeInternal": "C001",
  "year": 2020,
  "idOwner": "64f1a2b3c4d5e6f7g8h9i0j1",
  "featured": false,
  "createdAt": "2025-01-15T10:30:00Z",
  "updatedAt": "2025-01-15T10:30:00Z"
}
```

### Propietario

```json
{
  "idOwner": "64f1a2b3c4d5e6f7g8h9i0j1",
  "name": "Juan Pérez",
  "address": "Calle 123 #45-67",
  "photo": "https://example.com/photo.jpg",
  "birthday": "1990-01-01",
  "createdAt": "2025-01-15T10:30:00Z",
  "updatedAt": "2025-01-15T10:30:00Z"
}
```

### Transacción

```json
{
  "idPropertyTrace": "64f1a2b3c4d5e6f7g8h9i0j1",
  "dateSale": "2025-01-15",
  "name": "Comprador ABC",
  "value": 250000000,
  "tax": 3750000,
  "idProperty": "64f1a2b3c4d5e6f7g8h9i0j1",
  "createdAt": "2025-01-15T10:30:00Z",
  "updatedAt": "2025-01-15T10:30:00Z"
}
```

## 🌱 Sistema de Seeding

### Población de Datos de Prueba

El proyecto incluye un sistema de seeding para poblar la base de datos con datos de prueba:

```bash
# Ejecutar seeder
cd SeedData
dotnet run
```

#### Características del Seeder

- ✅ **Datos Realistas** - Propiedades, propietarios y transacciones con datos coherentes
- ✅ **Imágenes Reales** - URLs de imágenes de Unsplash para propiedades y propietarios
- ✅ **Relaciones Consistentes** - Propietarios vinculados correctamente con propiedades
- ✅ **Transacciones Válidas** - Historial de ventas con cálculos de impuestos correctos
- ✅ **Estados Variados** - Propiedades destacadas, disponibles y vendidas
- ✅ **Timestamps** - Fechas de creación y modificación realistas

#### Datos Generados

- **7 Propietarios** - Con información personal completa
- **7 Propiedades** - Con diferentes tipos y precios
- **5 Transacciones** - Historial de ventas con impuestos calculados
- **Imágenes Únicas** - Cada propiedad y propietario con imagen única

## 🚀 Despliegue

### Variables de Entorno de Producción

```env
MONGO_CONNECTION_STRING=mongodb://production-server:27017
DATABASE_NAME=PropertiesBD_Production
AUTH0_DOMAIN=production.auth0.com
AUTH0_CLIENT_ID=production-client-id
AUTH0_CLIENT_SECRET=production-client-secret
AUTH0_AUDIENCE=production-audience
```

### Docker Compose

```yaml
version: "3.8"
services:
  api:
    build: .
    ports:
      - "5120:80"
    environment:
      - MONGO_CONNECTION_STRING=mongodb://mongo:27017
      - DATABASE_NAME=PropertiesBD
    depends_on:
      - mongo

  mongo:
    image: mongo:latest
    ports:
      - "27017:27017"
    volumes:
      - mongo_data:/data/db

volumes:
  mongo_data:
```

## 👨‍💻 Desarrollado por

**Ismael Parra** - [LinkedIn](https://www.linkedin.com/in/ismaelparra)
