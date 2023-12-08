# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore

# copy everything else and build app
WORKDIR /source/HilfeOhneGrenzen.API/
RUN dotnet publish --no-restore -c Release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime

RUN ln -s /lib/x86_64-linux-gnu/libdl.so.2 /lib/x86_64-linux-gnu/libdl.so
RUN apt update
RUN apt-get install -y libgdiplus
RUN ln -s /usr/lib/libgdiplus.so /lib/x86_64-linux-gnu/libgdiplus.so
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev

WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "camp-fire.API.dll"]