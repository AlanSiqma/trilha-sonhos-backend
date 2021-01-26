#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Ostium.BeforeIDie.API/Ostium.BeforeIDie.API.csproj", "Ostium.BeforeIDie.API/"]
RUN dotnet restore "Ostium.BeforeIDie.API/Ostium.BeforeIDie.API.csproj"
COPY . .
WORKDIR "/src/Ostium.BeforeIDie.API"
RUN dotnet build "Ostium.BeforeIDie.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ostium.BeforeIDie.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ostium.BeforeIDie.API.dll"]