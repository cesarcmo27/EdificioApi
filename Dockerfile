# compilacion
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source

COPY  . .

RUN dotnet restore "./API/API.csproj" --disable-parallel
RUN dotnet publish "./API/API.csproj" -c release -o /app --no-restore


# ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal 
WORKDIR /app

COPY --from=build /app ./

expose 5000

ENTRYPOINT [ "dotnet","API.dll" ]
