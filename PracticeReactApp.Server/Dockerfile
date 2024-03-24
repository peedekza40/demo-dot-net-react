# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PracticeReactApp.Server.csproj", "PracticeReactApp.Server/"]
RUN dotnet restore "PracticeReactApp.Server/PracticeReactApp.Server.csproj"
COPY . .
WORKDIR "/src/PracticeReactApp.Server"
RUN dotnet build "PracticeReactApp.Server.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "PracticeReactApp.Server.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PracticeReactApp.Server.dll"]