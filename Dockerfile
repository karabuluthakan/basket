FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build
COPY . .
RUN dotnet restore src/Basket.Api/Basket.Api.csproj
RUN dotnet publish -c Release src/Basket.Api/Basket.Api.csproj -o /app
# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]
