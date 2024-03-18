#ao commit
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

ENV MongoDB__ConnectionString="mongodb+srv://atendimentos-db:Suxmet13@atendimentos.cvsttlz.mongodb.net/?retryWrites=true&w=majority"
ENV MongoDB__DatabaseName="atendimentos-db"

COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "LanchoneteDaRua.Ms.Pedidos.Api.dll"]


