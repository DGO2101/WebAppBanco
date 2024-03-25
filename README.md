WebAppBanco
WebAppBanco es una aplicación web desarrollada en ASP.NET Core que permite gestionar tarjetas de crédito, compras y pagos asociados. La aplicación utiliza una base de datos SQL Server para almacenar la información.

Configuración:
Requisitos previos
.NET Core SDK instalado en tu máquina.
Configuración de la base de datos
La aplicación utiliza Entity Framework Core y una base de datos SQL Server. Para configurar la cadena de conexión a la base de datos, sigue estos pasos:

Abre el archivo appsettings.json en la raíz del proyecto.
En la sección "ConnectionStrings", asegúrate de que la cadena de conexión apunte a tu base de datos SQL Server.
"ConnectionStrings": {
    "WebAppBancoContext": "Server=<server>;Database=<database>;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
Reemplaza <server> y <database> con los valores correspondientes a tu instancia de SQL Server y la base de datos que deseas utilizar.
Ejecución de la aplicación
Para ejecutar la aplicación, sigue estos pasos:

Abre una terminal en la carpeta raíz del proyecto.
Ejecuta el comando dotnet run para iniciar la aplicación.
Una vez que la aplicación esté en funcionamiento, podrás gestionar tarjetas de crédito, compras y pagos a través de la interfaz web. Puedes utilizar las API proporcionadas para integrar la funcionalidad en otros sistemas.
