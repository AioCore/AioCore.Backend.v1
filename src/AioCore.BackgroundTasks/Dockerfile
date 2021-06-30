#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AioCore.BackgroundTasks/AioCore.BackgroundTasks.csproj", "src/AioCore.BackgroundTasks/"]
COPY ["src/AioCore.Shared/05.AioCore.Shared.csproj", "src/AioCore.Shared/"]
COPY ["src/AioCore.Packages/Package.Elasticsearch/Package.Elasticsearch.csproj", "src/AioCore.Packages/Package.Elasticsearch/"]
COPY ["src/AioCore.Packages/Package.Extensions/Package.Extensions.csproj", "src/AioCore.Packages/Package.Extensions/"]
COPY ["src/AioCore.Packages/Package.EventBus/Package.EventBus.csproj", "src/AioCore.Packages/Package.EventBus/"]
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