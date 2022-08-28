# NTTDataPractica
Repositorio Creado para Challenge Practico NTT DATA


Para levantar la aplicacion en DOCKER, se deben realizar los sgtes pasos:


Ubicarse en la ruta del dockerFile
y ejecutar

"docker build -f DockerFile -t nttchallengenetcore ."

Esto nos brindara la imagen necesaria para levantar la app.


Considerar que se necesita una imagen para base de datos sqlserver :   
mcr.microsoft.com/mssql/server:2017-latest  

Seguido de eso se podra levantar las aplicaciones con el sgte comando:

docker-compose up

Disponibles desde : localhost:8003


--- Es necesario Realizar la creacion de la Base de datos y las migraciones respectivas, accediendo desde el puerto disponible :5043
El nombre de la base debe ser NTTBanco



