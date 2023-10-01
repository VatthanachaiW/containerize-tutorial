# docker command

## Check current docker information
```bash
docker info
```

## Diplay docker images
```bash
docker images
```

## Display docker container
```bash
docker ps
```
** to display all use -a
```bash
docker ps -a
```
## check network
```bash
docker network ls
```

## Build docker image
```bash
docker build -t <image name>:<tag optional> -f <path to DockerFile> .
```
Sample
The docker file are current store in folder src 
```bash
docker build -t webapi -f Dockerfile .\src
```

## Docker tag to tag with version or etc.
```bash
docker tag <original image name>:<tag> <new image name>:<tag>
```
Sample
Need to add new tag to current docker image
```bash
docker tag webapi:8.0-alpine webapi:latest
```

## Login to docker repository
```bash
docker login <repository>
```
and enter the username and password

## Push docker image to repository
```bash
docker push <repository>/<docker image name>:<tag>
```

## Pull docker image from repository
```bash
docker pull <repository>/<docker image name>:<tag>
```

## Delete docker image
```bash
docker rmi <imagename / id>:<tag>
```

## Delete docker container
```bash
docker rm <container name / id>
```

## clear builder
```bash
docker builder prune
```

## clear all container
```bash
docker container prune
```

## clear docker image
```bash
docker image prune
```

## Run container
```bash
docker run --name <container name> -p <target port>:<expose port> -e <environment key>=<value> -v <target volume>:<source volume>
```
sample
```bash
docker run --name webapi -p 8080:80 -e ASPNET_ENVIRONMENT=Development -v ./data/:/data/
```

