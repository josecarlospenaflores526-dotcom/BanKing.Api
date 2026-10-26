[README.md](https://github.com/user-attachments/files/33944333/README.md)
# Banking API

## Descripción del proyecto

Este proyecto consiste en el desarrollo de una API REST para la gestión de cuentas bancarias. Fue realizado utilizando C# y ASP.NET Core con .NET 10, con el objetivo de implementar las operaciones básicas que se realizan en un sistema bancario.

La aplicación permite registrar clientes, crear cuentas bancarias, consultar saldos, realizar depósitos y retiros, así como llevar un historial de los movimientos realizados.

Para guardar la información utilicé SQLite junto con Entity Framework Core.

## Tecnologías utilizadas

Durante el desarrollo del proyecto utilicé las siguientes tecnologías:

- C# como lenguaje de programación.
- ASP.NET Core Web API con .NET 10.
- Entity Framework Core para trabajar con la base de datos.
- SQLite para almacenar la información.
- xUnit para realizar las pruebas unitarias.
- Postman para probar los endpoints.
- Visual Studio como entorno de desarrollo.

## Organización del proyecto

Para mantener el código ordenado, dividí la solución en cuatro proyectos:

**BanKing.Api:** contiene los controladores que reciben las solicitudes HTTP, la configuración de la aplicación y el manejo de errores.

**Banking.Application:** contiene los modelos, las interfaces, los DTOs y las reglas de negocio utilizadas en las operaciones bancarias.

**Banking.Infrastructure:** se encarga de la conexión con la base de datos, los servicios y las migraciones de Entity Framework Core.

**Banking.Tests:** contiene las pruebas unitarias que permiten comprobar el funcionamiento de algunas de las reglas principales del sistema.

Esta separación permite mantener una mejor organización del código y facilita realizar cambios sin afectar innecesariamente otras partes del proyecto.

## Funcionalidades desarrolladas

El sistema cuenta con las siguientes funcionalidades:

- Registrar nuevos clientes y consultar su información.
- Crear cuentas bancarias asociadas a un cliente.
- Generar automáticamente un número de cuenta.
- Consultar el saldo disponible de una cuenta.
- Realizar depósitos y actualizar el saldo.
- Realizar retiros, verificando que exista dinero suficiente.
- Mostrar un mensaje cuando se intenta retirar más dinero del disponible.
- Consultar el historial de depósitos y retiros.
- Evitar números de cuenta duplicados mediante una restricción en la base de datos.

Los números de cuenta utilizan el formato `ACC-YYYYMMDD-XXXX`.

## Base de datos

Para almacenar la información utilicé SQLite.

La base de datos está formada principalmente por tres tablas:

- **Customers:** almacena los datos de los clientes.
- **BankAccounts:** almacena las cuentas bancarias y sus saldos.
- **BankTransactions:** registra los movimientos realizados en cada cuenta.

También utilicé migraciones de Entity Framework Core para crear las tablas y configurar un índice único para los números de cuenta.

## Cómo ejecutar el proyecto

Para ejecutar la aplicación es necesario tener instalado .NET 10 SDK y Visual Studio compatible con esa versión.

Los pasos son los siguientes:

1. Descargar o clonar el repositorio.
2. Abrir la solución en Visual Studio.
3. Restaurar los paquetes NuGet si es necesario.
4. Revisar la cadena de conexión en el archivo `appsettings.json`.
5. Abrir la Consola del Administrador de paquetes NuGet.
6. Ejecutar el siguiente comando para aplicar las migraciones de la base de datos:

```powershell
Update-Database -Project Banking.Infrastructure -StartupProject BanKing.Api
```

7. Seleccionar `BanKing.Api` como proyecto de inicio.
8. Ejecutar la aplicación presionando F5.

Una vez iniciada la API, se pueden probar sus endpoints utilizando Postman.

## Endpoints disponibles

| Método | Endpoint | Función |
|---|---|---|
| POST | `/api/customers` | Registrar un cliente |
| GET | `/api/customers` | Consultar todos los clientes |
| GET | `/api/customers/{id}` | Buscar un cliente por ID |
| POST | `/api/accounts` | Crear una cuenta bancaria |
| GET | `/api/accounts/{number}/balance` | Consultar el saldo |
| POST | `/api/accounts/{number}/deposits` | Realizar un depósito |
| POST | `/api/accounts/{number}/withdrawals` | Realizar un retiro |
| GET | `/api/accounts/{number}/transactions` | Consultar los movimientos |

## Pruebas realizadas

Para comprobar el funcionamiento de la API realicé diferentes pruebas utilizando Postman.

Primero verifiqué que fuera posible registrar clientes y crear cuentas bancarias. Después comprobé que el saldo se actualizara correctamente al realizar depósitos y retiros.

También probé qué sucedía al intentar retirar una cantidad mayor al saldo disponible. En ese caso, la API devolvió un error `400 Bad Request` con el mensaje de fondos insuficientes, evitando que se realizara la operación.

Por último, consulté el historial de transacciones para verificar que los depósitos y retiros quedaran registrados.

Además, agregué pruebas unitarias con xUnit para comprobar las operaciones de depósito, retiro, validación de fondos y generación de números de cuenta.

Al ejecutar las pruebas desde Visual Studio, se obtuvieron **6 pruebas aprobadas y ninguna fallida**.

## Comentario final

Con este proyecto pude poner en práctica diferentes conceptos relacionados con el desarrollo de APIs REST, la organización del código por capas, el uso de bases de datos y la validación de operaciones.

Durante el desarrollo también realicé pruebas para identificar posibles errores y comprobar que las funcionalidades principales respondieran de la manera esperada.

El resultado es una API bancaria básica que permite gestionar clientes, cuentas y movimientos, manteniendo separadas las responsabilidades de cada parte del sistema.
