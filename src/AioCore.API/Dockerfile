#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AioCore.API/AioCore.API.csproj", "src/AioCore.API/"]
COPY ["src/AioCore.Application/AioCore.Application.csproj", "src/AioCore.Application/"]
COPY ["src/AioCore.Domain/AioCore.Domain.csproj", "src/AioCore.Domain/"]
COPY ["src/AioCore.Shared/AioCore.Shared.csproj", "src/AioCore.Shared/"]
COPY ["src/AioCore.Infrastructure/AioCore.Infrastructure.csproj", "src/AioCore.Infrastructure/"]
RUN dotnet restore "src/AioCore.API/AioCore.API.csproj"
COPY . .
WORKDIR "/src/src/AioCore.API"
RUN dotnet build "AioCore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AioCore.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AioCore.API.dll"]
