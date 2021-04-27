#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["CategorizationService/CategorizationService.csproj", "CategorizationService/"]
RUN dotnet restore "CategorizationService/CategorizationService.csproj"
COPY . .
WORKDIR "/src/CategorizationService"
RUN dotnet build "CategorizationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CategorizationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CategorizationService.dll"]