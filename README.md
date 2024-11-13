<h1 align="center">E.T. Nº12 D.E. 1º "Libertador Gral. José de San Martín"</h1>
<p align="center">
  <img src="https://et12.edu.ar/imgs/et12.svg">
</p>

## Computación 2024

**Asignatura**: **Programación Sobre Redes**

**Nombre TP**: **HospedApp**

**Apellido y nombre Alumno**: **Lugones Ramón, Noelia Almaraz, Josué Alvarado**

**Curso**: **6to 7ma**

# HospedApp

**HospedApp** es una aplicación diseñada para facilitar la gestión de hoteles, proporcionando herramientas clave para administrar clientes, cuartos y reservas de forma eficiente. Ideal para pequeñas y medianas empresas del sector hotelero, **HospedApp** simplifica el control de la ocupación de habitaciones, la asignación de clientes y el seguimiento de reservas en tiempo real.

## Funcionalidades principales:

- **Gestión de Hoteles**: Registra y administra diferentes hoteles, con sus respectivas configuraciones y características.
- **Clientes**: Almacena la información de los clientes, permitiendo crear perfiles simples para gestionar su historial de reservas.
- **Cuartos**: Organiza y clasifica las habitaciones disponibles en cada hotel.
- **Reservas**: Permite reservar cuartos en un hotel, especificando el período de la estancia (fecha de inicio y fin), asegurando la disponibilidad para el cliente.

## Comenzando 🚀
Clonar el repositorio

```
git clone https://github.com/Ramon-Ariel-ET12/HospedApp/
```

Una vez ubicado en la carpeta MySQL iniciar la conexion a la base de datos
```
mysql -u [Nombre de usuario] -p
```
Inicializar los scripts
```
source Install.sql
```
Iniciar el proyecto
```
dotnet run
```


### Pre-requisitos 📋

Antes de comenzar, asegúrate de tener instalados los siguientes componentes en tu máquina:

1. **[.NET SDK](https://dotnet.microsoft.com/download)** - Necesario para compilar y ejecutar aplicaciones en C#.
2. **[MySQL](https://www.mysql.com/downloads/)** - Debes tener MySQL instalado y en funcionamiento para gestionar la base de datos.
5. **[Git](https://git-scm.com/)** - Para clonar el repositorio y gestionar el control de versiones.

## Construido con 🛠️

- **[Visual Studio Code](https://code.visualstudio.com/)** - Editor de código ligero utilizado para el desarrollo del proyecto.
- **[C#](https://learn.microsoft.com/dotnet/csharp/)** - Lenguaje de programación utilizado para la lógica de la aplicación.
- **[ASP.NET MVC](https://dotnet.microsoft.com/apps/aspnet/mvc)** - Framework para estructurar la aplicación siguiendo el patrón Modelo-Vista-Controlador (MVC).
- **[MySQL](https://www.mysql.com/)** - Base de datos relacional para almacenar la información.
- **[Dapper](https://github.com/DapperLib/Dapper)** - Micro ORM utilizado para facilitar el acceso a la base de datos.
- **[Bootstrap](https://getbootstrap.com/)** - Framework CSS utilizado para dar estilo y estructura a la interfaz.

## Autores ✒️

* **Ramon-Ariel-ET12** - *Desarrollo Full-Stack* - [Ramon-Ariel-ET12](https://github.com/Ramon-Ariel-ET12)
* **NoeliaAT** - *Desarrollo Back-End* - [NoeliaAT](https://github.com/NoeliaAT)
* **ElJonsu** - *Diseñador de interfaces* - [ElJonsu](https://github.com/ElJonsu)

## Desventajas del proyecto
### 1. Uso masivo de memoria:
El proyecto en si al consultar la base de datos, al consultar una tabla, no trae en si en un dato especifico, sino que trae todos los datos de la tabla, en caso de una relacion traerá también en esa tabla, lo que termina causando que el navegador del usuario utilice más recursos.
### 2. Manipulación de datos:
Al tener los datos extraidos de la base de datos, tenes que ordenarlo de mayor a menor (asi mostrando los más recientes), lo que se deberia hacerse al momento de hacer una consulta sql (traer datos de la base de datos), también aplicando el uso de JavaScript, en momentos de una dependencia de un select a otro select, tambien en la barra de busqueda que su función es ocultar los datos del usuario cuando el valor de la busqueda que no corresponda a un valor de un dato de la tabla.