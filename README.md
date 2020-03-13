# General info

This project was created for the Docker Compose related training session. This repository includes a simple web API project and a DockerFile to create the image for this project. In the training session, the Docker Compose file will be added to orchestrate the db container and the application container.

Before integrate Docker compose to the project, the following steps could be used to create the containers manually. 

## Contents
* [Create db container](#Create-db-container)
* [Webapp using db container](#Webapp-using-db-container)
* [Containerize the application](#Containerize-the-application)
* [Add Docker Compose support](#Add-Docker-Compose-support)

## Create db container

The first step will be create a database container based on MySQL. 
* Download the docker image from Docker Hub repository
> *docker pull mysql*
* Create a new volume to store the data. This is necessary to prevent loss of information if the container is stopped. 
> *docker volume create session7_1*
* Create a new container for MySQL. This container will use the new volume and environment variables required by MySQL (https://hub.docker.com/_/mysql). 
> *docker run --name dblocal -e MYSQL_RANDOM_ROOT_PASSWORD=yes -e MYSQL_DATABASE=fooddb -e MYSQL_USER=test -e MYSQL_PASSWORD=123456 -v session7_1:/var/lib/mysql -p 3311:3306 -d mysql*
* The new container could be accesed through port 3311 using the credentials indicated (user test and password 123456).


## Webapp using db container

After db container, a new web application will be created to use the database. 
* Create a new web api project with Linux Docker support.
* The following nuget packages were added to upport Entity Framework and MySQL. 
   - Microsoft.EntityFrameworkCore
   - Microsoft.EntityFrameworkCore.Design
   - Pomelo.EntityFrameworkCore.MySql
* A new entity was added to the project: *Models.Food*. This only has an ID and a name.
* A new context was added to support Entity Framework and store new information using Food entity: *Models.FoodContext*.
* A new controller was added. This include only Get and Post actions. *FoodController*.
* Appsettings.json was updated to add a new connection string using the db parameters defined for the db container.
* Startup class was updated to use the context and read the connection string.
* The following command was executed to create the migrations: 
> *dotnet ef migrations add Initial*
* Finally, the web application could be executed using IIS Express. The app will access to db container to return/add items.


## Containerize the application

The next step will be containerize the web application to be available using Docker.
* Create a docker image for the project
> docker build -f "C:\Users\Erik Basto\source\repos\Session7\Session7\Dockerfile" 
--force-rm -t session7_img  "C:\Users\Erik Basto\source\repos\Session7" 
* Create a container based on the new image and linked to db container
> docker run --name session7_app -p 8086:80 -e "ConnectionStrings:FoodDB"="Server=dblocal;Port=3306;Database=fooddb; Uid=test; Pwd=123456" --link dblocal -it session7_img
* Consider the following points:
   - The connection string was overwrite to support the db container using -e parameter.
   - The server parameter in the connection string was changed **to have the same name as the db container**: dblocal.
   - The port parameter in the connection string was changed to have the same value as internal port in db container: 3306. When a container A is linked to container B, all the environment variables declared in A are exported to B, **all exposed ports are opened to container B**, and the IP address of A is also exported by the name of container A.
* Consume the get/post methods. The db records included in the previous section are availables.

## Add Docker Compose support

This will be added on the next session.




