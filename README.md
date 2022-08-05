# Workshop-microservices

This project is designed to optimize a high-loaded digital goods marketplace.

![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/Immortian/Workshop-microservices?include_prereleases)

## Whats Including In This Repository

#### Docker compose which includes:

* ***potgreSQL*** - 3 containers on image: postgres:13.3 (users database, items database and transactions database)
* ***pgAdmin*** - container on image: dpage/pgadmin4 to manage DBs
* ***RabbitMQ*** - massage broker on image: rabbitmq:3-management with web UI
* ***ASP.Net Core*** - 3 containers on image builded from mcr.microsoft.com/dotnet/aspnet:6.0 (users microservice, items microservice and confirm transaction microservice)

## Run The Project
You will need the following tools:

* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* [Git Bash](https://git-scm.com/downloads)

You may also need this tools:

* [Visual Studio](https://visualstudio.microsoft.com/en/)

### Configuration
Follow these steps to get your development environment set up:

1. Clone this repository:
```
 git clone "https://github.com/Immortian/Workshop-microservices"
```
2. Go to Workshop-microservices/ folder with cmd and build docker compose file
```
 docker compose up -d 
```
Next steps depends on your purpose

### DBs management:
1. Go to http://localhost:5051/ (or open pgAdmin with browser in Docker Desktop)
2. Sign In using authorization data in docker compose.yml file 
```
default: 
	PGADMIN_DEFAULT_EMAIL: admin@admin.com
	PGADMIN_DEFAULT_PASSWORD: workshopsv
```
3. For each posgre container: Add new server: fill up name, there you have to fill connection tab
* "Host name/address": container name in docker compose.yml
```
example:
	container_name: workshop-users-pg-13.3
```
* "port": 5432
* "Username": user name in docker compose.yml
```
example:
	POSTGRES_USER: workshopuser
```
* "Password": password in docker compose.yml
```
example:
	POSTGRES_PASSWORD: workshopsv
```
4. Enjoy

### RabbitMQ management:

1. Go to http://localhost:15672/ (or open RabbitMQ with browser in Docker Desktop)
2. Sign In using username: guest and passwork guest
3. In another tab go to http://localhost:8002/Swagger/index.html
4. Execute Http post request with random data
5. Watch in RabbitMQ

### ASP.Net microservices management:
***There are 2 environments:***
#### Production - default environment in docker compose.yml
it will be automaticaly applyed, when you run docker compose

#### Development - to debug  without virtualization (but the reqired services must be run separately)

1. Read docker compose.yml to find reqirements of microservices you need
```
exemple:
	depends_on:
	  - rabbitmq
```
2. Search for options of reqired container: container_name, image, ports
3. For each reqired container: in cmd console, use this command:
```
docker run -p [ports]:[ports] --name [container_name] -d [image]
```
4. Go to Visual Studio and assign projects to be launched
5. Start projects
