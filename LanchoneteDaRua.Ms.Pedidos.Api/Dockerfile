FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LanchoneteDaRua.Ms.Pedidos.Api/LanchoneteDaRua.Ms.Pedidos.Api.csproj", "LanchoneteDaRua.Ms.Pedidos.Api/"]
RUN dotnet restore "LanchoneteDaRua.Ms.Pedidos.Api/LanchoneteDaRua.Ms.Pedidos.Api.csproj"
COPY . .
WORKDIR "/src/LanchoneteDaRua.Ms.Pedidos.Api"
RUN dotnet build "LanchoneteDaRua.Ms.Pedidos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LanchoneteDaRua.Ms.Pedidos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LanchoneteDaRua.Ms.Pedidos.Api.dll"]
