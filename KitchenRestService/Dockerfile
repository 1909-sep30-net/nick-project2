# latest
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

# restore dependencies as separate layer
COPY *.sln ./
COPY KitchenRestService.Logic/*.csproj KitchenRestService.Logic/
COPY KitchenRestService.Data/*.csproj KitchenRestService.Data/
COPY KitchenRestService.Tests/*.csproj KitchenRestService.Tests/
COPY KitchenRestService.Api/*.csproj KitchenRestService.Api/

RUN dotnet restore

COPY . ./

RUN dotnet publish KitchenRestService.Api -c Release -o publish --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0

WORKDIR /app

COPY --from=build /app/publish ./

CMD dotnet KitchenRestService.Api.dll
