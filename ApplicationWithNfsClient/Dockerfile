#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
RUN apt-get update -qq && apt-get install -y nfs-common -qq && apt-get install -y telnet -qq && apt-get install -y telnet -qq && apt-get install -y net-tools -qq 
RUN echo "mkdir -p /mnt/"'$MOUNT_PATH'"" >> /mnt/mount.sh
RUN echo "mount -o nolock "'$HOSTNAME'":/"'$MOUNT_PATH'" /mnt/"'$MOUNT_PATH'"" >> /mnt/mount.sh
WORKDIR /app
EXPOSE 80 443 111/udp 2049/tcp

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY . ./ApplicationWithNfsClient/
WORKDIR "/src/ApplicationWithNfsClient"
RUN dotnet build "ApplicationWithNfsClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApplicationWithNfsClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /scripts
RUN echo "sh /mnt/mount.sh" >> /scripts/commands.sh
RUN echo "dotnet /app/ApplicationWithNfsClient.dll" >> /scripts/commands.sh
ENTRYPOINT ["sh", "/scripts/commands.sh"]
