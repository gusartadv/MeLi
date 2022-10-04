# MeLi
## A test for MeLi

Para este test se utilizaron las siguientes herramientas
- Net core 6 - Framework en el que se creo la api
- Sql Server - Para la creacion de la base de datos
- Docker - para contenerizar la api y la base de datos de sql server
- Swagger - para documentar la api y hacer los diferentes test
- Azure - Como cloud para alojar la api y la base de datos
- AKS - Como servicio de contenerizacion
- Entity Framework - Como ORM

## tecnicas - Patrones - Arquitectura

- Tecnica code first para la creacion de la base de datos
- La arquitectura de la solucion se diseño en capas
- El patron utilizado fue el repository pattern
- Para las pruebas unitarias se utilizo el patron Arrange - Act - Assert
- Se creo un middleware para el manejo de las excepciones