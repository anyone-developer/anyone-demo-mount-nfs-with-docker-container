docker build -t webapp-with-nfs-client:latest . 
docker run -it -e HOSTNAME=172.17.0.2 -e MOUNT_PATH=exports --name=webapp-with-nfs-client-1.3 --link nfs-server:nfs --privileged webapp-with-nfs-client:latest

#mount -o nolock $NFS_PORT_2049_TCP_ADDR:/exports /mnt/exports