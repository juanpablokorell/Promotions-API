#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Promotions.API/Promotions.API.csproj", "Promotions.API/"]
COPY ["Promotions.Infrastucture/Promotions.Infrastucture.csproj", "Promotions.Infrastucture/"]
COPY ["Promotions.Core/Promotions.Core.csproj", "Promotions.Core/"]
RUN dotnet restore "Promotions.API/Promotions.API.csproj"
COPY . .
WORKDIR "/src/Promotions.API"
RUN dotnet build "Promotions.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Promotions.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Promotions.API.dll"]
