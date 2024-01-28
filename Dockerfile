FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app
COPY . .
RUN dotnet restore . 
RUN dotnet build "./src/LanchoneteDaRua.Ms.Pedidos.Api/LanchoneteDaRua.Ms.Pedidos.Api.csproj" -c Release

ENTRYPOINT ["dotnet", "LanchoneteDaRua.Ms.Pedidos.Api.dll"]
