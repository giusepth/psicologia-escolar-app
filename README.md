# API de Gestión de Tickets para Atención Psicológica Escolar

##  Descripción General

Este proyecto consiste en una API RESTful desarrollada en ASP.NET Core que permite la gestión de tickets de atención psicológica dentro de un entorno escolar. A través de esta solución, los docentes pueden registrar solicitudes dirigidas al área de psicología para dar seguimiento a estudiantes que requieren acompañamiento.

La API permite crear, consultar, actualizar y eliminar tickets, asegurando así una trazabilidad clara y organizada de cada caso.


##  Planteamiento del Problema

En el entorno escolar, la comunicación entre docentes y el equipo de psicología puede presentar desafíos logísticos, especialmente cuando se manejan múltiples casos simultáneamente. El registro de solicitudes suele depender de medios informales o no estructurados (como notas físicas, mensajes por redes sociales o correos electrónicos), lo que puede dar lugar a olvidos, demoras o pérdidas de información importante.

Para resolver esta problemática, se propone la implementación de una API que estandarice y digitalice el proceso de solicitud de atención psicológica. Esto permitirá llevar un control centralizado de cada solicitud, registrar las respuestas ofrecidas y acceder al historial de seguimiento de forma ordenada y segura.


##  Tecnologías Utilizadas

| Tecnología     | Uso en el Proyecto |
|----------------|--------------------|
| **ASP.NET Core** | Framework principal para la construcción de la API RESTful. Elegido por su robustez, escalabilidad y soporte empresarial. |
| **SQL Server**   | Sistema de gestión de base de datos relacional usado para almacenar toda la información. Se eligió por su integración nativa con .NET y su potencia en consultas complejas. |
| **ADO.NET**      | Biblioteca para la conexión y manipulación directa de datos entre la aplicación y la base de datos mediante procedimientos almacenados. Ideal para controlar con precisión cada operación. |
| **Postman**      | Herramienta utilizada para probar y validar los endpoints de la API de forma visual y dinámica. |
| **Git & GitHub** | Control de versiones y hospedaje del código fuente, facilitando la colaboración, historial de cambios y despliegue continuo. |


##  Objetivos

### Objetivo General

Desarrollar una API RESTful que permita la gestión eficiente de tickets de atención psicológica escolar, facilitando el registro, seguimiento y resolución de solicitudes realizadas por los docentes hacia el área de psicología.

### Objetivos Específicos

- Diseñar una estructura de base de datos que almacene información relevante de los tickets, incluyendo datos del usuario, descripción del caso, estado y respuesta.
- Implementar endpoints seguros y funcionales en ASP.NET Core para realizar operaciones CRUD (crear, leer, actualizar y eliminar) sobre los tickets.
- Conectar el backend con SQL Server mediante ADO.NET utilizando procedimientos almacenados para garantizar integridad y eficiencia en las operaciones de base de datos.
- Validar el correcto funcionamiento del API utilizando herramientas como Postman y Swagger para realizar pruebas de las rutas y verificar los datos enviados y recibidos.
- Gestionar el código fuente del proyecto mediante Git y GitHub, asegurando un control de versiones adecuado y la posibilidad de seguimiento del desarrollo.


