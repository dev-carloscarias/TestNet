# PruebaNET_CarlosCarias_API

## Descripción del Proyecto

Este proyecto es una API RESTful construida con .NET Core (versión 8.0). La API se utiliza para manejar el registro y la gestión de productos. Incluye funcionalidades para insertar, actualizar y obtener productos por su ID, con características adicionales como el almacenamiento en caché de los estados de los productos y el logueo del tiempo de respuesta de las solicitudes.

## Arquitectura y Patrones Usados

### Patrones de Diseño

- **CQRS (Command Query Responsibility Segregation)**:

  - **Descripción**: Este patrón se aplicó separando las operaciones de comando (escritura) y consulta (lectura) en la aplicación. Los comandos se utilizan para operaciones como crear y actualizar productos, mientras que las consultas se utilizan para obtener datos. Esto mejora la claridad y la mantenibilidad del código.
  - **Implementación**:
    - Comandos: `CreateProductCommand`, `UpdateProductCommand`
    - Consultas: `GetProductByIdQuery`

- **Mediator Pattern**:

  - **Descripción**: Utilizamos MediatR para manejar la comunicación entre objetos de manera desacoplada. Esto permite que los controladores no tengan que conocer la lógica de negocio directamente, sino que deleguen las solicitudes a los manejadores apropiados.
  - **Implementación**:
    - `IMediator` se inyecta en los controladores y se utiliza para enviar comandos y consultas.
    - Manejadores: `CreateProductHandler`, `UpdateProductHandler`, `GetProductByIdHandler`

- **Repository Pattern**:

  - **Descripción**: Este patrón se aplicó para abstraer la lógica de acceso a datos, permitiendo que la lógica de negocio se mantenga independiente de la infraestructura de datos. Esto facilita el mantenimiento y la prueba de la aplicación.
  - **Implementación**:
    - Interfaz: `IProductRepository`
    - Implementación: `ProductRepository`
    - La lógica de acceso a datos se implementa en el repositorio, mientras que el servicio de aplicación utiliza el repositorio para realizar operaciones de datos.

- **Middleware**:
  - **Descripción**: Se utilizó middleware personalizado para registrar el tiempo de respuesta de las solicitudes. Esto permite facilitar el monitoreo del rendimiento.
  - **Implementación**:
    - `RequestTimingMiddleware`: Registra el tiempo de respuesta de cada solicitud en un archivo de texto.

### Principios SOLID

El proyecto sigue los principios SOLID para asegurar un código limpio y mantenible:

- **S**: Responsabilidad Única (Single Responsibility Principle) - Cada clase tiene una única responsabilidad.
- **O**: Abierto/Cerrado (Open/Closed Principle) - Las clases están abiertas para extensión pero cerradas para modificación.
- **L**: Sustitución de Liskov (Liskov Substitution Principle) - Las clases derivadas pueden ser reemplazadas por sus clases base.
- **I**: Segregación de Interfaces (Interface Segregation Principle) - Las interfaces específicas son preferidas sobre las interfaces generales.
- **D**: Inversión de Dependencias (Dependency Inversion Principle) - Las dependencias se invierten utilizando inyección de dependencias.

### Estructura del Proyecto

El proyecto está estructurado en varias capas para seguir el principio de separación de responsabilidades:

- **API**: Controladores y configuración de la API.
- **Application**: Lógica de aplicación, comandos y manejadores de comandos.
- **Domain**: Entidades y servicios de dominio.
- **Infrastructure**: Implementación de la persistencia, repositorios y servicios de infraestructura.

## Requisitos Previos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Configuración y Ejecución del Proyecto

### Paso 1: Clonar el Repositorio

```bash
git clone https://github.com/dev-carloscarias/TestNet.git
cd PruebaNET_CarlosCarias_API
```

### Paso 2: Configurar la base de datos

En el archivo 'appsettings.json' dentro del proyecto PruebaNET_CarlosCarias_API debe modificarse la conexion a su conexion local como este ejemplo:

```bash
  "DefaultConnection": "Server=MSI-LITO;Database=TestDB;Integrated Security=True TrustServerCertificate=True;"
```

En SQL Server crear una base de datos llamada TestDB

Luego dentro del proyecto ./TestNet/PruebaNET_CarlosCarias/Prueba_NET.Infrastructure abrir el cmd con los siguientes comandos:

```bash
dotnet ef migrations add InitialCreate --startup-project ../PruebaNet_CarlosCarias_API
dotnet ef database update --startup-project ../PruebaNet_CarlosCarias_API
```

Luego en la base de datos correr el siguiente comando para crear los status

```bash
INSERT INTO ProductStatuses (StatusName) VALUES
('Inactive'),
('Active');
```

### Paso 3: Ejecutar el proyecto

Ejecutar el proyecto PruebaNET_CarlosCarias.sln para levantar el API en Visual Studio 2022 local con .NET 8

### Paso 3: Correr el Frontend

Asegurarse de tener estas versiones de NPM y Angular CLI:

```bash
Node: v16.20.1
Angular CLI: 16.2.14
```

Instalar paquetes

```bash
npm install
```

Correr el siguiente comando para levantar el proyecto:

```bash
ng serve
```
