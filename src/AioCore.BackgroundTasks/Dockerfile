FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AioCore.BackgroundTasks/AioCore.BackgroundTasks.csproj", "src/AioCore.BackgroundTasks/"]
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