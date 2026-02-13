• Requisitos previos 
- Node.js 20.13.1
- Angular CLI 16
- .NET 8
- SQL Server 20

• Instrucciones para ejecutar base de datos, backend y frontend 

back
package para el funcionamiento
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
o en administrador de nutget
y conexion a base de datos
5)al json y crear la cadena de conexion 
 "ConnectionStrings": {
   "conexion": "Server=46PORTXWXE\\MSSQLSERVER02;Database=Prueba;Trusted_Connection=True;TrustServerCertificate=True"
 }

 front
 angular/cli@16

• Decisiones o supuestos relevantes
Se utilizó arquitectura en capas
Se usó Entity Framework Core
Se utilizó Angular 16 con Reactive Forms


1. Si el sistema empieza a crecer y más usuarios lo usan al mismo tiempo, ¿qué 3 mejoras harías primero y por qué?

1) Implementar caché ( Memory Cache)
Lo haría para reducir la carga sobre la base de datos en consultas. Esto mejoraría considerablemente el tiempo de respuesta y el comportamiento del sistema.

2) Optimizar la base de datos (índices y consultas)
Revisaría las consultas más usadas y agregaría mejoras en las columnas. También optimizaría consultas pesadas para evitar bloqueos y mejorar la velocidad cuando múltiples usuarios accedan al mismo tiempo.

3) Implementar paginación y filtros eficientes Aplicaría paginación en todos lass consultas grandesen lugar de traer todos los registros. Esto reduce consumo de memoria y carga en la base de datos cuando el volumen de datos crece.

--Menciona tres buenas prácticas que aplicaste en Angular en esta solución.

En el frontend con Angular apliqué las siguientes buenas prácticas:

1️) Uso de Reactive Forms con FormGroup y FormArray
Implementé formularios reactivos para manejar validaciones y estados del formulario. Utilicé FormArray para permitir agregarmúltiples exámenes dentro de una orden, manteniendo el control estructurado de los datos.

2️) Validaciones personalizadas y control de errores en el template
Agregué validaciones como la restricción de fecha futura (futureDate) y mostré mensajes de error utilizando *ngIf, lo que mejora la experiencia del usuario y asegura integridad en los datos antes de enviarlos al backend.

3️) Separación de lógica en servicios
Centralicé las llamadas HTTP en servicios independientes (por ejemplo, examen.service.ts), evitando colocar lógica de comunicación con la API dentro del componente, lo que facilita el mantenimiento y la escalabilidad.

 
