#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AioCore.API/01.AioCore.API.csproj", "src/AioCore.API/"]
COPY ["src/DynamicMigrations.MsSql/DynamicMigrations.MsSql.csproj", "src/DynamicMigrations.MsSql/"]
COPY ["src/AioCore.Infrastructure/03.AioCore.Infrastructure.csproj", "src/AioCore.Infrastructure/"]
COPY ["src/AioCore.Domain/04.AioCore.Domain.csproj", "src/AioCore.Domain/"]
COPY ["src/AioCore.Packages/Package.Elasticsearch/Package.Elasticsearch.csproj", "src/AioCore.Packages/Package.Elasticsearch/"]
COPY ["src/AioCore.Packages/Package.Extensions/Package.Extensions.csproj", "src/AioCore.Packages/Package.Extensions/"]
COPY ["src/AioCore.Shared/05.AioCore.Shared.csproj", "src/AioCore.Shared/"]
COPY ["src/AioCore.Packages/Package.NestedSet/Package.NestedSet.csproj", "src/AioCore.Packages/Package.NestedSet/"]
COPY ["src/AioCore.Application/02.AioCore.Application.csproj", "src/AioCore.Application/"]
COPY ["src/AioCore.Packages/Package.Localization/Package.Localization.csproj", "src/AioCore.Packages/Package.Localization/"]
COPY ["src/AioCore.Packages/Package.DatabaseManagement/Package.DatabaseManagement.csproj", "src/AioCore.Packages/Package.DatabaseManagement/"]
COPY ["src/AioCore.Packages/AioCore.Mediator/AioCore.Mediator.csproj", "src/AioCore.Packages/AioCore.Mediator/"]
COPY ["src/AioCore.Packages/Package.FileServer/Package.FileServer.csproj", "src/AioCore.Packages/Package.FileServer/"]
COPY ["src/AioCore.Packages/Package.AutoMapper/Package.AutoMapper.csproj", "src/AioCore.Packages/Package.AutoMapper/"]
COPY ["src/AioCore.Packages/Package.EventBus/Package.EventBus.csproj", "src/AioCore.Packages/Package.EventBus/"]
COPY ["src/AioCore.Packages/Package.Redis/Package.Redis.csproj", "src/AioCore.Packages/Package.Redis/"]
COPY ["src/AioCore.ViewRender/AioCore.ViewRender.csproj", "src/AioCore.ViewRender/"]
COPY ["src/AioCore.ActionProcessors/AioCore.ActionProcessors.csproj", "src/AioCore.ActionProcessors/"]
COPY ["src/DynamicMigrations.PostgresSql/DynamicMigrations.PostgresSql.csproj", "src/DynamicMigrations.PostgresSql/"]
RUN dotnet restore "src/AioCore.API/01.AioCore.API.csproj"
COPY . .
WORKDIR "/src/src/AioCore.API"
RUN dotnet build "01.AioCore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "01.AioCore.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AioCore.API.dll"]