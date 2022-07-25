# Workshop-microsevices

This project is designed to optimize a high-loaded digital goods marketplace.

## Whats Including In This Repository

#### Docker compose which includes:

* ***potgreSQL*** - 3 containers on postgres:13.3 image (users database, items database and transactions database)
* ***pgAdmin*** - container on dpage/pgadmin4 image to manage DBs

## Run The Project
You will need the following tools:

* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* [Git Bash](https://git-scm.com/downloads)

### Configuration
Follow these steps to get your development environment set up:

1. Clone this repository:
```
 git clone "https://github.com/Immortian/Workshop-microsevices"
```
2. Go to WorkShop/ folder with cmd and build docker compose file
```
 docker compose up -d 
```
3. Go to http://localhost:5050/ (or open pgAdmin with browser in Docker Desktop)
4. Sign In using authorization data in docker compose.yml file 
```
default: 
	PGADMIN_DEFAULT_EMAIL: admin@admin.com
	PGADMIN_DEFAULT_PASSWORD: workshopsv
```
5. For each posgre container: Add new server: fill up name, there you have to fill connection tab
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
6. Enjoy