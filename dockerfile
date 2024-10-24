FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY . ./

RUN dotnet restore 

RUN dotnet publish -c release -o /Publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0

EXPOSE 5000

ENV ASPNETCORE_URLS="http://0.0.0.0:5000"

WORKDIR /Aplicacion

COPY --from=build /Publish ./

ENTRYPOINT ["dotnet", "HospedApp.MVC.dll"]