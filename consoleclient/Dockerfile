FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /

COPY src/consoleclient.csproj /build/

RUN dotnet restore build/consoleclient.csproj

COPY src/ build/

WORKDIR /build/
RUN dotnet publish "consoleclient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "consoleclient.dll"]
