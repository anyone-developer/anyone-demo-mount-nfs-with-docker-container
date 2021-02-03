# anyone-demo-mount-nfs-with-docker-container

There are some people ask me how to make .NET 5 app to use remote nfs server with offical Microsoft aspnet image. I just demo how to mount nfs with image `mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim`. You can make it work in K8S or any other orchestration platform. As long as you have nfs server first. Then have network access to nfs. Mount it and use it.

## By the way
*If you like my module, please buy me a coffee.*

*More and more tiny and useful GitHub action modules are on the way. Please donate to me. I accept a part-time job contract. if you need, please contact me: zhang_nan_163@163.com*

## Demo show up

### 1. Start nfs-server locally

go into the Nfs-Server folder. you would see a Dockerfile. Please build it as a local image.

```shell
docker build -t=yourname/nfs .
```

### 2. Run a Container locally

```shell
docker run -it --name nfs-server --privileged yourname/nfs
```

### 3. Setting up nfs-server

docker exec to container and make some modification.

```shell
echo "/exports *(rw,sync,no_subtree_check,fsid=0,no_root_squash)" >> /etc/exports
```

Then execute some commands:

```shell
# export file system from Linux
exportfs -r
# Start rpcbind service
service rpcbind start
# Start nfs service
service nfs-kernel-server start
```

### 4. Build Demo dotnet web application

go into ApplicationWithNfsClient folder. build image application image locally.

```shell
docker build -t webapp-with-nfs-client:latest . 
```

### 5. Run application Container

run Container into your docker environment. keep the container in same network with nfs-server. replease place holder $NFS_SERVER_IP with real ip of nfs-server container.

```shell
docker run -it -e HOSTNAME=$NFS_SERVER_IP -e MOUNT_PATH=exports --name=webapp-with-nfs-client-1.3 --link nfs-server:nfs --privileged webapp-with-nfs-client:latest
```

### 6. Try use curl to request webapp-client

## Donation

PalPal: https://paypal.me/nzhang4

<img src="https://raw.githubusercontent.com/anyone-developer/anyone-validate-json/main/misc/alipay.JPG" width="500">

<img src="https://raw.githubusercontent.com/anyone-developer/anyone-validate-json/main/misc/webchat_pay.JPG" width="500">


