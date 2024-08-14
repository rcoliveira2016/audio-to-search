#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AudioToSearch.Api/AudioToSearch.Api.csproj", "src/AudioToSearch.Api/"]
COPY ["src/AudioToSearch.Aplication/AudioToSearch.Aplication.csproj", "src/AudioToSearch.Aplication/"]
COPY ["src/AudioToSearch.Domain/AudioToSearch.Domain.csproj", "src/AudioToSearch.Domain/"]
COPY ["src/AudioToSearch.Infra.CrossCutting.Settings/AudioToSearch.Infra.CrossCutting.Settings.csproj", "src/AudioToSearch.Infra.CrossCutting.Settings/"]
COPY ["src/AudioToSearch.Infra.Data/AudioToSearch.Infra.Data.csproj", "src/AudioToSearch.Infra.Data/"]
COPY ["src/AudioToSearch.Infra.ServiceAgents.SpeechToText/AudioToSearch.Infra.ServiceAgents.SpeechToText.csproj", "src/AudioToSearch.Infra.ServiceAgents.SpeechToText/"]
RUN dotnet restore "./src/AudioToSearch.Api/AudioToSearch.Api.csproj"
COPY . .
WORKDIR "/src/src/AudioToSearch.Api"
RUN dotnet build "./AudioToSearch.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AudioToSearch.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AudioToSearch.Api.dll"]