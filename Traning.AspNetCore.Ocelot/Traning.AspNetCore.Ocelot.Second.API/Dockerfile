#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Traning.AspNetCore.Ocelot.Second.API/Traning.AspNetCore.Ocelot.Second.API.csproj", "Traning.AspNetCore.Ocelot.Second.API/"]
RUN dotnet restore "Traning.AspNetCore.Ocelot.Second.API/Traning.AspNetCore.Ocelot.Second.API.csproj"
COPY . .
WORKDIR "/src/Traning.AspNetCore.Ocelot.Second.API"
RUN dotnet build "Traning.AspNetCore.Ocelot.Second.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Traning.AspNetCore.Ocelot.Second.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Traning.AspNetCore.Ocelot.Second.API.dll"]