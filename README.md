# MeLi
## A test for MeLi

Para este test se utilizaron las siguientes herramientas
- Net core 6 - Framework en el que se creo la api
- Sql Server - Para la creacion de la base de datos
- Docker - para contenerizar la api
- Swagger - para documentar la api y hacer los diferentes test
- Azure - Como cloud para alojar la api y la base de datos
- ACR - Como servicio de almacenamiento de contenedores
- Entity Framework - Como ORM
- App service como servicio para alojar el contenedor

## Tecnicas - Patrones - Arquitectura

- Tecnica code first para la creacion de la base de datos
- La arquitectura de la solucion se diseño en capas
- El patron utilizado fue el repository pattern
- Para las pruebas unitarias se utilizo el patron Arrange - Act - Assert
- Se creo un middleware para el manejo de las excepciones

## Probar el API

- https://melitest20221004153511.azurewebsites.net/swagger/index.html

- Existen 3 usuarios registrados para hacer pruebas

|Id              |TARGET                         |
|----------------|-------------------------------|
|1               |NEW                            |
|2               |NEW                            |
|3               |NEW                            |