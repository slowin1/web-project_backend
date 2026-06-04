FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY backend.sln ./
COPY eUseControl.Api/eUseControl.Api.csproj eUseControl.Api/
COPY eUseControl.BussinessLogic/eUseControl.BussinessLogic.csproj eUseControl.BussinessLogic/
COPY eUseControl.Domain/eUseControl.Domain.csproj eUseControl.Domain/
COPY EUseControl.DataAccess/EUseControl.DataAccess.csproj EUseControl.DataAccess/

RUN dotnet restore eUseControl.Api/eUseControl.Api.csproj

COPY . .
RUN dotnet publish eUseControl.Api/eUseControl.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["sh", "-c", "dotnet eUseControl.Api.dll --urls http://0.0.0.0:${PORT:-8080}"]
