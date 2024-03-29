#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["InjaAPI/InjaAPI.csproj", "InjaAPI/"]
COPY ["InjaData/InjaData.csproj", "InjaData/"]
COPY ["InjaDTO/InjaDTO.csproj", "InjaDTO/"]
RUN dotnet restore "InjaAPI/InjaAPI.csproj"
COPY . .
WORKDIR "/src/InjaAPI"
RUN dotnet build "InjaAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InjaAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InjaAPI.dll"]