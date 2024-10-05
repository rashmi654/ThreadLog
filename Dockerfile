
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./src/ .

RUN dotnet publish "./Logger.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM  mcr.microsoft.com/dotnet/runtime:6.0-alpine AS prod
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Logger.dll"]