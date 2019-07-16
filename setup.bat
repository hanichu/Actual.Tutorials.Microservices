@ECHO OFF
set mode=%1

IF "%mode%"=="cleanup" goto cleanup

ECHO Ensure you updated "nginx.conf" with machine IP address then hit enter
PAUSE

ECHO =================================================================
ECHO = PULL MONGODB DOCKER IMAGE
ECHO =================================================================
docker pull mongo:xenial

ECHO =================================================================
ECHO = START MONGODB CONTAINER
ECHO =================================================================
docker run -d --restart unless-stopped --name tutorial-mongo -p 27017:27017 mongo:xenial

ECHO =================================================================
ECHO = LOADING TEST DATA
ECHO =================================================================
docker cp ./mongosetup.js tutorial-mongo:\home\mongosetup.js
docker exec -it tutorial-mongo mongo "mongodb://localhost:27017" /home/mongosetup.js

ECHO =================================================================
ECHO = PULL RABBITMQ DOCKER IMAGE
ECHO =================================================================
docker pull rabbitmq:management-alpine

ECHO =================================================================
ECHO = START RABBITMQ DOCKER IMAGE
ECHO =================================================================
docker run -d --restart unless-stopped --name tutorial-rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:management-alpine

timeout 10

ECHO =================================================================
ECHO = ADDING USER AND ENABLING ACCESS
ECHO =================================================================
docker exec -it tutorial-rabbitmq rabbitmqctl add_user tutorial P2ssw0rd
docker exec -it tutorial-rabbitmq rabbitmqctl set_user_tags tutorial administrator
docker exec -it tutorial-rabbitmq rabbitmqctl set_permissions -p / tutorial ".*" ".*" ".*"

ECHO =================================================================
ECHO = BUILDING NGINX GATEWAY
ECHO =================================================================
docker build -t tutorial-nginx:latest .

ECHO =================================================================
ECHO = STARTING NGINX GATEWAY
ECHO =================================================================
docker run -d --restart unless-stopped --name tutorial-nginx -p 8280:8280 -p 8082:80 tutorial-nginx

ECHO =================================================================
ECHO = VERIFY CONTAINERS
ECHO =================================================================
docker ps


PAUSE
goto finalexit

:cleanup

ECHO =================================================================
ECHO = CLEANING UP NGINX
ECHO =================================================================

docker stop tutorial-nginx
docker rm tutorial-nginx
docker rmi tutorial-nginx:latest

ECHO =================================================================
ECHO = CLEANING UP MONGODB
ECHO =================================================================

docker stop tutorial-mongo
docker rm tutorial-mongo
docker rmi mongo:xenial

ECHO =================================================================
ECHO = CLEANING UP RABBITMQ
ECHO =================================================================

docker stop tutorial-rabbitmq
docker rm tutorial-rabbitmq
docker rmi rabbitmq:management-alpine

:finalexit

ECHO =================================================================
ECHO = COMPLETED
ECHO =================================================================

