#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

USER root
RUN chmod -R 777 /app/
USER app

USER root
RUN mkdir /testfiles/
RUN chmod -R 777 /testfiles/
RUN chown -R app:app /testfiles/

USER app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["UserService/UserService.csproj", "UserService/"]
RUN dotnet restore "./UserService/UserService.csproj"
COPY . .
WORKDIR "/src/UserService"
RUN dotnet build "./UserService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER app
ENTRYPOINT ["dotnet", "UserService.dll"]


