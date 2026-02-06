FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY Chronos.API/Chronos.API.csproj                         Chronos.API/
COPY Chronos.Application/Chronos.Application.csproj         Chronos.Application/
COPY Chronos.Infrastructure/Chronos.Infrastructure.csproj   Chronos.Infrastructure/
COPY Chronos.Core/Chronos.Core.csproj                       Chronos.Core/

RUN dotnet restore Chronos.API

COPY . .

RUN dotnet publish Chronos.API -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /src/out .
EXPOSE 80
CMD ["dotnet", "Chronos.API.dll"]

