#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AioCore.BackgroundTasks/AioCore.BackgroundTasks.csproj", "src/AioCore.BackgroundTasks/"]
COPY ["src/AioCore.Shared/AioCore.Shared.csproj", "src/AioCore.Shared/"]
COPY ["src/AioCore.Packages/Package.Localization/Package.Localization.csproj", "src/AioCore.Packages/Package.Localization/"]
COPY ["src/AioCore.Packages/Package.Redis/Package.Redis.csproj", "src/AioCore.Packages/Package.Redis/"]
COPY ["src/AioCore.Packages/Package.Elasticsearch/Package.Elasticsearch.csproj", "src/AioCore.Packages/Package.Elasticsearch/"]
COPY ["src/AioCore.Packages/Package.Extensions/Package.Extensions.csproj", "src/AioCore.Packages/Package.Extensions/"]
COPY ["src/AioCore.Packages/Package.FileServer/Package.FileServer.csproj", "src/AioCore.Packages/Package.FileServer/"]
COPY ["src/AioCore.Packages/Package.AutoMapper/Package.AutoMapper.csproj", "src/AioCore.Packages/Package.AutoMapper/"]
COPY ["src/AioCore.Packages/Package.EventBus/Package.EventBus.csproj", "src/AioCore.Packages/Package.EventBus/"]
COPY ["src/AioCore.Packages/Package.Mongo/Package.Mongo.csproj", "src/AioCore.Packages/Package.Mongo/"]
COPY ["src/AioCore.Packages/Package.NestedSet/Package.NestedSet.csproj", "src/AioCore.Packages/Package.NestedSet/"]
RUN dotnet restore "src/AioCore.BackgroundTasks/AioCore.BackgroundTasks.csproj"
COPY . .
WORKDIR "/src/src/AioCore.BackgroundTasks"
RUN dotnet build "AioCore.BackgroundTasks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AioCore.BackgroundTasks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AioCore.BackgroundTasks.dll"]